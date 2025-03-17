using NetAF.Assets;
using NetAF.Utilities;

namespace NetAF.Example.Assets.Regions.Zelda.Items
{
    public class Sword : IAssetTemplate<Item>
    {
        #region Constants

        internal const string Name = "Sword";
        private const string Description = "A small sword handed down by the Kokiri. It has a wooden handle but the blade is sharp.";

        #endregion

        #region Implementation of IAssetTemplate<Item>

        /// <summary>
        /// Instantiate a new instance of the asset.
        /// </summary>
        /// <returns>The item.</returns>
        public Item Instantiate()
        {
            return new(Name, Description, true);
        }

        #endregion
    }
}
