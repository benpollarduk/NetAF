using NetAF.Assets.Interaction;
using NetAF.Commands;

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
        /// <param name="identifier">The identifier.</param>
        /// <param name="description">The description.</param>
        /// <param name="items">The items.</param>
        /// <param name="commands">This objects commands.</param>
        /// <param name="interaction">The interaction.</param>
        public PlayableCharacter(string identifier, string description, Item[] items = null, CustomCommand[] commands = null, InteractionCallback interaction = null) : this(new Identifier(identifier), new Description(description), items, commands, interaction)
        {
        }

        /// <summary>
        /// Initializes a new instance of the PlayableCharacter class.
        /// </summary>
        /// <param name="identifier">The identifier.</param>
        /// <param name="description">The description.</param>
        /// <param name="items">The items.</param>
        /// <param name="commands">This objects commands.</param>
        /// <param name="interaction">The interaction.</param>
        public PlayableCharacter(Identifier identifier, Description description, Item[] items = null, CustomCommand[] commands = null, InteractionCallback interaction = null) : this(identifier, description, true, items, commands, interaction)
        {
        }

        /// <summary>
        /// Initializes a new instance of the PlayableCharacter class.
        /// </summary>
        /// <param name="identifier">The identifier.</param>
        /// <param name="description">The description.</param>
        /// <param name="canConverse">If this object can converse with an IConverser.</param>
        /// <param name="items">The items.</param>
        /// <param name="commands">This objects commands.</param>
        /// <param name="interaction">The interaction.</param>
        public PlayableCharacter(string identifier, string description, bool canConverse, Item[] items = null, CustomCommand[] commands = null, InteractionCallback interaction = null) : this(new Identifier(identifier), new Description(description), canConverse, items, commands, interaction)
        {
        }

        /// <summary>
        /// Initializes a new instance of the PlayableCharacter class.
        /// </summary>
        /// <param name="identifier">The identifier.</param>
        /// <param name="description">The description.</param>
        /// <param name="canConverse">If this object can converse with an IConverser.</param>
        /// <param name="items">The items.</param>
        /// <param name="commands">This objects commands.</param>
        /// <param name="interaction">The interaction.</param>
        public PlayableCharacter(Identifier identifier, Description description, bool canConverse, Item[] items = null, CustomCommand[] commands = null, InteractionCallback interaction = null)
        {
            Identifier = identifier;
            Description = description;
            CanConverse = canConverse;
            Items = items ?? [];
            Commands = commands ?? [];
            Interaction = interaction ?? (i => new(InteractionEffect.NoEffect, i));
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