using NetAF.Log;

namespace NetAF.Serialization.Assets
{
    /// <summary>
    /// Represents a serialization of a LogEntry.
    /// </summary>
    public sealed class LogEntrySerialization : IObjectSerialization<LogEntry>
    {
        #region Properties

        /// <summary>
        /// Get or set the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Get or set the content.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Get or set if this has expired.
        /// </summary>
        public bool HasExpired { get; set; }

        #endregion

        #region StaticMethods

        /// <summary>
        /// Create a new serialization from a LogEntry.
        /// </summary>
        /// <param name="LogEntry">The LogEntry to create the serialization from.</param>
        /// <returns>The serialization.</returns>
        public static LogEntrySerialization FromLogEntry(LogEntry LogEntry)
        {
            return new()
            {
                Name = LogEntry.Name,
                Content = LogEntry.Content,
                HasExpired = LogEntry.HasExpired
            };
        }

        #endregion

        #region Implementation of IObjectSerialization<Exit>

        /// <summary>
        /// Restore an instance from this serialization.
        /// </summary>
        /// <param name="LogEntry">The LogEntry to restore.</param>
        void IObjectSerialization<LogEntry>.Restore(LogEntry LogEntry)
        {
            ((IRestoreFromObjectSerialization<LogEntrySerialization>)LogEntry).RestoreFrom(this);
        }

        #endregion
    }
}
