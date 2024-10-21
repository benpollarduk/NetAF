using NetAF.Assets.Locations;
using NetAF.Examples.Assets.Regions.Hub.Rooms;
using NetAF.Utilities;

namespace NetAF.Examples.Assets.Regions.Hub
{
    internal class Hub : IAssetTemplate<Region>
    {
        #region Constants

        private const string Name = "Jungle";
        private const string Description = "A dense jungle, somewhere tropical.";

        #endregion

        #region Implementation of IAssetTemplate<Region>

        /// <summary>
        /// Instantiate a new instance of the asset.
        /// </summary>
        /// <returns>The asset.</returns>
        public Region Instantiate()
        {
            var regionMaker = new RegionMaker(Name, Description)
            {
                [0, 0, 0] = new Clearing().Instantiate()
            };

            return regionMaker.Make(0, 0, 0);
        }

        #endregion
    }
}
