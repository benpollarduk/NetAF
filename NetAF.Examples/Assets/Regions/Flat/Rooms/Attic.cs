using NetAF.Assets.Locations;
using NetAF.Utilities;

namespace NetAF.Examples.Assets.Regions.Flat.Rooms
{
    internal class Attic : IAssetTemplate<Room>
    {
        #region Constants

        private const string Name = "Attic";
        private const string Description = "You are in the Attic. Even though there aren't many boxes up here the lack of light makes it pretty creepy.";

        #endregion

        #region Implementation of IAssetTemplate<Room>

        /// <summary>
        /// Instantiate a new instance of the asset.
        /// </summary>
        /// <returns>The asset.</returns>
        public Room Instantiate()
        {
            return new(Name, Description, new Exit(Direction.Down));
        }

        #endregion
    }
}
