using NetAF.Logging.Notes;
using System.Collections.Generic;
using System.Linq;

namespace NetAF.Serialization.Assets
{
    /// <summary>
    /// Represents a serialization of a NoteManager.
    /// </summary>
    public sealed class NoteManagerSerialization : IObjectSerialization<NoteManager>
    {
        #region Properties

        /// <summary>
        /// Get or set the entries.
        /// </summary>
        public List<NoteEntrySerialization> Entries { get; set; }

        #endregion

        #region StaticMethods

        /// <summary>
        /// Create a new serialization from an NoteManager.
        /// </summary>
        /// <param name="noteManager">The NoteManager to create the serialization from.</param>
        /// <returns>The serialization.</returns>
        public static NoteManagerSerialization FromNoteManager(NoteManager noteManager)
        {
            return new()
            {
                Entries = noteManager.GetAll()?.Select(NoteEntrySerialization.FromNoteEntry).ToList() ?? []
            };
        }

        #endregion

        #region Implementation of IObjectSerialization<LogManager>

        /// <summary>
        /// Restore an instance from this serialization.
        /// </summary>
        /// <param name="noteManager">The note manager to restore.</param>
        void IObjectSerialization<NoteManager>.Restore(NoteManager noteManager)
        {
            ((IRestoreFromObjectSerialization<NoteManagerSerialization>)noteManager).RestoreFrom(this);
        }

        #endregion
    }
}
