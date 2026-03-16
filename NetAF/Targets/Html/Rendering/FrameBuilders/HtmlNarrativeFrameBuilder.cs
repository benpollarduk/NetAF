using NetAF.Assets;
using NetAF.Narratives;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;
using NetAF.Utilities;
using System;
using System.Text;

namespace NetAF.Targets.Html.Rendering.FrameBuilders
{
    /// <summary>
    /// Provides a builder of narrative frames.
    /// </summary>
    /// <param name="builder">A builder to use for the text layout.</param>
    /// <param name="resizeMode">The mode to use for the visual when the design size and the render size differ and the content needs to be resized.</param>
    public sealed class HtmlNarrativeFrameBuilder(HtmlBuilder builder, VisualResizeMode resizeMode = VisualResizeMode.Scale) : INarrativeFrameBuilder
    {
        #region Properties

        /// <summary>
        /// Get or set the mode to use for the visaul when the design size and the render size differ and the content needs to be resized.
        /// </summary>
        public VisualResizeMode ResizeMode { get; set; } = resizeMode;

        #endregion

        #region Implementation of INarrativeFrameBuilder

        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="narrative">The narrative.</param>
        /// <param name="size">The size of the frame.</param>
        /// <returns>The frame.</returns>
        public IFrame Build(Narrative narrative, Size size)
        {
            builder.Clear();

            StringBuilder stringBuilder = new();

            foreach (var line in narrative.AllUntilCurrent())
                stringBuilder.AppendLine(line + StringUtilities.Newline);

            var usedLines = 0;

            builder.H1(narrative.Title);
            builder.Br();
            builder.P(stringBuilder.ToString());

            usedLines = 3;

            if (narrative.CurrentVisual?.VisualBuilder != null)
            {
                builder.Br();
                usedLines++;

                var visual = narrative.CurrentVisual;

                // determine the render size
                var renderSize = new Size(size.Width, size.Height - usedLines);

                // check if resize of the visual is needed
                if (visual.VisualBuilder.DisplaySize.Width != renderSize.Width ||
                    visual.VisualBuilder.DisplaySize.Height != renderSize.Height)
                {
                    // perform resize
                    visual = ResizeMode switch
                    {
                        VisualResizeMode.Crop => visual.Crop(renderSize),
                        VisualResizeMode.Scale => visual.Scale(renderSize),
                        _ => throw new NotImplementedException()
                    };
                }

                builder.Raw(HtmlAdapter.ConvertGridVisualBuilderToHtmlString(visual.VisualBuilder));
            }

            return new HtmlFrame(builder);
        }

        #endregion
    }
}
