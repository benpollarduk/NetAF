using NetAF.Assets;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;
using System;

namespace NetAF.Targets.Html.Rendering.FrameBuilders
{
    /// <summary>
    /// Provides a builder of visual frames.
    /// </summary>
    /// <param name="builder">A builder to use for the text layout.</param>
    /// <param name="resizeMode">The mode to use when the design size and the render size differ and the content needs to be resized.</param>
    public sealed class HtmlVisualFrameBuilder(HtmlBuilder builder, VisualResizeMode resizeMode = VisualResizeMode.Scale) : IVisualFrameBuilder
    {
        #region Properties

        /// <summary>
        /// Get or set the mode to use when the design size and the render size differ and the content needs to be resized.
        /// </summary>
        public VisualResizeMode ResizeMode { get; set; } = resizeMode;

        #endregion

        #region Implementation of IVisualFrameBuilder

        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="visual">The visual.</param>
        /// <param name="size">The size of the frame.</param>
        /// <returns>The frame.</returns>
        public IFrame Build(Visual visual, Size size)
        {
            builder.Clear();

            var usedLines = 0;

            if (!string.IsNullOrEmpty(visual.Name))
            {
                builder.H1(visual.Name);
                builder.Br();

                usedLines += 2;
            }

            if (!string.IsNullOrEmpty(visual.Description))
            {
                builder.P(visual.Description);
                builder.Br();

                usedLines += 2;
            }

            if (visual.VisualBuilder != null)
            {
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
