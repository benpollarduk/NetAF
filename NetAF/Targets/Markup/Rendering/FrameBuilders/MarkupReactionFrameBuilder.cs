using NetAF.Assets;
using NetAF.Extensions;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;

namespace NetAF.Targets.Markup.Rendering.FrameBuilders
{
    /// <summary>
    /// Provides a builder of reaction frames.
    /// </summary>
    /// <param name="builder">A builder to use for the text layout.</param>
    public sealed class MarkupReactionFrameBuilder(MarkupBuilder builder) : IReactionFrameBuilder
    {
        #region Implementation of IReactionFrameBuilder

        /// <inheritdoc/>
        public IFrame Build(string title, string message, bool isError, Size size)
        {
            builder.Clear();

            if (!string.IsNullOrEmpty(title))
            {
                builder.Heading(title, HeadingLevel.H1);
                builder.Newline();
            }

            builder.WriteLine(message.EnsureFinishedSentence());

            return new MarkupFrame(builder);
        }

        #endregion
    }
}
