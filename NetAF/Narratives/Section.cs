namespace NetAF.Narratives
{
    /// <summary>
    /// Provides a section of narrative.
    /// </summary>
    /// <param name="elements">The elements that make up this section.</param>
    public class Section(string[] elements)
    {
        #region Fields

        private int index;

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
            var entry = Current();

            if (index < (elements?.Length - 1 ?? 0))
                index++;

            return entry;
        }

        /// <summary>
        /// Get the current entry.
        /// </summary>
        /// <returns>The current entry.</returns>
        public string Current()
        {
            return elements?[index] ?? string.Empty;
        }

        /// <summary>
        /// Get all entries up to and including the current entry.
        /// </summary>
        /// <returns>All entries up to and including the current entry.</returns>
        public string[] AllUntilCurrent()
        {
            var current = new string[index + 1];

            for (var i = 0; i <= index; i++)
                current[i] = elements[i];

            return current;
        }
    }
}
