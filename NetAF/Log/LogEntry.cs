using NetAF.Serialization;
using NetAF.Serialization.Assets;

namespace NetAF.Log
{
    /// <summary>
    /// Provides a log entry.
    /// </summary>
    /// <param name="name">The name of the entry.</param>
    /// <param name="content">The content of the entry.</param>
    public class LogEntry(string name, string content) : IRestoreFromObjectSerialization<LogEntrySerialization>
    {
        #region Properties

        /// <summary>
        /// Get the name of this entry.
        /// </summary>
        public string Name { get; private set; } = name;

        /// <summary>
        /// Get the content of this entry.
        /// </summary>
        public string Content { get; private set; } = content;

        /// <summary>
        /// Get if this entry has expired.
        /// </summary>
        public bool HasExpired { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// Mark this entry as expired.
        /// </summary>
        public void Expire()
        {
            HasExpired = true;
        }

        #endregion

        #region StaticMethods

        /// <summary>
        /// Create a new instance of LogEntry from a serialization.
        /// </summary>
        /// <param name="serialization">The serialization.</param>
        /// <returns>The log entry.</returns>
        public static LogEntry FromSerialization(LogEntrySerialization serialization)
        {
            LogEntry entry = new(string.Empty, string.Empty);
            ((IRestoreFromObjectSerialization<LogEntrySerialization>)entry).RestoreFrom(serialization);
            return entry;
        }

        #endregion

        #region Implementation of IRestoreFromObjectSerialization<LogEntrySerialization>

        /// <summary>
        /// Restore this object from a serialization.
        /// </summary>
        /// <param name="serialization">The serialization to restore from.</param>
        void IRestoreFromObjectSerialization<LogEntrySerialization>.RestoreFrom(LogEntrySerialization serialization)
        {
            Name = serialization.Name;
            Content = serialization.Content;
            HasExpired = serialization.HasExpired;
        }

        #endregion
    }
}
