namespace NetAF.Narratives
{
    /// <summary>
    /// Provides a section of narrative.
    /// </summary>
    /// <param name="elements">The elements that make up this section.</param>
    public class Section(string[] elements)
    {
        #region Fields

        private int index = -1;

        #endregion

        #region Properties

        /// <summary>
        /// Get if this section of narrative is complete.
        /// </summary>
        public bool IsComplete => elements == null || index == elements.Length - 1;

        #endregion

        /// <summary>
        /// Get the next entry.
        /// </summary>
        /// <returns>The next entry.</returns>
        public string Next()
        {
            if (index < (elements?.Length - 1 ?? 0))
                index++;

            var entry = elements?[index] ?? string.Empty;

            return entry;
        }

        /// <summary>
        /// Get all entries up to and including the current entry.
        /// </summary>
        /// <returns>All entries up to and including the current entry.</returns>
        public string[] AllUntilCurrent()
        {
            if (index < 0)
                Next();

            var current = new string[index + 1];

            for (var i = 0; i <= index; i++)
                current[i] = elements[i];

            return current;
        }
    }
}
