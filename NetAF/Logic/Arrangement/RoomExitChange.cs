using NetAF.Assets.Locations;

namespace NetAF.Logic.Arrangement
{
    /// <summary>
    /// Provides a record of where a NonPlayableCharacter is moving from and to.
    /// </summary>
    /// <param name="room">The room.</param>
    /// <param name="exit">The exit.</param>
    /// <param name="change">The change.</param>
    internal class RoomExitChange(Room room, Exit exit, ExitChange change)
    {
        #region Properties

        /// <summary>
        /// Get the room.
        /// </summary>
        public Room Room { get; private set; } = room;

        /// <summary>
        /// Get the exit.
        /// </summary>
        public Exit Exit { get; private set; } = exit;

        /// <summary>
        /// Get the change.
        /// </summary>
        public ExitChange Change { get; private set; } = change;

        #endregion
    }
}
