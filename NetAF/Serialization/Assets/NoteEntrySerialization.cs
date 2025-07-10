using NetAF.Logging.Notes;

namespace NetAF.Serialization.Assets
{
    /// <summary>
    /// Represents a serialization of a NoteEntry.
    /// </summary>
    public sealed class NoteEntrySerialization : IObjectSerialization<NoteEntry>
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
        /// Create a new serialization from a NoteEntry.
        /// </summary>
        /// <param name="noteEntry">The NoteEntry to create the serialization from.</param>
        /// <returns>The serialization.</returns>
        public static NoteEntrySerialization FromNoteEntry(NoteEntry noteEntry)
        {
            return new()
            {
                Name = noteEntry.Name,
                Content = noteEntry.Content,
                HasExpired = noteEntry.HasExpired
            };
        }

        #endregion

        #region Implementation of IObjectSerialization<NoteEntry>

        /// <summary>
        /// Restore an instance from this serialization.
        /// </summary>
        /// <param name="noteEntry">The NoteEntry to restore.</param>
        void IObjectSerialization<NoteEntry>.Restore(NoteEntry noteEntry)
        {
            ((IRestoreFromObjectSerialization<NoteEntrySerialization>)noteEntry).RestoreFrom(this);
        }

        #endregion
    }
}
