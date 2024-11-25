using NetAF.Assets;
using NetAF.Assets.Characters;
using NetAF.Assets.Locations;
using NetAF.Commands;
using NetAF.Conversations;
using NetAF.Conversations.Instructions;
using NetAF.Logic.Modes;
using NetAF.Rendering.Console;
using NetAF.Rendering.FrameBuilders;
using NetAF.Utilities;

namespace NetAF.Examples.Assets.Regions.Hub.Rooms
{
    internal class Clearing : IAssetTemplate<Room>
    {
        #region Constants

        public const string Name = "Jungle Clearing";
        private const string Description = "You are in a small clearing in a jungle, tightly enclosed by undergrowth. You have no idea how you got here. The chirps and buzzes coming from insects in the undergrowth are intense. There are some stone pedestals in front of you. Each has a small globe on top of it.";

        #endregion

        #region StaticMethods

        private static GridPictureFrame GetView(Size size)
        {
            var gridPictureBuilder = new GridPictureBuilder(AnsiColor.Black, AnsiColor.BrightWhite);
            gridPictureBuilder.Resize(size);
            gridPictureBuilder.SetCell(40, 14, AnsiColor.Green);
            gridPictureBuilder.SetCell(39, 15, AnsiColor.Green);
            gridPictureBuilder.SetCell(40, 15, AnsiColor.Green);
            gridPictureBuilder.SetCell(41, 15, AnsiColor.Green);
            gridPictureBuilder.SetCell(38, 16, AnsiColor.Green);
            gridPictureBuilder.SetCell(39, 16, AnsiColor.Green);
            gridPictureBuilder.SetCell(40, 16, AnsiColor.Green);
            gridPictureBuilder.SetCell(41, 16, AnsiColor.Green);
            gridPictureBuilder.SetCell(42, 16, AnsiColor.Green);
            gridPictureBuilder.SetCell(37, 17, AnsiColor.Green);
            gridPictureBuilder.SetCell(38, 17, AnsiColor.Green);
            gridPictureBuilder.SetCell(39, 17, AnsiColor.Green);
            gridPictureBuilder.SetCell(40, 17, AnsiColor.Green);
            gridPictureBuilder.SetCell(41, 17, AnsiColor.Green);
            gridPictureBuilder.SetCell(42, 17, AnsiColor.Green);
            gridPictureBuilder.SetCell(43, 17, AnsiColor.Green);
            gridPictureBuilder.SetCell(36, 18, AnsiColor.Green);
            gridPictureBuilder.SetCell(37, 18, AnsiColor.Green);
            gridPictureBuilder.SetCell(38, 18, AnsiColor.Green);
            gridPictureBuilder.SetCell(39, 18, AnsiColor.Green);
            gridPictureBuilder.SetCell(40, 18, AnsiColor.Green);
            gridPictureBuilder.SetCell(41, 18, AnsiColor.Green);
            gridPictureBuilder.SetCell(42, 18, AnsiColor.Green);
            gridPictureBuilder.SetCell(43, 18, AnsiColor.Green);
            gridPictureBuilder.SetCell(44, 18, AnsiColor.Green);
            gridPictureBuilder.SetCell(35, 19, AnsiColor.Green);
            gridPictureBuilder.SetCell(36, 19, AnsiColor.Green);
            gridPictureBuilder.SetCell(37, 19, AnsiColor.Green);
            gridPictureBuilder.SetCell(38, 19, AnsiColor.Green);
            gridPictureBuilder.SetCell(39, 19, AnsiColor.Green);
            gridPictureBuilder.SetCell(40, 19, AnsiColor.Green);
            gridPictureBuilder.SetCell(41, 19, AnsiColor.Green);
            gridPictureBuilder.SetCell(42, 19, AnsiColor.Green);
            gridPictureBuilder.SetCell(43, 19, AnsiColor.Green);
            gridPictureBuilder.SetCell(44, 19, AnsiColor.Green);
            gridPictureBuilder.SetCell(45, 19, AnsiColor.Green);
            gridPictureBuilder.SetCell(40, 20, AnsiColor.Red);
            gridPictureBuilder.SetCell(40, 21, AnsiColor.Red);
            gridPictureBuilder.SetCell(40, 22, AnsiColor.Red);
            gridPictureBuilder.SetCell(40, 23, AnsiColor.Red);
            gridPictureBuilder.SetCell(40, 24, AnsiColor.Red);
            gridPictureBuilder.SetCell(40, 25, AnsiColor.Red);
            return new GridPictureFrame(gridPictureBuilder);
        }

        #endregion

        #region Implementation of IAssetTemplate<Room>

        /// <summary>
        /// Instantiate a new instance of the asset.
        /// </summary>
        /// <returns>The asset.</returns>
        public Room Instantiate()
        {
            var room = new Room(Name, Description, commands:
            [ 
                new(new("Look", "Look around."), true, true, (g, a) =>
                {
                    g.ChangeMode(new DirectRenderMode(GetView(g.Configuration.DisplaySize)));
                    return new(ReactionResult.GameModeChanged, string.Empty);
                })
             ]);

            var conversation = new Conversation(
                new("Squarrrkkk!"),
                new("Would you like to change modes?", "ModeQuestion")
                {
                    Responses =
                    [
                        new("Yes please, change to default."),
                        new("Yes please, change to simple.", new Jump(2)),
                        new("Yes please, change to legacy.", new Jump(3)),
                        new("No thanks, keep things as they are.", new Jump(4))
                    ]
                },
                new("Arrk! Color it is.", g => g.Configuration.FrameBuilders = FrameBuilderCollections.Default, new ToName("ModeQuestion")),
                new("Eeek, simple be fine too! Shame it's been deleted. Maybe it will be implemented again one day! Eeek!", new ToName("ModeQuestion")),
                new("Squarrk! Legacy, looks old. Shame it's been deleted. Maybe it will be implemented again one day! Arrk!", new ToName("ModeQuestion")),
                new("Fine, suit yourself! Squarrk!", new ToName("ModeQuestion"))
            );

            room.AddCharacter(new NonPlayableCharacter(new Identifier("Parrot"), new Description("A brightly colored parrot."), conversation: conversation));

            return room;
        }

        #endregion
    }
}
