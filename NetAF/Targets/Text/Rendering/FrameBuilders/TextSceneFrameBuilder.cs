using NetAF.Assets;
using NetAF.Assets.Characters;
using NetAF.Assets.Locations;
using NetAF.Commands;
using NetAF.Extensions;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;
using NetAF.Utilities;
using System.Linq;
using System.Text;

namespace NetAF.Targets.Text.Rendering.FrameBuilders
{
    /// <summary>
    /// Provides a builder of scene frames.
    /// </summary>
    /// <param name="builder">A builder to use for the text layout.</param>
    /// <param name="roomMapBuilder">A builder to use for room maps.</param>
    public sealed class TextSceneFrameBuilder(StringBuilder builder, IRoomMapBuilder roomMapBuilder) : ISceneFrameBuilder
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

            builder.AppendLine(room.Identifier.Name);
            builder.AppendLine();
            builder.AppendLine(room.Description.GetDescription().EnsureFinishedSentence());

            var extendedDescription = string.Empty;

            if (viewPoint.Any)
                builder.AppendLine(extendedDescription.AddSentence(SceneHelper.CreateViewpointAsString(room, viewPoint).EnsureFinishedSentence()));

            if (DisplayExaminables)
                builder.AppendLine(SceneHelper.CreateRoomString(room));

            if (player.Items.Length != 0)
                builder.AppendLine("You have " + StringUtilities.ConstructExaminablesAsSentence(player.Items?.Cast<IExaminable>().ToArray()).StartWithLower());

            builder.AppendLine();

            if (roomMapBuilder != null && showMap)
            {
                roomMapBuilder.BuildRoomMap(room, viewPoint, options);

                builder.AppendLine();
            }

            if (contextualCommands != null && contextualCommands.Length > 0)
            {
                builder.AppendLine(CommandTitle);

                foreach (var command in contextualCommands)
                    builder.AppendLine($"{command.DisplayCommand} - {command.Description.EnsureFinishedSentence()}");
            }

            return new TextFrame(builder);
        }

        #endregion
    }
}
