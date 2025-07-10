using NetAF.Extensions;
using NetAF.Serialization.Assets;
using NetAF.Serialization;
using System;
using System.Collections.Generic;

namespace NetAF.Logging.Notes
{
    /// <summary>
    /// Provides a manager for in-game notes.
    /// </summary>
    public class NoteManager : IRestoreFromObjectSerialization<NoteManagerSerialization>
    {
        #region Fields

        private readonly List<NoteEntry> entries = [];

        #endregion

        #region Properties

        /// <summary>
        /// Get the number of entries.
        /// </summary>
        public int Count => entries.Count;

        #endregion

        #region Methods

        /// <summary>
        /// Find an entry by its name.
        /// </summary>
        /// <param name="name">The name of the entry.</param>
        /// <returns>The entry, if found. Else null.</returns>
        private NoteEntry Find(string name)
        {
            return Array.Find([.. entries], x => x.Name.InsensitiveEquals(name));
        }

        /// <summary>
        /// Clear all entries.
        /// </summary>
        public void Clear()
        {
            entries.Clear();
        }

        /// <summary>
        /// Add a new entry.
        /// </summary>
        /// <param name="name">The name of the entry to add.</param>
        /// <param name="content">The content of the entry to add.</param>
        public void Add(string name, string content)
        {
            Add(new(name, content));
        }

        /// <summary>
        /// Add a new entry.
        /// </summary>
        /// <param name="entry">The entry to add.</param>
        public void Add(NoteEntry entry)
        {
            if (string.IsNullOrEmpty(entry.Content))
                return;

            var hit = Find(entry.Name);

            if (hit == null)
            {
                entries.Add(entry);
            }
            else if (hit.HasExpired)
            {
                // the entry was marked as expired before it was added, need to remove the old one and add a new
                // but expired entry now
                entries.Remove(hit);
                var duplicate = new NoteEntry(hit.Name, entry.Content);
                duplicate.Expire();
                entries.Add(duplicate);
            }   
        }

        /// <summary>
        /// Remove an entry.
        /// </summary>
        /// <param name="name">The name of the entry to remove.</param>
        public void Remove(string name)
        {
            var hit = Find(name);

            if (hit != null)
                entries.Remove(hit);
        }

        /// <summary>
        /// Expire an entry.
        /// </summary>
        /// <param name="name">The name of the entry to expire.</param>
        public void Expire(string name)
        {
            var hit = Find(name);

            if (hit == null)
            {
                // it's ok to expire a note that hasn't yet been added, just need to add one as a placeholder
                hit = new NoteEntry(name, string.Empty);
                entries.Add(hit);
            }

            hit.Expire();
        }

        /// <summary>
        /// Get is an entry is present.
        /// </summary>
        /// <param name="name">The name of the entry to check.</param>
        /// <returns>True if an entry with the same name is present.</returns>
        public bool ContainsEntry(string name)
        {
            return Find(name) != null;
        }

        /// <summary>
        /// Get is an entry has expired.
        /// </summary>
        /// <param name="name">The name of the entry to check.</param>
        /// <returns>True if an entry with the same name is present and has expired, else false.</returns>
        public bool HasExpired(string name)
        {
            var hit = Find(name);
            return hit?.HasExpired ?? false;
        }

        /// <summary>
        /// Get all entries.
        /// </summary>
        /// <returns>An array of all entries.</returns>
        public NoteEntry[] GetAll()
        {
            return [.. entries];
        }

        #endregion

        #region StaticMethods

        /// <summary>
        /// Create a new instance of NoteManager from a serialization.
        /// </summary>
        /// <param name="serialization">The serialization.</param>
        /// <returns>The note manager.</returns>
        public static NoteManager FromSerialization(NoteManagerSerialization serialization)
        {
            NoteManager manager = new();
            ((IRestoreFromObjectSerialization<NoteManagerSerialization>)manager).RestoreFrom(serialization);
            return manager;
        }

        #endregion

        #region Implementation of IRestoreFromObjectSerialization<NoteManagerSerialization>

        /// <summary>
        /// Restore this object from a serialization.
        /// </summary>
        /// <param name="serialization">The serialization to restore from.</param>
        void IRestoreFromObjectSerialization<NoteManagerSerialization>.RestoreFrom(NoteManagerSerialization serialization)
        {
            entries.Clear();

            foreach (var entry in serialization.Entries)
                Add(NoteEntry.FromSerialization(entry));
        }

        #endregion
    }
}
