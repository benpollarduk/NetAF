using NetAF.Assets.Locations;
using NetAF.Utilities;

namespace NetAF.Examples.Assets.Regions.Everglades.Rooms
{
    internal class ForestEntrance : IAssetTemplate<Room>
    {
        #region Constants

        private const string Name = "Forest Entrance";
        private const string Description = "You are standing on the edge of a beautiful forest. There is a parting in the trees to the north.";

        #endregion

        #region Implementation of IAssetTemplate<Room>

        /// <summary>
        /// Instantiate a new instance of the asset.
        /// </summary>
        /// <returns>The asset.</returns>
        public Room Instantiate()
        {
            return new(Name, Description, [new Exit(Direction.North)]);
        }

        #endregion
    }
}
