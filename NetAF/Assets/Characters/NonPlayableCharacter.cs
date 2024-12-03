using NetAF.Commands;
using NetAF.Conversations;
using NetAF.Serialization;
using NetAF.Serialization.Assets;

namespace NetAF.Assets.Characters
{
    /// <summary>
    /// Represents a non-playable character.
    /// </summary>
    public sealed class NonPlayableCharacter : Character, IConverser, IRestoreFromObjectSerialization<NonPlayableCharacterSerialization>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the NonPlayableCharacter class.
        /// </summary>
        /// <param name="identifier">The identifier.</param>
        /// <param name="description">The description.</param>
        /// <param name="conversation">The conversation.</param>
        /// <param name="commands">This objects commands.</param>
        /// <param name="interaction">The interaction.</param>
        /// <param name="examination">The examination.</param>
        public NonPlayableCharacter(string identifier, string description, Conversation conversation = null, CustomCommand[] commands = null, InteractionCallback interaction = null, ExaminationCallback examination = null) : this(new Identifier(identifier), new Description(description), conversation, commands, interaction, examination)
        {
        }

        /// <summary>
        /// Initializes a new instance of the NonPlayableCharacter class.
        /// </summary>
        /// <param name="identifier">The identifier.</param>
        /// <param name="description">The description.</param>
        /// <param name="conversation">The conversation.</param>
        /// <param name="commands">This objects commands.</param>
        /// <param name="interaction">The interaction.</param>
        /// <param name="examination">The examination.</param>
        public NonPlayableCharacter(Identifier identifier, IDescription description, Conversation conversation = null, CustomCommand[] commands = null, InteractionCallback interaction = null, ExaminationCallback examination = null) 
        {
            Identifier = identifier;
            Description = description;
            Conversation = conversation;
            Commands = commands ?? [];
            Interaction = interaction ?? (i => new(InteractionResult.NoChange, i));
            Examination = examination ?? DefaultExamination;
        }

        /// <summary>
        /// Initializes a new instance of the NonPlayableCharacter class.
        /// </summary>
        /// <param name="identifier">The identifier.</param>
        /// <param name="description">The description.</param>
        /// <param name="isAlive">If this character is alive.</param>
        /// <param name="conversation">The conversation.</param>
        /// <param name="commands">This objects commands.</param>
        /// <param name="interaction">The interaction.</param>
        /// <param name="examination">The examination.</param>
        public NonPlayableCharacter(Identifier identifier, IDescription description, bool isAlive, Conversation conversation = null, CustomCommand[] commands = null, InteractionCallback interaction = null, ExaminationCallback examination = null) : this(identifier, description, conversation, commands, interaction, examination)
        {
            IsAlive = isAlive;
            Interaction = interaction;
        }

        #endregion

        #region Implementation of IConverser

        /// <summary>
        /// Get the conversation.
        /// </summary>
        public Conversation Conversation { get; }

        #endregion

        #region Implementation of IRestoreFromObjectSerialization<NonPlayableCharacterSerialization>

        /// <summary>
        /// Restore this object from a serialization.
        /// </summary>
        /// <param name="serialization">The serialization to restore from.</param>
        void IRestoreFromObjectSerialization<NonPlayableCharacterSerialization>.RestoreFrom(NonPlayableCharacterSerialization serialization)
        {
            ((IRestoreFromObjectSerialization<CharacterSerialization>)this).RestoreFrom(serialization);
            ((IRestoreFromObjectSerialization<ConversationSerialization>)Conversation)?.RestoreFrom(serialization.Conversation);
        }

        #endregion
    }
}