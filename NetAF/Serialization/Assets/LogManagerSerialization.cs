using NetAF.Log;
using System.Collections.Generic;
using System.Linq;

namespace NetAF.Serialization.Assets
{
    /// <summary>
    /// Represents a serialization of a LogManager.
    /// </summary>
    public sealed class LogManagerSerialization : IObjectSerialization<LogManager>
    {
        #region Properties

        /// <summary>
        /// Get or set the entries.
        /// </summary>
        public List<LogEntrySerialization> Entries { get; set; }

        #endregion

        #region StaticMethods

        /// <summary>
        /// Create a new serialization from an LogManager.
        /// </summary>
        /// <param name="logManager">The LogManager to create the serialization from.</param>
        /// <returns>The serialization.</returns>
        public static LogManagerSerialization FromLogManager(LogManager logManager)
        {
            return new()
            {
                Entries = logManager.GetAll()?.Select(LogEntrySerialization.FromLogEntry).ToList() ?? []
            };
        }

        #endregion

        #region Implementation of IObjectSerialization<LogManager>

        /// <summary>
        /// Restore an instance from this serialization.
        /// </summary>
        /// <param name="logManager">The log manager to restore.</param>
        void IObjectSerialization<LogManager>.Restore(LogManager logManager)
        {
            ((IRestoreFromObjectSerialization<LogManagerSerialization>)logManager).RestoreFrom(this);
        }

        #endregion
    }
}
