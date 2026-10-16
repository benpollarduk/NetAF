using NetAF.Assets;
using NetAF.Extensions;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;
using NetAF.Utilities;

namespace NetAF.Targets.Markup.Rendering.FrameBuilders
{
    /// <summary>
    /// Provides a builder of reaction frames.
    /// </summary>
    /// <param name="builder">A builder to use for the text layout.</param>
    /// <param name="properties">The render properties.</param>
    public sealed class MarkupReactionFrameBuilder(MarkupBuilder builder, MarkupRenderProperties properties) : IReactionFrameBuilder
    {
        #region Implementation of IReactionFrameBuilder

        /// <inheritdoc/>
        public IFrame Build(string title, string message, bool isError, string[] keywords, Size size)
        {
            builder.Clear();

            if (!string.IsNullOrEmpty(title))
            {
                builder.Heading(title, HeadingLevel.H1);
                builder.Newline();
            }

            if (!properties.HighlightKeywords)
                keywords = [];

            var finishedMessage = message.EnsureFinishedSentence();
            builder.WriteLine(finishedMessage, StringUtilities.LocateStringMarkers(finishedMessage, keywords));

            return new MarkupFrame(builder);
        }

        #endregion
    }
}
