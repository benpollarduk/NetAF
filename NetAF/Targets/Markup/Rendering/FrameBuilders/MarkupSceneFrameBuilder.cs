using NetAF.Assets;
using NetAF.Assets.Characters;
using NetAF.Assets.Locations;
using NetAF.Commands;
using NetAF.Extensions;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;
using NetAF.Utilities;
using System.Linq;

namespace NetAF.Targets.Markup.Rendering.FrameBuilders
{
    /// <summary>
    /// Provides a builder of scene frames.
    /// </summary>
    /// <param name="builder">A builder to use for the text layout.</param>
    /// <param name="roomMapBuilder">A builder to use for room maps.</param>
    public sealed class MarkupSceneFrameBuilder(MarkupBuilder builder, IRoomMapBuilder roomMapBuilder) : ISceneFrameBuilder
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

            builder.Heading(room.Identifier.Name, HeadingLevel.H1);
            builder.Newline();
            builder.WriteLine(room.Description.GetDescription().EnsureFinishedSentence());
            builder.Newline();

            var extendedDescription = string.Empty;

            if (viewPoint.Any)
                builder.WriteLine(extendedDescription.AddSentence(SceneHelper.CreateViewpointAsString(room, viewPoint).EnsureFinishedSentence()));

            if (DisplayExaminables)
                builder.WriteLine(SceneHelper.CreateRoomString(room));

            if (player.Items.Length != 0)
                builder.WriteLine("You have " + StringUtilities.ConstructExaminablesAsSentence(player.Items?.Cast<IExaminable>().ToArray()).StartWithLower());

            builder.Newline();

            if (roomMapBuilder != null && showMap)
            {
                roomMapBuilder.BuildRoomMap(room, viewPoint, options);

                builder.Newline();
            }

            if (contextualCommands != null && contextualCommands.Length > 0)
            {
                builder.Heading(CommandTitle, HeadingLevel.H2);

                foreach (var command in contextualCommands)
                {
                    builder.Write(command.DisplayCommand, new TextStyle(Bold: true));
                    builder.WriteLine($" - {command.Description.EnsureFinishedSentence()}");
                }
            }

            return new MarkupFrame(builder);
        }

        #endregion
    }
}
