using NetAF.Assets;
using NetAF.Extensions;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;
using NetAF.Utilities;

namespace NetAF.Targets.Html.Rendering.FrameBuilders
{
    /// <summary>
    /// Provides a builder of reaction frames.
    /// </summary>
    /// <param name="builder">A builder to use for the text layout.</param>
    /// <param name="properties">The render properties.</param>
    public sealed class HtmlReactionFrameBuilder(HtmlBuilder builder, HtmlRenderProperties properties) : IReactionFrameBuilder
    {
        #region Implementation of IReactionFrameBuilder

        /// <inheritdoc/>
        public IFrame Build(string title, string message, bool isError, string[] keywords, Size size)
        {
            builder.Clear();

            if (!string.IsNullOrEmpty(title))
            {
                builder.H1(title);
                builder.Br();
            }
            
            if (!properties.HighlightKeywords)
                keywords = [];

            var finishedMessage = message.EnsureFinishedSentence();
            builder.P(finishedMessage, StringUtilities.LocateStringMarkers(finishedMessage, keywords));

            return new HtmlFrame(builder);
        }

        #endregion
    }
}
