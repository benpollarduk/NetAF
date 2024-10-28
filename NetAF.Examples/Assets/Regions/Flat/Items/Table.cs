using NetAF.Assets;
using NetAF.Utilities;

namespace NetAF.Examples.Assets.Regions.Flat.Items
{
    public class Table : IAssetTemplate<Item>
    {
        #region Constants

        internal const string Name = "Table";
        private const string Description = "The coffee table is one of those large oblong ones. It is made of reconstituted wood, made to look like birch.";

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
