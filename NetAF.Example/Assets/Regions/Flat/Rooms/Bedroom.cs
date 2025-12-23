using NetAF.Assets.Locations;
using NetAF.Commands;
using NetAF.Example.Assets.Regions.Flat.Items;
using NetAF.Logic.Modes;
using NetAF.Narratives;
using NetAF.Utilities;

namespace NetAF.Example.Assets.Regions.Flat.Rooms
{
    public class Bedroom : IAssetTemplate<Room>
    {
        #region Constants

        private const string Name = "Bedroom";
        private const string Description = "The bedroom is large, with one duck-egg blue wall.There is a double bed against the western wall, and a few other items of bedroom furniture are dotted around, but they all look pretty scruffy.To the north is a doorway leading to the hallway. A book is on the bedside table.";

        #endregion

        #region Implementation of IAssetTemplate<Room>

        /// <summary>
        /// Instantiate a new instance of the asset.
        /// </summary>
        /// <returns>The asset.</returns>
        public Room Instantiate()
        {
            CustomCommand[] commands =
            [
                new CustomCommand(new CommandHelp("Read Book", "Read the book."), true, true, (g, _) =>
                {
                    var section1 = new Section(["Now this is a story all about how,", "My life got flipped, turned upside down,", "And I'd like to take a minute, just sit right there,", "I'll tell you how I became the prince of a town called, er, Minehead."]);
                    var section2 = new Section(["Actually it isn't. It is an example of how to use a Narrative.", "What's a Narrative?", "Whose eyes are those eyes?"]);
                    var section3 = new Section(["Arrrrggghhhhhh!"]);
                    var section4 = new Section(["You close the book in a hurry!"]);

                    var narrative = new Narrative("Fresh Prince Of Minehead", [section1, section2, section3, section4]);
                    g.ChangeMode(new NarrativeMode(narrative));

                    return new Reaction(ReactionResult.GameModeChanged, string.Empty);
                })
            ];

            var room = new Room(Name, Description, [new Exit(Direction.North), new Exit(Direction.Up)], commands: commands);

            room.AddItem(new Bed().Instantiate());
            room.AddItem(new Picture().Instantiate());
            room.AddItem(new TV().Instantiate());

            return room;
        }

        #endregion
    }
}
