namespace NetAF.Assets
{
    /// <summary>
    /// Represents any object that is a container of items.
    /// </summary>
    public interface IItemContainer
    {
        /// <summary>
        /// Get the items.
        /// </summary>
        Item[] Items { get; }

        /// <summary>
        /// Add an item.
        /// </summary>
        /// <param name="item">The item to add.</param>
        void AddItem(Item item);

        /// <summary>
        /// Remove an item.
        /// </summary>
        /// <param name="item">The item to remove.</param>
        void RemoveItem(Item item);
    }
}
