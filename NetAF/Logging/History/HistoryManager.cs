using System.Collections.Generic;

namespace NetAF.Logging.History
{
    /// <summary>
    /// Provides a manager for in-game history.
    /// </summary>
    public class HistoryManager
    {
        #region Fields

        private readonly List<HistoryEntry> entries = [];

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
        public void Add(HistoryEntry entry)
        {
            entries.Add(entry);
        }

        #endregion
    }
}
