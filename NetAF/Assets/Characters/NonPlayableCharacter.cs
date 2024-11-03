using NetAF.Assets.Interaction;
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
        /// <param name="identifier">This NonPlayableCharacter's identifier.</param>
        /// <param name="description">The description of this NonPlayableCharacter.</param>
        /// <param name="conversation">The conversation.</param>
        /// <param name="interaction">Specify the interaction.</param>
        public NonPlayableCharacter(string identifier, string description, Conversation conversation = null, InteractionCallback interaction = null) : this(new Identifier(identifier), new Description(description), conversation, interaction)
        {
        }

        /// <summary>
        /// Initializes a new instance of the NonPlayableCharacter class.
        /// </summary>
        /// <param name="identifier">This NonPlayableCharacter's identifier.</param>
        /// <param name="description">The description of this NonPlayableCharacter.</param>
        /// <param name="conversation">The conversation.</param>
        /// <param name="interaction">Specify the interaction.</param>
        public NonPlayableCharacter(Identifier identifier, Description description, Conversation conversation = null, InteractionCallback interaction = null) 
        {
            Identifier = identifier;
            Description = description;
            Conversation = conversation;
            Interaction = interaction ?? (i => new(InteractionEffect.NoEffect, i));
        }

        /// <summary>
        /// Initializes a new instance of the NonPlayableCharacter class.
        /// </summary>
        /// <param name="identifier">This NonPlayableCharacter's identifier.</param>
        /// <param name="description">The description of this NonPlayableCharacter.</param>
        /// <param name="conversation">The conversation.</param>
        /// <param name="isAlive">Set if this NonPlayableCharacter is alive.</param>
        /// <param name="interaction">Specify the interaction.</param>
        public NonPlayableCharacter(Identifier identifier, Description description, Conversation conversation, bool isAlive, InteractionCallback interaction = null) : this(identifier, description, conversation, interaction)
        {
            IsAlive = isAlive;
            Interaction = interaction;
        }

        /// <summary>
        /// Initializes a new instance of the NonPlayableCharacter class.
        /// </summary>
        /// <param name="identifier">This NonPlayableCharacter's identifier.</param>
        /// <param name="description">The description of this NonPlayableCharacter.</param>
        /// <param name="conversation">The conversation.</param>
        /// <param name="isAlive">Set if this NonPlayableCharacter is alive.</param>
        /// <param name="examination">Set this NonPlayableCharacter's examination.</param>
        /// <param name="interaction">Specify the interaction.</param>
        public NonPlayableCharacter(Identifier identifier, Description description, Conversation conversation, bool isAlive, ExaminationCallback examination, InteractionCallback interaction = null) : this(identifier, description, conversation, isAlive, interaction)
        {
            Examination = examination;
        }

        #endregion

        #region Implementation of IConverser

        /// <summary>
        /// Get or set the conversation.
        /// </summary>
        public Conversation Conversation { get; set; }

        #endregion

        #region Implementation of IRestoreFromObjectSerialization<NonPlayableCharacterSerialization>

        /// <summary>
        /// Restore this object from a serialization.
        /// </summary>
        /// <param name="serialization">The serialization to restore from.</param>
        public void RestoreFrom(NonPlayableCharacterSerialization serialization)
        {
            base.RestoreFrom(serialization);

            Conversation?.RestoreFrom(serialization.Conversation);
        }

        #endregion
    }
}