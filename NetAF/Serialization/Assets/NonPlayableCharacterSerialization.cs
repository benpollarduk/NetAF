using NetAF.Assets.Characters;

namespace NetAF.Serialization.Assets
{
    /// <summary>
    /// Represents a serialization of a NonPlayableCharacter.
    /// </summary>
    /// <param name="character">The character to serialize.</param>
    public sealed class NonPlayableCharacterSerialization(NonPlayableCharacter character) : CharacterSerialization(character), IObjectSerialization<NonPlayableCharacter>
    {
        #region Properties

        /// <summary>
        /// Get the conversation serialization.
        /// </summary>
        public readonly ConversationSerialization Conversation = new(character.Conversation);

        #endregion

        #region Implementation of IObjectSerialization<NonPlayableCharacter>

        /// <summary>
        /// Restore an instance from this serialization.
        /// </summary>
        /// <param name="character">The character to restore.</param>
        public void Restore(NonPlayableCharacter character)
        {
            character.RestoreFrom(this);
        }

        #endregion
    }
}
