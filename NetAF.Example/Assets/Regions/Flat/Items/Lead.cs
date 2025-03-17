using NetAF.Assets;
using NetAF.Utilities;

namespace NetAF.Example.Assets.Regions.Flat.Items
{
    public class Lead : IAssetTemplate<Item>
    {
        #region Constants

        internal const string Name = "Lead";
        private const string Description = "A 10m Venom instrument lead.";

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
