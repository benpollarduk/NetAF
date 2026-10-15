using NetAF.Assets;
using NetAF.Assets.Characters;
using NetAF.Assets.Locations;
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
        public IFrame Build(string title, string message, bool isError, PlayableCharacter player, Room room, Size size)
        {
            builder.Clear();

            if (!string.IsNullOrEmpty(title))
            {
                builder.Heading(title, HeadingLevel.H1);
                builder.Newline();
            }

            string[] keywords = properties.HighlightKeywords switch
            {
                true => StringUtilities.FindKeywords(new ExaminationScene(player, room)),
                false => []
            };

            var finishedMessage = message.EnsureFinishedSentence();
            builder.WriteLine(finishedMessage, StringUtilities.LocateStringMarkers(finishedMessage, keywords));

            return new MarkupFrame(builder);
        }

        #endregion
    }
}
