using NetAF.Assets.Characters;
using NetAF.Assets.Locations;

namespace NetAF.Logic.Arrangement
{
    /// <summary>
    /// Provides a record of where a NonPlayableCharacter is moving from and to.
    /// </summary>
    /// <param name="character">The character.</param>
    /// <param name="from">The character's from location.</param>
    /// <param name="to">The character's to location.</param>
    internal class NonPlayableCharacterFromTo(NonPlayableCharacter character, Room from, Room to)
    {
        #region Properties

        /// <summary>
        /// Get the character.
        /// </summary>
        public NonPlayableCharacter Character { get; private set; } = character;

        /// <summary>
        /// Get the from location.
        /// </summary>
        public Room From { get; private set; } = from;

        /// <summary>
        /// Get the to location.
        /// </summary>
        public Room To { get; private set; } = to;

        #endregion
    }
}
