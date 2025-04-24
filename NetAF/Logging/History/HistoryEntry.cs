using System;

namespace NetAF.Logging.History
{
    /// <summary>
    /// Provides an entry to the history log.
    /// </summary>
    /// <param name="Name">The name of the entry.</param>
    /// <param name="Content">The content of the entry.</param>
    public record HistoryEntry(string Name, string Content)
    {
        #region Properties

        /// <summary>
        /// Get the time this element was created
        /// </summary>
        public DateTime CreationTime { get; } = DateTime.Now;

        #endregion
    }
}
