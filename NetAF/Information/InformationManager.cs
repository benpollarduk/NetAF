using NetAF.Extensions;
using NetAF.Serialization.Assets;
using NetAF.Serialization;
using System;
using System.Collections.Generic;

namespace NetAF.Information
{
    /// <summary>
    /// Provides a manager for acquired information.
    /// </summary>
    public class InformationManager : IRestoreFromObjectSerialization<InformationManagerSerialization>
    {
        #region Fields

        private readonly List<InformationEntry> entries = [];

        #endregion

        #region Properties

        /// <summary>
        /// Get the number of entries.
        /// </summary>
        public int Count => entries.Count;

        #endregion

        #region Methods

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
        /// <param name="entry">The entry to add.</param>
        public void Add(InformationEntry entry)
        {
            Remove(entry.Name);
            entries.Add(entry);
        }

        /// <summary>
        /// Remove an entry.
        /// </summary>
        /// <param name="name">The name of the entry to remove.</param>
        public void Remove(string name)
        {
            var hit = Array.Find([..entries], x => x.Name.InsensitiveEquals(name));

            if (hit == null)
                return;

            entries.Remove(hit);
        }

        /// <summary>
        /// Expire an entry.
        /// </summary>
        /// <param name="name">The name of the entry to expire.</param>
        public void Expire(string name)
        {
            var hit = Array.Find([.. entries], x => x.Name.InsensitiveEquals(name));

            if (hit == null)
                return;

            hit.Expire();
        }

        /// <summary>
        /// Get all entries.
        /// </summary>
        /// <returns>An array of all entries.</returns>
        public InformationEntry[] GetAll()
        {
            return [.. entries];
        }

        #endregion

        #region Implementation of IRestoreFromObjectSerialization<InformationManagerSerialization>

        /// <summary>
        /// Restore this object from a serialization.
        /// </summary>
        /// <param name="serialization">The serialization to restore from.</param>
        void IRestoreFromObjectSerialization<InformationManagerSerialization>.RestoreFrom(InformationManagerSerialization serialization)
        {
            entries.Clear();

            foreach (var entry in serialization.Entries)
                Add(InformationEntry.FromSerialization(entry));
        }

        #endregion

        #region StaticMethods

        /// <summary>
        /// Create a new instance of InformationManager from a serialization.
        /// </summary>
        /// <param name="serialization">The serialization.</param>
        /// <returns>The information manager.</returns>
        public static InformationManager FromSerialization(InformationManagerSerialization serialization)
        {
            InformationManager manager = new();
            ((IRestoreFromObjectSerialization<InformationManagerSerialization>)manager).RestoreFrom(serialization);
            return manager;
        }

        #endregion
    }
}
