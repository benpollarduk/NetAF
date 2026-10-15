using NetAF.Assets;
using NetAF.Utilities;

namespace NetAF.Example.Lighthouse.Assets.Items
{
    /// <summary>
    /// The rusted key that opens the boathouse. Hidden beneath the coiled rope.
    /// </summary>
    public class RustedKey : IAssetTemplate<Item>
    {
        #region Constants

        internal const string Name = "Rusted Key";
        private const string Description = "A stubby iron key, orange with rust but still solid. A scrap of twine is knotted through its bow.";

        #endregion

        #region Implementation of IAssetTemplate<Item>

        /// <inheritdoc/>
        public Item Instantiate()
        {
            return new Item(Name, Description, true);
        }

        #endregion
    }
}
