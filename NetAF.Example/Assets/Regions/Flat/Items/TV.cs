using NetAF.Assets;
using NetAF.Utilities;

namespace NetAF.Example.Assets.Regions.Flat.Items
{
    public class TV : IAssetTemplate<Item>
    {
        #region Constants

        internal const string Name = "TV";
        private const string Description = "The TV is small - the screen is only 14\"! Two DVD's are propped alongside it 'Miranda' and 'The Vicar Of Dibly'.";

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
