using NetAF.Assets.Locations;
using NetAF.Utilities;

namespace NetAF.Example.Assets.Regions.Everglades.Rooms
{
    public class CaveMouth : IAssetTemplate<Room>
    {
        #region Constants

        private const string Name = "Cave Mouth";
        private const string Description = "A cave mouth looms in front of you to the north. You can hear the sound of the ocean coming from the west.";

        #endregion

        #region Implementation of IAssetTemplate<Room>

        /// <summary>
        /// Instantiate a new instance of the asset.
        /// </summary>
        /// <returns>The asset.</returns>
        public Room Instantiate()
        {
            return new(Name, Description, [new Exit(Direction.North), new Exit(Direction.South), new Exit(Direction.West)]);
        }

        #endregion
    }
}
