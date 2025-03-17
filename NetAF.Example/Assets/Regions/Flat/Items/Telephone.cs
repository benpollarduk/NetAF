using NetAF.Assets;
using NetAF.Utilities;

namespace NetAF.Example.Assets.Regions.Flat.Items
{
    public class Telephone : IAssetTemplate<Item>
    {
        #region Constants

        internal const string Name = "TV";
        private const string Description = "As soon as you pickup the telephone to examine it you hear hideous feedback. You replace it quickly!";

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
