using NetAF.Assets;
using NetAF.Utilities;

namespace NetAF.Examples.Assets.Regions.Zelda.Items
{
    public class Table : IAssetTemplate<Item>
    {
        #region Constants

        internal const string Name = "Table";
        private const string Description = "A small wooden table made from a slice of a trunk of a Deku tree. Pretty handy, but you can't take it with you.";

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
