using NetAF.Assets;
using NetAF.Utilities;

namespace NetAF.Example.Assets.Items
{
    public class Knife : IAssetTemplate<Item>
    {
        #region Constants

        internal const string Name = "Knife";
        private const string Description = "A small pocket knife.";

        #endregion

        #region Implementation of IAssetTemplate<out Item>

        /// <inheritdoc/>
        public Item Instantiate()
        {
            return new(Name, Description, true);
        }

        #endregion
    }
}
