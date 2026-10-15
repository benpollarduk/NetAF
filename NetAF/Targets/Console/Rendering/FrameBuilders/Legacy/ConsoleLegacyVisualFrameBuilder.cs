using NetAF.Assets;
using NetAF.Extensions;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;
using System;

namespace NetAF.Targets.Console.Rendering.FrameBuilders.Legacy
{
    /// <summary>
    /// Provides a builder of visual frames.
    /// </summary>
    /// <param name="gridStringBuilder">A builder to use for the string layout.</param>
    /// <param name="properties">The render properties.</param>
    /// <param name="resizeMode">The mode to use when the design size and the render size differ and the content needs to be resized.</param>
    public sealed class ConsoleLegacyVisualFrameBuilder(GridStringBuilder gridStringBuilder, ConsoleRenderProperties properties, VisualResizeMode resizeMode = VisualResizeMode.ScaleDown) : ConsoleLegacyFrameBuilder, IVisualFrameBuilder
    {
        #region Properties

        /// <summary>
        /// Get or set the mode to use when the design size and the render size differ and the content needs to be resized.
        /// </summary>
        public VisualResizeMode ResizeMode { get; set; } = resizeMode;

        #endregion

        #region Implementation of IVisualFrameBuilder

        /// <inheritdoc/>
        public IFrame Build(Visual visual, Size size)
        {
            gridStringBuilder.Resize(size);

            const int xMargin = 2;
            var availableWidth = size.Width - (xMargin * 2);
            var constrainedWidth = GetConstrainedWidth(availableWidth, properties.MaximumNarrativeLineWidth);

            var lastY = 2;

            if (!string.IsNullOrEmpty(visual.Name))
            {
                gridStringBuilder.DrawWrapped(visual.Name, xMargin, lastY, constrainedWidth, properties.TitleColor, out _, out lastY);

                gridStringBuilder.DrawUnderline(xMargin, lastY + 1, visual.Name.Length, properties.TitleColor);

                lastY += 3;
            }

            if (!string.IsNullOrEmpty(visual.Description))
                gridStringBuilder.DrawWrapped(visual.Description.EnsureFinishedSentence(), xMargin, lastY + 3, constrainedWidth, properties.MainTextColor, out _, out lastY);

            GridVisualBuilder finalBuilder = new(properties.BackgroundColor, properties.TitleColor);
            finalBuilder.Resize(size);

            // check if resize of the visual is needed
            visual = visual.ResizeIfNeeded(new Size(availableWidth, size.Height - lastY - 2), ResizeMode);

            var xOffset = Math.Max(xMargin, size.Width / 2 - visual.VisualBuilder.DisplaySize.Width / 2);
            var yOffset = Math.Max(lastY, size.Height / 2 - visual.VisualBuilder.DisplaySize.Height / 2);

            finalBuilder.Overlay(0, 0, gridStringBuilder);
            finalBuilder.Overlay(xOffset, yOffset, visual.VisualBuilder);

            gridStringBuilder.DrawBoundary(properties.LeftBoundary, properties.RightBoundary, properties.HorizontalDivider, properties.BorderColor);

            return new GridVisualFrame(finalBuilder) { ShowCursor = false };
        }

        #endregion
    }
}
