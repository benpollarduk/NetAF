using NetAF.Assets;
using NetAF.Assets.Characters;
using NetAF.Assets.Locations;
using NetAF.Extensions;
using NetAF.Logic;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;
using NetAF.Targets.Console.Rendering.FrameBuilders.Legacy;
using NetAF.Utilities;
using System.Linq;

namespace NetAF.Targets.Console.Rendering.FrameBuilders.Modern
{
    /// <summary>
    /// Provides a builder of reaction frames.
    /// </summary>
    /// <param name="gridStringBuilder">A builder to use for the string layout.</param>
    /// <param name="properties">The render properties.</param>
    public sealed class ConsoleModernReactionFrameBuilder(GridStringBuilder gridStringBuilder, ConsoleRenderProperties properties) : ConsoleLegacyFrameBuilder, IReactionFrameBuilder
    {
        #region Implementation of IReactionFrameBuilder

        /// <inheritdoc/>
        public IFrame Build(string title, string message, bool isError, PlayableCharacter player, Room room, Size size)
        {
            gridStringBuilder.Resize(size);

            const int xMargin = 2;
            var availableWidth = size.Width - (xMargin * 2);
            var constrainedWidth = GetConstrainedWidth(availableWidth, properties.MaximumNarrativeLineWidth);
            var lastY = 2;

            string[] keywords = properties.HighlightKeywords switch
            {
                true => [.. Game.GetAllPlayerVisibleExaminables(player, room).Select(x => x.Identifier.Name)],
                false => []
            };

            if (!string.IsNullOrEmpty(title))
            {
                gridStringBuilder.DrawWrapped(title, xMargin, lastY, constrainedWidth, properties.TitleColor, out _, out lastY);

                lastY += 3;
            }

            var finishedMessage = message.EnsureFinishedSentence();
            gridStringBuilder.DrawWrapped(finishedMessage, xMargin, lastY, constrainedWidth, properties.MainTextColor, properties.HighlightTextColor, StringUtilities.LocateStringMarkers(finishedMessage, keywords), out _, out _);

            return new GridTextFrame(gridStringBuilder, 0, 0, properties.BackgroundColor) { ShowCursor = false };
        }

        #endregion
    }
}
