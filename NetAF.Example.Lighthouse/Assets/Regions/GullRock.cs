using NetAF.Assets.Locations;
using NetAF.Example.Lighthouse.Assets.Rooms;
using NetAF.Utilities;

namespace NetAF.Example.Lighthouse.Assets.Regions
{
    /// <summary>
    /// The Gull Rock Lighthouse region, made up of the five rooms of the adventure.
    /// </summary>
    public class GullRock : IAssetTemplate<Region>
    {
        #region Constants

        private const string Name = "Gull Rock Lighthouse";
        private const string Description = "A storm-lashed rock in the sound, crowned by a dark lighthouse.";

        #endregion

        #region Implementation of IAssetTemplate<Region>

        /// <inheritdoc/>
        public Region Instantiate()
        {
            var regionMaker = new RegionMaker(Name, Description)
            {
                [0, 0, 0] = new LandingJetty().Instantiate(),
                [0, 1, 0] = new KeepersCottage().Instantiate(),
                [1, 0, 0] = new Boathouse().Instantiate(),
                [0, 0, 1] = new SpiralStairwell().Instantiate(),
                [0, 0, 2] = new LampRoom().Instantiate()
            };

            return regionMaker.Make(0, 0, 0);
        }

        #endregion
    }
}
