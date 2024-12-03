using NetAF.Assets.Characters;
using System.Linq;

namespace NetAF.Serialization.Assets
{
    /// <summary>
    /// Represents a serialization of a Character.
    /// </summary>>
    public class CharacterSerialization : ExaminableSerialization, IObjectSerialization<Character>
    {
        #region Properties

        /// <summary>
        /// Get or set the item serializations.
        /// </summary>
        public ItemSerialization[] Items { get; set; }

        /// <summary>
        /// Get or set if the character is alive.
        /// </summary>
        public bool IsAlive { get; set; }

        #endregion

        #region StaticMethods

        /// <summary>
        /// Create a new serialization from a Character.
        /// </summary>
        /// <param name="character">The Character to create the serialization from.</param>
        /// <returns>The serialization.</returns>
        public static CharacterSerialization FromCharacter(Character character)
        {
            return new()
            {
                Identifier = character?.Identifier?.IdentifiableName,
                IsPlayerVisible = character?.IsPlayerVisible ?? false,
                AttributeManager = AttributeManagerSerialization.FromAttributeManager(character?.Attributes),
                Commands = character?.Commands?.Select(CustomCommandSerialization.FromCustomCommand).ToArray() ?? [],
                Items = character?.Items?.Select(ItemSerialization.FromItem).ToArray() ?? [],
                IsAlive = character?.IsAlive ?? false
            };
        }

        #endregion

        #region Implementation of IObjectSerialization<Character>

        /// <summary>
        /// Restore an instance from this serialization.
        /// </summary>
        /// <param name="character">The character to restore.</param>
        public void Restore(Character character)
        {
            ((IRestoreFromObjectSerialization<CharacterSerialization>)character).RestoreFrom(this);
        }

        #endregion
    }
}
