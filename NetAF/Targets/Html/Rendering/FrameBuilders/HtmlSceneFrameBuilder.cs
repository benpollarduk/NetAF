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
    /// <param name="roomMapBuilder">A builder to use for room maps.</param>
    public sealed class HtmlSceneFrameBuilder(HtmlBuilder builder, IRoomMapBuilder roomMapBuilder) : ISceneFrameBuilder
    {
        #region Properties

        /// <summary>
        /// Get or set the command title.
        /// </summary>
        public string CommandTitle { get; set; } = "You can:";

        #endregion

        #region Implementation of ISceneFrameBuilder

        /// <inheritdoc/>
        public IFrame Build(Room room, ViewPoint viewPoint, PlayableCharacter player, CommandHelp[] contextualCommands, bool showMap, RoomMapRenderOptions options, Size size)
        {
            builder.Clear();

            builder.H1(room.Identifier.Name);
            builder.Br();
            builder.P(room.Description.GetDescription().EnsureFinishedSentence());

            var extendedDescription = string.Empty;

            if (viewPoint.Any)
                builder.P(extendedDescription.AddSentence(SceneHelper.CreateViewpointAsString(room, viewPoint).EnsureFinishedSentence()));

            if (player.Items.Length != 0)
                builder.P("You have " + StringUtilities.ConstructExaminablesAsSentence(player.Items?.Cast<IExaminable>().ToArray()).StartWithLower());

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
