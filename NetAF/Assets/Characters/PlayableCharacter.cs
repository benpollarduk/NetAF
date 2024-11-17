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
        /// <param name="examination">The examination.</param>
        public PlayableCharacter(string identifier, string description, Item[] items = null, CustomCommand[] commands = null, InteractionCallback interaction = null, ExaminationCallback examination = null) : this(new Identifier(identifier), new Description(description), items, commands, interaction, examination)
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
        /// <param name="examination">The examination.</param>
        public PlayableCharacter(Identifier identifier, Description description, Item[] items = null, CustomCommand[] commands = null, InteractionCallback interaction = null, ExaminationCallback examination = null) : this(identifier, description, true, items, commands, interaction, examination)
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
        /// <param name="examination">The examination.</param>
        public PlayableCharacter(string identifier, string description, bool canConverse, Item[] items = null, CustomCommand[] commands = null, InteractionCallback interaction = null, ExaminationCallback examination = null) : this(new Identifier(identifier), new Description(description), canConverse, items, commands, interaction, examination)
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
        /// <param name="examination">The examination.</param>
        public PlayableCharacter(Identifier identifier, Description description, bool canConverse, Item[] items = null, CustomCommand[] commands = null, InteractionCallback interaction = null, ExaminationCallback examination = null)
        {
            Identifier = identifier;
            Description = description;
            CanConverse = canConverse;
            Items = items ?? [];
            Commands = commands ?? [];
            Interaction = interaction ?? (i => new(InteractionResult.NeitherItemOrTargetExpired, i));

            if (examination != null)
                Examination = examination;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Use an item.
        /// </summary>
        /// <param name="item">The item to use.</param>
        /// <param name="targetObject">A target object to use the item on.</param>
        /// <returns>The interaction.</returns>
        public Interaction UseItem(Item item, IInteractWithItem targetObject)
        {
            return targetObject.Interact(item);
        }

        #endregion
    }
}