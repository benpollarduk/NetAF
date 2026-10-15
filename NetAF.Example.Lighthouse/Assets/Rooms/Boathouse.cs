using NetAF.Assets.Locations;
using NetAF.Example.Lighthouse.Assets.Items;
using NetAF.Utilities;

namespace NetAF.Example.Lighthouse.Assets.Rooms
{
    /// <summary>
    /// The boathouse. Holds the whale oil cask used to fill the oil can, and a dry box
    /// of matches - though the matches are damp from the crossing.
    /// </summary>
    public class Boathouse : IAssetTemplate<Room>
    {
        #region Constants

        internal const string Name = "Boathouse";
        private const string Description = "The boathouse reeks of oil and rotting net. An upturned skiff fills half the floor, and shelves of ships' stores line the walls. The only way out is west, back to the jetty.";

        #endregion

        #region Implementation of IAssetTemplate<Room>

        /// <inheritdoc/>
        public Room Instantiate()
        {
            var room = new Room(Name, Description, [new Exit(Direction.West)]);

            room.AddItem(new WhaleOil().Instantiate());
            room.AddItem(new Matches().Instantiate());

            return room;
        }

        #endregion
    }
}
