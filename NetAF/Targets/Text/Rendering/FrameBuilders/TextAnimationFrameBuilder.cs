using NetAF.Assets;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetAF.Targets.Text.Rendering.FrameBuilders
{
    /// <summary>
    /// Provides a builder of animation frames.
    /// </summary>
    /// <param name="builder">A builder to use for the text layout.</param>
    /// <param name="resizeMode">The mode to use when the design size and the render size differ and the content needs to be resized.</param>
    public sealed class TextAnimationFrameBuilder(StringBuilder builder, VisualResizeMode resizeMode = VisualResizeMode.ScaleDown) : IAnimationFrameBuilder
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
            List<string> frames = [];

            int renderNonAnimationContent()
            {
                builder.Clear();

                var usedLines = 0;

                if (!string.IsNullOrEmpty(animation.Name))
                {
                    builder.AppendLine(animation.Name);
                    builder.AppendLine();

                    usedLines += 2;
                }

                if (!string.IsNullOrEmpty(animation.Description))
                {
                    builder.AppendLine(animation.Description);
                    builder.AppendLine();

                    usedLines += 2;
                }

                return usedLines;
            }

            if (animation?.VisualBuilders?.Length > 0)
            {
                foreach (var visualBuilder in animation.VisualBuilders)
                {
                    var usedLines = renderNonAnimationContent();

                    if (animation.VisualBuilders?.Length > 0)
                    {
                        // determine the render size
                        var renderSize = new Size(size.Width, size.Height - usedLines);

                        // check if resize of the animation is needed
                        animation = animation.ResizeIfNeeded(renderSize, ResizeMode);

                        builder.Append(TextAdapter.ConvertGridVisualBuilderToString(visualBuilder));
                    }

                    frames.Add(builder.ToString());
                }
            }
            else
            {
                renderNonAnimationContent();
                frames.Add(builder.ToString());
            }

            return new FixedRateUpdatableFrame([.. frames], size, (int)TimeSpan.FromSeconds(1d / animation.Options.FramesPerSecond).TotalMilliseconds, animation.Options.Cycles);
        }

        #endregion
    }
}
