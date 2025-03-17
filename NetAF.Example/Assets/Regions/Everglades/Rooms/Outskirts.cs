using NetAF.Assets.Locations;
using NetAF.Utilities;


namespace NetAF.Example.Assets.Regions.Everglades.Rooms
{
    public class Outskirts : IAssetTemplate<Room>
    {
        #region Constants

        private const string Name = "Outskirts";
        private const string Description = "A vast chasm falls away before you.";

        #endregion

        #region Implementation of IAssetTemplate<Room>

        /// <summary>
        /// Instantiate a new instance of the asset.
        /// </summary>
        /// <returns>The asset.</returns>
        public Room Instantiate()
        {
            return new(Name, Description, [new Exit(Direction.South)]);
        }

        #endregion
    }
}
