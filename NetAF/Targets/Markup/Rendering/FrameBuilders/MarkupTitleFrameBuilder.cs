using NetAF.Assets;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;

namespace NetAF.Targets.Markup.Rendering.FrameBuilders
{
    /// <summary>
    /// Provides a builder of title frames.
    /// </summary>
    /// <param name="builder">A builder to use for the text layout.</param>
    public sealed class MarkupTitleFrameBuilder(MarkupBuilder builder) : ITitleFrameBuilder
    {
        #region Implementation of ITitleFrameBuilder

        /// <inheritdoc/>
        public IFrame Build(string title, string description, Size size)
        {
            builder.Clear();

            builder.Heading(title, HeadingLevel.H1);
            builder.Newline();
            builder.WriteLine(description);

            return new MarkupFrame(builder);
        }

        #endregion
    }
}
