namespace NetAF.Assets
{
    /// <summary>
    /// Represents any object that can interact with an item.
    /// </summary>
    public interface IInteractWithItem
    {
        /// <summary>
        /// Interact with an item.
        /// </summary>
        /// <param name="item">The item to interact with.</param>
        /// <returns>The interaction.</returns>
        Interaction Interact(Item item);
    }
}