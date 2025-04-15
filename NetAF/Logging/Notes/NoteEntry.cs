using NetAF.Serialization;
using NetAF.Serialization.Assets;

namespace NetAF.Logging.Notes
{
    /// <summary>
    /// Provides a note log entry.
    /// </summary>
    /// <param name="name">The name of the entry.</param>
    /// <param name="content">The content of the entry.</param>
    public class NoteEntry(string name, string content) : IRestoreFromObjectSerialization<NoteEntrySerialization>
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
        /// Create a new instance of NoteEntry from a serialization.
        /// </summary>
        /// <param name="serialization">The serialization.</param>
        /// <returns>The log entry.</returns>
        public static NoteEntry FromSerialization(NoteEntrySerialization serialization)
        {
            NoteEntry entry = new(string.Empty, string.Empty);
            ((IRestoreFromObjectSerialization<NoteEntrySerialization>)entry).RestoreFrom(serialization);
            return entry;
        }

        #endregion

        #region Implementation of IRestoreFromObjectSerialization<NoteEntrySerialization>

        /// <summary>
        /// Restore this object from a serialization.
        /// </summary>
        /// <param name="serialization">The serialization to restore from.</param>
        void IRestoreFromObjectSerialization<NoteEntrySerialization>.RestoreFrom(NoteEntrySerialization serialization)
        {
            Name = serialization.Name;
            Content = serialization.Content;
            HasExpired = serialization.HasExpired;
        }

        #endregion
    }
}
