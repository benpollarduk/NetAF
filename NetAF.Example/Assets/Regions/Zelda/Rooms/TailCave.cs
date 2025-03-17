using NetAF.Assets.Locations;
using NetAF.Utilities;

namespace NetAF.Example.Assets.Regions.Zelda.Rooms
{
    public class TailCave : IAssetTemplate<Room>
    {
        #region Constants

        internal const string Name = "Tail Cave";
        private const string Description = "The cave is dark, and currently very empty. Quite shabby really, not like the cave on Koholint at all...";

        #endregion

        #region Implementation of IAssetTemplate<Room>

        /// <summary>
        /// Instantiate a new instance of the asset.
        /// </summary>
        /// <returns>The asset.</returns>
        public Room Instantiate()
        {
            return new(Name, Description, [new Exit(Direction.West)]);
        }

        #endregion
    }
}
