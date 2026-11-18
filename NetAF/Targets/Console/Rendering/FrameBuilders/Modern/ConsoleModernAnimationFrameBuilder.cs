using NetAF.Assets;
using NetAF.Extensions;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;
using NetAF.Targets.Console.Rendering.FrameBuilders.Legacy;
using System;
using System.Collections.Generic;

namespace NetAF.Targets.Console.Rendering.FrameBuilders.Modern
{
    /// <summary>
    /// Provides a builder of animation frames.
    /// </summary>
    /// <param name="gridStringBuilder">A builder to use for the string layout.</param>
    /// <param name="properties">The render properties.</param>
    /// <param name="resizeMode">The mode to use when the design size and the render size differ and the content needs to be resized.</param>
    public sealed class ConsoleModernAnimationFrameBuilder(GridStringBuilder gridStringBuilder, ConsoleRenderProperties properties, VisualResizeMode resizeMode = VisualResizeMode.ScaleDown) : ConsoleLegacyFrameBuilder, IAnimationFrameBuilder
    {
        #region Properties

        /// <summary>
        /// Get or set the mode to use when the design size and the render size differ and the content needs to be resized.
        /// </summary>
        public VisualResizeMode ResizeMode { get; set; } = resizeMode;

        #endregion

        #region Implementation of IAnimationFrameBuilder

        /// <inheritdoc/>
        public IFrame Build(Animation animation, Size size)
        {
            gridStringBuilder.Resize(size);

            const int xMargin = 2;
            var availableWidth = size.Width - (xMargin * 2);
            var constrainedWidth = GetConstrainedWidth(availableWidth, properties.MaximumNarrativeLineWidth);

            var lastY = 2;

            if (!string.IsNullOrEmpty(animation.Name))
            {
                gridStringBuilder.DrawWrapped(animation.Name, xMargin, lastY, constrainedWidth, properties.TitleColor, out _, out lastY);

                lastY += 3;
            }

            if (!string.IsNullOrEmpty(animation.Description))
                gridStringBuilder.DrawWrapped(animation.Description.EnsureFinishedSentence(), xMargin, lastY + 3, constrainedWidth, properties.MainTextColor, out _, out lastY);

            List<GridVisualFrame> frames = [];

            foreach (var visualBuilder in animation.VisualBuilders)
            {
                GridVisualBuilder finalBuilder = new(properties.BackgroundColor, properties.TitleColor);
                finalBuilder.Resize(size);

                // check if resize of the animation is needed
                animation = animation.ResizeIfNeeded(new Size(availableWidth, size.Height - lastY - 2), ResizeMode);

                var xOffset = Math.Max(xMargin, size.Width / 2 - animation.Width / 2);
                var yOffset = Math.Max(lastY, size.Height / 2 - animation.Height / 2);

                finalBuilder.Overlay(0, 0, gridStringBuilder);
                finalBuilder.Overlay(xOffset, yOffset, visualBuilder);
                frames.Add(new GridVisualFrame(finalBuilder) { ShowCursor = false });
            }

            return new GridAnimatedVisualFrame([.. frames], animation.Options) { ShowCursor = false };
        }

        #endregion
    }
}
