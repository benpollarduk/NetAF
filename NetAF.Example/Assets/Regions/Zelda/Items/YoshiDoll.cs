using NetAF.Assets;
using NetAF.Utilities;

namespace NetAF.Example.Assets.Regions.Zelda.Items
{
    public class YoshiDoll : IAssetTemplate<Item>
    {
        #region Constants

        internal const string Name = "Yoshi Doll";
        private const string Description = "A small mechanical doll in the shape of Yoshi. Apparently these are all the rage on Koholint...";

        #endregion

        #region Implementation of IAssetTemplate<Item>

        /// <inheritdoc/>
        public Item Instantiate()
        {
            return new(Name, Description);
        }

        #endregion
    }
}
