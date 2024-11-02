using NetAF.Assets.Characters;
using System.Linq;

namespace NetAF.Serialization.Assets
{
    /// <summary>
    /// Represents a serialization of a Character.
    /// </summary>
    /// <param name="character">The character to serialize.</param>
    public class CharacterSerialization(Character character) : ExaminableSerialization(character), IObjectSerialization<Character>
    {
        #region Properties

        /// <summary>
        /// Get or set the item serializations.
        /// </summary>
        public ItemSerialization[] Items { get; set; } = character?.Items?.Select(x => new ItemSerialization(x))?.ToArray() ?? [];

        /// <summary>
        /// Get or set if the character is alive.
        /// </summary>
        public bool IsAlive { get; set; } = character?.IsAlive ?? false;

        #endregion

        #region Implementation of IObjectSerialization<Character>

        /// <summary>
        /// Restore an instance from this serialization.
        /// </summary>
        /// <param name="character">The character to restore.</param>
        public void Restore(Character character)
        {
            character.RestoreFrom(this);
        }

        #endregion
    }
}
