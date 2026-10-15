using NetAF.Assets;
using NetAF.Assets.Characters;
using NetAF.Assets.Locations;
using NetAF.Commands;
using NetAF.Extensions;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;
using NetAF.Utilities;
using System.Linq;

namespace NetAF.Targets.Html.Rendering.FrameBuilders
{
    /// <summary>
    /// Provides a builder of scene frames.
    /// </summary>
    /// <param name="builder">A builder to use for the text layout.</param>
    /// <param name="properties">The render properties.</param>
    /// <param name="roomMapBuilder">A builder to use for room maps.</param>
    public sealed class HtmlSceneFrameBuilder(HtmlBuilder builder, HtmlRenderProperties properties, IRoomMapBuilder roomMapBuilder) : ISceneFrameBuilder
    {
        #region Properties

        /// <summary>
        /// Get or set the command title.
        /// </summary>
        public string CommandTitle { get; set; } = "You can:";

        /// <summary>
        /// Get or set if examinables should be displayed.
        /// </summary>
        public bool DisplayExaminables { get; set; } = true;

        #endregion

        #region Implementation of ISceneFrameBuilder

        /// <inheritdoc/>
        public IFrame Build(Room room, ViewPoint viewPoint, PlayableCharacter player, CommandHelp[] contextualCommands, bool showMap, RoomMapRenderOptions options, Size size)
        {
            builder.Clear();

            string[] keywords = properties.HighlightKeywords switch
            {
                true => StringUtilities.FindKeywords(new ExaminationScene(player, room), viewPoint),
                false => []
            };

            builder.H1(room.Identifier.Name);
            builder.Br();

            var description = room.Description.GetDescription().EnsureFinishedSentence();
            builder.P(description, StringUtilities.LocateStringMarkers(description, keywords));

            var extendedDescription = string.Empty;

            if (viewPoint.Any)
            {
                extendedDescription.AddSentence(SceneHelper.CreateViewpointAsString(room, viewPoint).EnsureFinishedSentence());
                builder.P(extendedDescription, StringUtilities.LocateStringMarkers(extendedDescription, keywords));
            }

            if (DisplayExaminables)
            {
                var roomString = SceneHelper.CreateRoomString(room);

                if (!string.IsNullOrEmpty(roomString))
                    builder.P(roomString, StringUtilities.LocateStringMarkers(roomString, keywords));
            }

            if (player.Items.Length != 0)
            {
                var inventoryString = $"You have {StringUtilities.ConstructExaminablesAsSentence(player.Items?.Cast<IExaminable>().ToArray()).StartWithLower()}";
                builder.P(inventoryString, StringUtilities.LocateStringMarkers(inventoryString, keywords));
            }

            builder.Br();

            if (roomMapBuilder != null && showMap)
                roomMapBuilder.BuildRoomMap(room, viewPoint, options);

            if (contextualCommands != null && contextualCommands.Length > 0)
            {
                builder.H4(CommandTitle);

                foreach (var command in contextualCommands)
                    builder.P($"{command.DisplayCommand} - {command.Description.EnsureFinishedSentence()}");
            }

            return new HtmlFrame(builder);
        }

        #endregion
    }
}
