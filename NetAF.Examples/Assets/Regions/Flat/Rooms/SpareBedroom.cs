using NetAF.Assets;
using NetAF.Assets.Locations;
using NetAF.Examples.Assets.Regions.Flat.Items;
using NetAF.Utilities;

namespace NetAF.Examples.Assets.Regions.Flat.Rooms
{
    internal class SpareBedroom(InteractionCallback Interaction) : IAssetTemplate<Room>
    {
        #region Constants

        private const string Name = "Spare Bedroom";

        #endregion

        #region Implementation of IAssetTemplate<Room>

        /// <summary>
        /// Instantiate a new instance of the asset.
        /// </summary>
        /// <returns>The asset.</returns>
        public Room Instantiate()
        {
            Room room = null;

            ConditionalDescription description = new("You are in a very tidy room. The eastern wall is painted in a dark red colour. Against the south wall is a line of guitar amplifiers, all turned on. A very tidy blue guitar rests against the amps just begging to be played. There is a Gamecube against the northern wall. A doorway to the north leads back to the Western Hallway.",
                                                     "You are in a very tidy room. The eastern wall is painted in a dark red colour. Against the south wall is a line of guitar amplifiers, all turned on. There is a Gamecube against the northern wall. A doorway to the north leads back to the Western Hallway.",
                                                     () => room.ContainsItem(Guitar.Name));

            room = new(new Identifier(Name), description, [new Exit(Direction.North)], interaction: Interaction);

            room.AddItem(new GameCube().Instantiate());
            room.AddItem(new Guitar().Instantiate());

            return room;
        }

        #endregion
    }
}
