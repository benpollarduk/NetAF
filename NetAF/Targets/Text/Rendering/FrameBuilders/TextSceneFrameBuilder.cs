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

        #endregion

        #region Implementation of ISceneFrameBuilder

        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="room">Specify the Room.</param>
        /// <param name="viewPoint">Specify the viewpoint from the room.</param>
        /// <param name="player">Specify the player.</param>
        /// <param name="contextualCommands">The contextual commands to display.</param>
        /// <param name="keyType">The type of key to use.</param>
        /// <param name="size">The size of the frame.</param>
        /// <returns>The frame.</returns>
        public IFrame Build(Room room, ViewPoint viewPoint, PlayableCharacter player, CommandHelp[] contextualCommands, KeyType keyType, Size size)
        {
            builder.Clear();

            builder.AppendLine(room.Identifier.Name);
            builder.AppendLine();
            builder.AppendLine(room.Description.GetDescription().EnsureFinishedSentence());

            var extendedDescription = string.Empty;

            if (viewPoint.Any)
                builder.AppendLine(extendedDescription.AddSentence(SceneHelper.CreateViewpointAsString(room, viewPoint).EnsureFinishedSentence()));

            if (player.Items.Length != 0)
                builder.AppendLine("You have " + StringUtilities.ConstructExaminablesAsSentence(player.Items?.Cast<IExaminable>().ToArray()).StartWithLower());

            if (player.Attributes.Count > 0)
                builder.AppendLine(StringUtilities.ConstructAttributesAsString(player.Attributes.GetAsDictionary()));

            builder.AppendLine();

            roomMapBuilder.BuildRoomMap(room, viewPoint, keyType);

            builder.AppendLine();

            if (contextualCommands?.Any() ?? false)
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
