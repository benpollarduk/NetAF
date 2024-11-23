using NetAF.Assets.Characters;
using System.Linq;

namespace NetAF.Serialization.Assets
{
    /// <summary>
    /// Represents a serialization of a NonPlayableCharacter.
    /// </summary>
    public sealed class NonPlayableCharacterSerialization : CharacterSerialization, IObjectSerialization<NonPlayableCharacter>
    {
        #region Properties

        /// <summary>
        /// Get or set the conversation serialization.
        /// </summary>
        public ConversationSerialization Conversation { get; set; }

        #endregion

        #region StaticMethods

        /// <summary>
        /// Create a new serialization from a NonPlayableCharacter.
        /// </summary>
        /// <param name="nonPlayableCharacter">The NonPlayableCharacter to create the serialization from.</param>
        /// <returns>The serialization.</returns>
        public static NonPlayableCharacterSerialization FromNonPlayableCharacter(NonPlayableCharacter nonPlayableCharacter)
        {
            return new()
            {
                Identifier = nonPlayableCharacter?.Identifier?.IdentifiableName,
                IsPlayerVisible = nonPlayableCharacter?.IsPlayerVisible ?? false,
                AttributeManager = AttributeManagerSerialization.FromAttributeManager(nonPlayableCharacter?.Attributes),
                Commands = nonPlayableCharacter?.Commands?.Select(CustomCommandSerialization.FromCustomCommand).ToArray() ?? [],
                Items = nonPlayableCharacter?.Items?.Select(ItemSerialization.FromItem).ToArray() ?? [],
                IsAlive = nonPlayableCharacter?.IsAlive ?? false,
                Conversation = ConversationSerialization.FromConversation(nonPlayableCharacter?.Conversation)
            };
        }

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
