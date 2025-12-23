namespace NetAF.Narratives
{
    /// <summary>
    /// Provides a narrative built up from sections.
    /// </summary>
    /// <param name="sections">The sections that make up the narrative.</param>
    public class Narrative(Section[] sections)
    {
        #region Fields

        private int index;

        #endregion

        #region Properties

        /// <summary>
        /// Get if the narrative is complete.
        /// </summary>
        public bool IsComplete => sections == null || index == sections.Length - 1 && IsCurrentSectionComplete;

        /// <summary>
        /// Get if the current section is complete.
        /// </summary>
        public bool IsCurrentSectionComplete => sections == null || sections[index].IsComplete;

        #endregion

        #region Methods

        /// <summary>
        /// Get the next element.
        /// </summary>
        /// <returns>The next element</returns>
        public string Next()
        {
            var entry = Current();

            sections?[index].Next();

            if ((sections?[index].IsComplete ?? false) && index < sections.Length - 1)
                index++;

            return entry;
        }

        /// <summary>
        /// Get the current element.
        /// </summary>
        /// <returns>The current element.</returns>
        public string Current()
        {
            return sections?[index].Current() ?? string.Empty;
        }

        /// <summary>
        /// Get all entries in the current section up to and including the current element.
        /// </summary>
        /// <returns>All elements in the current section up to and including the current element.</returns>
        public string[] AllUntilCurrent()
        {
            return sections?[index].AllUntilCurrent() ?? [string.Empty];
        }

        #endregion
    }
}
