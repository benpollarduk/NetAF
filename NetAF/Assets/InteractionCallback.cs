namespace NetAF.Assets
{
    /// <summary>
    /// Represents the callback for interacting with objects.
    /// </summary>
    /// <param name="item">The item to interact with.</param>
    /// <returns>The interaction.</returns>
    public delegate Interaction InteractionCallback(Item item);
}