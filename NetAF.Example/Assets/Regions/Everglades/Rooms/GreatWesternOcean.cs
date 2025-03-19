using NetAF.Assets.Locations;
using NetAF.Example.Assets.Regions.Everglades.Items;
using NetAF.Utilities;

namespace NetAF.Example.Assets.Regions.Everglades.Rooms
{
    public class GreatWesternOcean : IAssetTemplate<Room>
    {
        #region Constants

        private const string Name = "Great Western Ocean";
        private const string Description = "The Great Western Ocean stretches to the horizon. The shore runs to the north and south. You can hear the lobstrosities clicking hungrily. To the east is a small clearing.";

        #endregion

        #region Implementation of IAssetTemplate<Room>

        /// <summary>
        /// Instantiate a new instance of the asset.
        /// </summary>
        /// <returns>The asset.</returns>
        public Room Instantiate()
        {
            var room = new Room(Name, Description, [new Exit(Direction.East)]);
            room.AddItem(new ConchShell().Instantiate());
            return room;
        }

        #endregion
    }
}
