using NetAF.Assets.Locations;

namespace NetAF.Serialization.Assets
{
    /// <summary>
    /// Represents a serialization of an Exit.
    /// </summary>
    /// <param name="exit">The exit to serialize.</param>
    public class ExitSerialization(Exit exit) : ExaminableSerialization(exit), IObjectSerialization<Exit>
    {
        #region Properties

        /// <summary>
        /// Get if the exit is locked.
        /// </summary>
        public readonly bool IsLocked = exit.IsLocked;

        #endregion

        #region Implementation of IObjectSerialization<Exit>

        /// <summary>
        /// Restore an instance from this serialization.
        /// </summary>
        /// <param name="exit">The exit to restore.</param>
        public void Restore(Exit exit)
        {
            exit.RestoreFrom(this);
        }

        #endregion
    }
}
