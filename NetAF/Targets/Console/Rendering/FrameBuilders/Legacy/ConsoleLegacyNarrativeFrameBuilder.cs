using NetAF.Assets;
using NetAF.Extensions;
using NetAF.Narratives;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;
using NetAF.Utilities;
using System;
using System.Text;

namespace NetAF.Targets.Console.Rendering.FrameBuilders.Legacy
{
    /// <summary>
    /// Provides a builder of narrative frames.
    /// </summary>
    /// <param name="gridStringBuilder">A builder to use for the string layout.</param>
    /// <param name="properties">The render properties.</param>
    /// <param name="resizeMode">The mode to use for the visual when the design size and the render size differ and the content needs to be resized.</param>
    public sealed class ConsoleLegacyNarrativeFrameBuilder(GridStringBuilder gridStringBuilder, ConsoleRenderProperties properties, VisualResizeMode resizeMode = VisualResizeMode.ScaleDown) : ConsoleLegacyFrameBuilder, INarrativeFrameBuilder
    {
        #region Properties

        /// <summary>
        /// Get or set the mode to use for the visual when the design size and the render size differ and the content needs to be resized.
        /// </summary>
        public VisualResizeMode ResizeMode { get; set; } = resizeMode;

        #endregion

        #region Methods

        private IFrame BuildVisualFrame(Narrative narrative, Size size)
        {
            gridStringBuilder.Resize(size);

            var lastY = 0;
            var availableWidth = size.Width - 4;
            var constrainedWidth = GetConstrainedWidth(availableWidth, properties.MaximumNarrativeLineWidth);
            const int xMargin = 2;

            if (!string.IsNullOrEmpty(narrative.Title))
            {
                gridStringBuilder.DrawWrapped(narrative.Title, xMargin, 2, constrainedWidth, properties.TitleColor, out _, out lastY);
                gridStringBuilder.DrawUnderline(xMargin, lastY + 1, narrative.Title.Length, properties.TitleColor);
            }

            lastY += 3;

            // resize if needed - -2 because of boundary and space at bottom
            var visual = narrative.CurrentVisual.ResizeIfNeeded(new Size(constrainedWidth, size.Height - lastY - 2), ResizeMode);

            var xOffset = Math.Max(xMargin, size.Width / 2 - visual.VisualBuilder.DisplaySize.Width / 2);
            var yOffset = lastY;

            lastY += visual.VisualBuilder.DisplaySize.Height + 1;

            StringBuilder builder = new();

            foreach (var line in narrative.AllUntilCurrent())
                builder.AppendLine(line + StringUtilities.Newline);

            gridStringBuilder.DrawWrapped(builder.ToString().EnsureFinishedSentence(), xMargin, lastY, constrainedWidth, properties.MainTextColor, out _, out lastY);

            gridStringBuilder.DrawBoundary(properties.LeftBoundary, properties.RightBoundary, properties.HorizontalDivider, properties.BorderColor);

            GridVisualBuilder finalBuilder = new(properties.BackgroundColor, properties.TitleColor);
            finalBuilder.Resize(size);
            finalBuilder.Overlay(xOffset, yOffset, visual.VisualBuilder);
            finalBuilder.Overlay(0, 0, gridStringBuilder);

            return new GridVisualFrame(finalBuilder) { ShowCursor = false };
        }

        private IFrame BuildTextFrame(Narrative narrative, Size size)
        {
            gridStringBuilder.Resize(size);

            var lastY = 0;
            const int xMargin = 2;
            var availableWidth = size.Width - (xMargin * 2);
            var constrainedWidth = GetConstrainedWidth(availableWidth, properties.MaximumNarrativeLineWidth);

            lastY += 2;

            if (!string.IsNullOrEmpty(narrative.Title))
            {
                gridStringBuilder.DrawWrapped(narrative.Title, xMargin, lastY, constrainedWidth, properties.TitleColor, out _, out lastY);
                gridStringBuilder.DrawUnderline(xMargin, lastY + 1, narrative.Title.Length, properties.TitleColor);

                lastY += 3;
            }

            StringBuilder builder = new();

            foreach (var line in narrative.AllUntilCurrent())
                builder.AppendLine(line + StringUtilities.Newline);

            gridStringBuilder.DrawWrapped(builder.ToString().EnsureFinishedSentence(), xMargin, lastY, constrainedWidth, properties.MainTextColor, out _, out lastY);

            gridStringBuilder.DrawBoundary(properties.LeftBoundary, properties.RightBoundary, properties.HorizontalDivider, properties.BorderColor);

            return new GridTextFrame(gridStringBuilder, 0, 0, properties.BackgroundColor) { ShowCursor = false };
        }

        #endregion

        #region Implementation of INarrativeFrameBuilder

        /// <inheritdoc/>
        public IFrame Build(Narrative narrative, Size size)
        {
            if (narrative.CurrentVisual != null)
                return BuildVisualFrame(narrative, size);
            else
                return BuildTextFrame(narrative, size);
        }

        #endregion
    }
}
