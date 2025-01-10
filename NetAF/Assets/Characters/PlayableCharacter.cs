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

        /// <summary>
        /// Get if this playable character can take and drop items.
        /// </summary>
        public bool CanTakeAndDropItems { get; private set; }

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
        public PlayableCharacter(Identifier identifier, IDescription description, Item[] items = null, CustomCommand[] commands = null, InteractionCallback interaction = null, ExaminationCallback examination = null) : this(identifier, description, true, true, items, commands, interaction, examination)
        {
        }

        /// <summary>
        /// Initializes a new instance of the PlayableCharacter class.
        /// </summary>
        /// <param name="identifier">The identifier.</param>
        /// <param name="description">The description.</param>
        /// <param name="canConverse">If this playable character can converse with an IConverser.</param>
        /// <param name="canTakeAndDropItems">If this playable character can take and drop items.</param>
        /// <param name="items">The items.</param>
        /// <param name="commands">This objects commands.</param>
        /// <param name="interaction">The interaction.</param>
        /// <param name="examination">The examination.</param>
        public PlayableCharacter(string identifier, string description, bool canConverse, bool canTakeAndDropItems = true, Item[] items = null, CustomCommand[] commands = null, InteractionCallback interaction = null, ExaminationCallback examination = null) : this(new Identifier(identifier), new Description(description), canConverse, canTakeAndDropItems, items, commands, interaction, examination)
        {
        }

        /// <summary>
        /// Initializes a new instance of the PlayableCharacter class.
        /// </summary>
        /// <param name="identifier">The identifier.</param>
        /// <param name="description">The description.</param>
        /// <param name="canConverse">If this playable character can converse with an IConverser.</param>
        /// <param name="canTakeAndDropItems">If this playable character can take and drop items.</param>
        /// <param name="items">The items.</param>
        /// <param name="commands">This objects commands.</param>
        /// <param name="interaction">The interaction.</param>
        /// <param name="examination">The examination.</param>
        public PlayableCharacter(Identifier identifier, IDescription description, bool canConverse, bool canTakeAndDropItems = true, Item[] items = null, CustomCommand[] commands = null, InteractionCallback interaction = null, ExaminationCallback examination = null)
        {
            Identifier = identifier;
            Description = description;
            CanConverse = canConverse;
            CanTakeAndDropItems = canTakeAndDropItems;
            Items = items ?? [];
            Commands = commands ?? [];
            Interaction = interaction ?? (i => new(InteractionResult.NoChange, i));
            Examination = examination ?? DefaultExamination;
        }

        #endregion
    }
}