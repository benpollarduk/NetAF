using System;
using NetAF.Assets.Interaction;

namespace NetAF.Assets.Characters
{
    /// <summary>
    /// Represents a playable character.
    /// </summary>
    public sealed class PlayableCharacter : Character
    {
        #region Properties

        /// <summary>
        /// Get if this playable character can converse with an IConverser.
        /// </summary>
        public bool CanConverse { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the PlayableCharacter class.
        /// </summary>
        /// <param name="identifier">This PlayableCharacter's identifier.</param>
        /// <param name="description">The description of the player.</param>
        /// <param name="items">The players items.</param>
        public PlayableCharacter(string identifier, string description, params Item[] items) : this(new Identifier(identifier), new Description(description), items)
        {
        }

        /// <summary>
        /// Initializes a new instance of the PlayableCharacter class.
        /// </summary>
        /// <param name="identifier">This PlayableCharacter's identifier.</param>
        /// <param name="description">The description of the player.</param>
        /// <param name="items">The players items.</param>
        public PlayableCharacter(Identifier identifier, Description description, params Item[] items) : this(identifier, description, true, items)
        {
        }

        /// <summary>
        /// Initializes a new instance of the PlayableCharacter class.
        /// </summary>
        /// <param name="identifier">This PlayableCharacter's identifier.</param>
        /// <param name="description">The description of the player.</param>
        /// <param name="canConverse">If this PlayableCharacter can converse with an IConverser.</param>
        /// <param name="items">The players items.</param>
        public PlayableCharacter(string identifier, string description, bool canConverse, params Item[] items) : this(new Identifier(identifier), new Description(description), canConverse, items)
        {
        }

        /// <summary>
        /// Initializes a new instance of the PlayableCharacter class.
        /// </summary>
        /// <param name="identifier">This PlayableCharacter's identifier.</param>
        /// <param name="description">The description of the player.</param>
        /// <param name="canConverse">If this PlayableCharacter can converse with an IConverser.</param>
        /// <param name="items">The players items.</param>
        public PlayableCharacter(Identifier identifier, Description description, bool canConverse, params Item[] items)
        {
            Identifier = identifier;
            Description = description;
            CanConverse = canConverse;
            Items = items ?? Array.Empty<Item>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Use an item.
        /// </summary>
        /// <param name="item">The item to use.</param>
        /// <param name="targetObject">A target object to use the item on.</param>
        /// <returns>The result of the items usage.</returns>
        public InteractionResult UseItem(Item item, IInteractWithItem targetObject)
        {
            var result = targetObject.Interact(item);

            if (result.Effect == InteractionEffect.FatalEffect)
                IsAlive = false;

            return result;
        }

        #endregion
    }
}