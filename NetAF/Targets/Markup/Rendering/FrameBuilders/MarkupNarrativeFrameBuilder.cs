using NetAF.Assets;
using NetAF.Narratives;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;
using NetAF.Utilities;
using System.Text;

namespace NetAF.Targets.Markup.Rendering.FrameBuilders
{
    /// <summary>
    /// Provides a builder of narrative frames.
    /// </summary>
    /// <param name="builder">A builder to use for the text layout.</param>
    /// <param name="resizeMode">The mode to use for the visual when the design size and the render size differ and the content needs to be resized.</param>
    public sealed class MarkupNarrativeFrameBuilder(MarkupBuilder builder, VisualResizeMode resizeMode = VisualResizeMode.Scale) : INarrativeFrameBuilder
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

            var usedLines = 0;
            StringBuilder stringBuilder = new();

            foreach (var line in narrative.AllUntilCurrent())
            {
                stringBuilder.AppendLine(line + StringUtilities.Newline);
                usedLines += 2;
            }

            builder.Heading(narrative.Title, HeadingLevel.H1);
            builder.Newline();

            usedLines += 2;

            if (narrative.CurrentVisual?.VisualBuilder != null)
            {
                builder.Newline();
                usedLines++;

                // resize if needed
                var visual = narrative.CurrentVisual.ResizeIfNeeded(new Size(size.Width, size.Height - usedLines), ResizeMode);

                builder.Raw(MarkupAdapter.ConvertGridVisualBuilderToMarkupString(visual.VisualBuilder));
            }

            builder.WriteLine(stringBuilder.ToString());

            return new MarkupFrame(builder);
        }

        #endregion
    }
}
