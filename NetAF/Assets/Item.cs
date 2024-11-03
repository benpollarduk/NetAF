using NetAF.Assets.Interaction;
using NetAF.Serialization;
using NetAF.Serialization.Assets;

namespace NetAF.Assets
{
    /// <summary>
    /// Represents an item that can be used within the game.
    /// </summary>
    public sealed class Item : ExaminableObject, IInteractWithItem, IRestoreFromObjectSerialization<ItemSerialization>
    {
        #region Properties

        /// <summary>
        /// Get or set if this is takeable.
        /// </summary>
        public bool IsTakeable { get; private set; }

        /// <summary>
        /// Get the interaction.
        /// </summary>
        public InteractionCallback Interaction { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Item class.
        /// </summary>
        /// <param name="identifier">This Items identifier.</param>
        /// <param name="description">A description of this Item.</param>
        /// <param name="isTakeable">Specify if this item is takeable.</param>
        /// <param name="interaction">Specify the interaction.</param>
        public Item(string identifier, string description, bool isTakeable = false, InteractionCallback interaction = null) : this(new Identifier(identifier), new Description(description), isTakeable, interaction)
        {
        }

        /// <summary>
        /// Initializes a new instance of the Item class.
        /// </summary>
        /// <param name="identifier">This Items identifier.</param>
        /// <param name="description">A description of this Item.</param>
        /// <param name="isTakeable">Specify if this item is takeable.</param>
        /// <param name="interaction">Specify the interaction.</param>
        public Item(Identifier identifier, Description description, bool isTakeable = false, InteractionCallback interaction = null)
        {
            Identifier = identifier;
            Description = description;
            IsTakeable = isTakeable;
            Interaction = interaction ?? (i => new(InteractionEffect.NoEffect, i));
        }

        #endregion

        #region IInteractWithItem Members

        /// <summary>
        /// Interact with an item.
        /// </summary>
        /// <param name="item">The item to interact with.</param>
        /// <returns>The result of the interaction.</returns>
        public InteractionResult Interact(Item item)
        {
            return Interaction.Invoke(item);
        }

        #endregion

        #region Implementation of IRestoreFromObjectSerialization<ItemSerialization>

        /// <summary>
        /// Restore this object from a serialization.
        /// </summary>
        /// <param name="serialization">The serialization to restore from.</param>
        public void RestoreFrom(ItemSerialization serialization)
        {
            base.RestoreFrom(serialization);
        }

        #endregion
    }
}