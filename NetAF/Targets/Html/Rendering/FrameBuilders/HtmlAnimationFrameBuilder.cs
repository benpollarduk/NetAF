using NetAF.Assets;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;

namespace NetAF.Targets.Html.Rendering.FrameBuilders
{
    /// <summary>
    /// Provides a builder of animation frames.
    /// </summary>
    /// <param name="builder">A builder to use for the text layout.</param>
    /// <param name="resizeMode">The mode to use when the design size and the render size differ and the content needs to be resized.</param>
    public sealed class HtmlAnimationFrameBuilder(HtmlBuilder builder, VisualResizeMode resizeMode = VisualResizeMode.ScaleDown) : IAnimationFrameBuilder
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
            builder.Clear();

            var usedLines = 0;

            if (!string.IsNullOrEmpty(animation.Name))
            {
                builder.H1(animation.Name);
                builder.Br();

                usedLines += 2;
            }

            if (!string.IsNullOrEmpty(animation.Description))
            {
                builder.P(animation.Description);
                builder.Br();

                usedLines += 2;
            }

            if (animation.VisualBuilder != null)
            {
                // determine the render size
                var renderSize = new Size(size.Width, size.Height - usedLines);

                // check if resize of the animation is needed
                animation = animation.ResizeIfNeeded(renderSize, ResizeMode);

                builder.Raw(HtmlAdapter.ConvertGridVisualBuilderToHtmlString(animation.VisualBuilder));
            }

            return new HtmlFrame(builder);
        }

        #endregion
    }
}
