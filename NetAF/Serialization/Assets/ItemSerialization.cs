using NetAF.Assets;
using System.Linq;

namespace NetAF.Serialization.Assets
{
    /// <summary>
    /// Represents a serialization of an Item.
    /// </summary>
    public sealed class ItemSerialization : ExaminableSerialization, IObjectSerialization<Item>
    {
        #region StaticMethods

        /// <summary>
        /// Create a new serialization from an Item.
        /// </summary>
        /// <param name="item">The Item to create the serialization from.</param>
        /// <returns>The serialization.</returns>
        public static ItemSerialization FromItem(Item item)
        {
            return new()
            {
                Identifier = item?.Identifier?.IdentifiableName,
                IsPlayerVisible = item?.IsPlayerVisible ?? false,
                AttributeManager = AttributeManagerSerialization.FromAttributeManager(item?.Attributes),
                Commands = item?.Commands?.Select(CustomCommandSerialization.FromCustomCommand).ToArray() ?? []
            };
        }

        #endregion

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
