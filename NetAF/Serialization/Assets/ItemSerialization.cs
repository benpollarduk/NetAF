using NetAF.Assets;

namespace NetAF.Serialization.Assets
{
    /// <summary>
    /// Represents a serialization of an Item.
    /// </summary>
    /// <param name="item">The item to serialize.</param>
    public sealed class ItemSerialization(Item item) : ExaminableSerialization(item), IObjectSerialization<Item>
    {
        #region Implementation of IObjectSerialization<Item>

        /// <summary>
        /// Restore an instance from this serialization.
        /// </summary>
        /// <param name="item">The item to restore.</param>
        public void Restore(Item item)
        {
            item.RestoreFrom(this);
        }

        #endregion
    }
}
