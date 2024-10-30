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
        /// Get the item serializations.
        /// </summary>
        public readonly ItemSerialization[] Items = character?.Items?.Select(x => new ItemSerialization(x))?.ToArray() ?? [];

        /// <summary>
        /// Get if the character is alive.
        /// </summary>
        public readonly bool IsAlive = character.IsAlive;

        #endregion

        #region Implementation of IObjectSerialization<Character>

        /// <summary>
        /// Restore an instance from this serialization.
        /// </summary>
        /// <param name="character">The character to restore.</param>
        public void Restore(Character character)
        {
            //character.RestoreFrom(this);
        }

        #endregion
    }
}
