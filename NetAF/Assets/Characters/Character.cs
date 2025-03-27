using System;
using System.Linq;
using NetAF.Extensions;
using NetAF.Serialization;
using NetAF.Serialization.Assets;

namespace NetAF.Assets.Characters
{
    /// <summary>
    /// Represents a generic in game character.
    /// </summary>
    public abstract class Character : ExaminableObject, IInteractWithItem, IItemContainer, IRestoreFromObjectSerialization<CharacterSerialization>
    {
        #region Properties

        /// <summary>
        /// Get if this character is alive.
        /// </summary>
        public bool IsAlive { get; protected set; } = true;

        /// <summary>
        /// Get the interaction.
        /// </summary>
        public InteractionCallback Interaction { get; protected set; }

        #endregion

        #region Methods

        /// <summary>
        /// Interact with a specified item.
        /// </summary>
        /// <param name="item">The item to interact with.</param>
        /// <returns>The interaction.</returns>
        protected virtual Interaction InteractWithItem(Item item)
        {
            return Interaction.Invoke(item);
        }

        /// <summary>
        /// Kill the character.
        /// </summary>
        public virtual void Kill()
        {
            IsAlive = false;
        }

        /// <summary>
        /// Determine if this PlayableCharacter has an item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="includeInvisibleItems">Specify if invisible items should be included.</param>
        /// <returns>True if the item is found, else false.</returns>
        public virtual bool HasItem(Item item, bool includeInvisibleItems = false)
        {
            return Items.Contains(item) && (includeInvisibleItems || item.IsPlayerVisible);
        }

        /// <summary>
        /// Find an item.
        /// </summary>
        /// <param name="itemName">The items name.</param>
        /// <param name="item">The item.</param>
        /// <param name="includeInvisibleItems">Specify if invisible items should be included.</param>
        /// <returns>True if the item was found.</returns>
        public virtual bool FindItem(string itemName, out Item item, bool includeInvisibleItems = false)
        {
            var items = Items.Where(x => x.Identifier.Equals(itemName) && (includeInvisibleItems || x.IsPlayerVisible)).ToArray();

            if (items.Length > 0)
            {
                item = items[0];
                return true;
            }

            item = null;
            return false;
        }

        /// <summary>
        /// Give an item to another in game Character.
        /// </summary>
        /// <param name="item">The item to give.</param>
        /// <param name="character">The Character to give the item to.</param>
        /// <returns>True if the transaction completed OK, else false.</returns>
        public virtual bool Give(Item item, Character character)
        {
            if (!HasItem(item))
                return false;
            
            RemoveItem(item);
            character.AddItem(item);
            return true;

        }

        #endregion

        #region Implementation of IInteractWithItem

        /// <summary>
        /// Interact with an item.
        /// </summary>
        /// <param name="item">The item to interact with.</param>
        /// <returns>The interaction.</returns>
        public Interaction Interact(Item item)
        {
            return InteractWithItem(item);
        }

        #endregion

        #region Implementation of IItemContainer

        /// <summary>
        /// Get the items.
        /// </summary>
        public Item[] Items { get; protected set; } = [];

        /// <summary>
        /// Add an item.
        /// </summary>
        /// <param name="item">The item to add.</param>
        public void AddItem(Item item)
        {
            Items = Items.Add(item);
        }

        /// <summary>
        /// Remove an item.
        /// </summary>
        /// <param name="item">The item to remove.</param>
        public void RemoveItem(Item item)
        {
            Items = Items.Remove(item);
        }

        #endregion

        #region Implementation of IRestoreFromObjectSerialization<CharacterSerialization>

        /// <summary>
        /// Restore this object from a serialization.
        /// </summary>
        /// <param name="serialization">The serialization to restore from.</param>
        void IRestoreFromObjectSerialization<CharacterSerialization>.RestoreFrom(CharacterSerialization serialization)
        {
            ((IRestoreFromObjectSerialization<ExaminableSerialization>)this).RestoreFrom(serialization);

            IsAlive = serialization.IsAlive;

            foreach (var item in Items)
            {
                var itemSerialization = Array.Find(serialization.Items, x => item.Identifier.Equals(x.Identifier));
                ((IObjectSerialization<Item>)itemSerialization)?.Restore(item);
            }
        }

        #endregion
    }
}