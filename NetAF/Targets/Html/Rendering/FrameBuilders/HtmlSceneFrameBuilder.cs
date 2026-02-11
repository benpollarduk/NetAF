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

            builder.H1(room.Identifier.Name);
            builder.Br();
            builder.P(room.Description.GetDescription().EnsureFinishedSentence());

            var extendedDescription = string.Empty;

            if (viewPoint.Any)
                builder.P(extendedDescription.AddSentence(SceneHelper.CreateViewpointAsString(room, viewPoint).EnsureFinishedSentence()));

            if (player.Items.Length != 0)
                builder.P("You have " + StringUtilities.ConstructExaminablesAsSentence(player.Items?.Cast<IExaminable>().ToArray()).StartWithLower());

            builder.Br();

            roomMapBuilder?.BuildRoomMap(room, viewPoint, keyType);

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
