using NetAF.Assets;
using NetAF.Utilities;

namespace NetAF.Example.Assets.Regions.Flat.Items
{
    public class GameCube : IAssetTemplate<Item>
    {
        #region Constants

        internal const string Name = "GameCube";
        private const string Description = "A Nintendo Gamecube.You pop the disk cover, it looks like someone has been playing Killer7.";

        #endregion

        #region Implementation of IAssetTemplate<Item>

        /// <summary>
        /// Instantiate a new instance of the asset.
        /// </summary>
        /// <returns>The item.</returns>
        public Item Instantiate()
        {
            return new(Name, Description);
        }

        #endregion
    }
}
