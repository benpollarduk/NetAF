using System;

namespace NetAF.Logging.History
{
    /// <summary>
    /// Provides an entry to the history log.
    /// </summary>
    /// <param name="name">The name of the entry.</param>
    /// <param name="content">The content of the entry.</param>
    public record HistoryEntry(string name, string content)
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
        /// Get the time this element was created
        /// </summary>
        public DateTime CreationTime { get; } = DateTime.Now;

        #endregion
    }
}
