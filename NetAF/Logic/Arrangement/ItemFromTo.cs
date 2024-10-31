using NetAF.Assets;

namespace NetAF.Logic.Arrangement
{
    /// <summary>
    /// Provides a record of where an item is moving from and to.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="from">The items from location.</param>
    /// <param name="to">The items to location.</param>
    internal class ItemFromTo(Item item, IItemContainer from, IItemContainer to)
    {
        #region Properties

        /// <summary>
        /// Get the item.
        /// </summary>
        public Item Item { get; private set; } = item;

        /// <summary>
        /// Get the from location.
        /// </summary>
        public IItemContainer From { get; private set; } = from;

        /// <summary>
        /// Get the to location.
        /// </summary>
        public IItemContainer To { get; private set; } = to;

        #endregion
    }
}
