using NetAF.Assets;
using NetAF.Extensions;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;

namespace NetAF.Targets.Markup.Rendering.FrameBuilders
{
    /// <summary>
    /// Provides a builder of completion frames.
    /// </summary>
    /// <param name="builder">A builder to use for the text layout.</param>
    public sealed class MarkupCompletionFrameBuilder(MarkupBuilder builder) : ICompletionFrameBuilder
    {
        #region Implementation of ICompletionFrameBuilder

        /// <inheritdoc/>
        public IFrame Build(string message, string reason, Size size)
        {
            builder.Clear();

            builder.Heading(message, HeadingLevel.H1);

            builder.WriteLine(reason.EnsureFinishedSentence());

            return new MarkupFrame(builder);
        }

        #endregion
    }
}
