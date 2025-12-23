namespace NetAF.Narratives
{
    /// <summary>
    /// Provides a narrative built up from sections.
    /// </summary>
    /// <param name="title">A title for the narrative.</param>
    /// <param name="sections">The sections that make up the narrative.</param>
    public class Narrative(string title, Section[] sections)
    {
        #region Fields

        private int index = -1;

        #endregion

        #region Properties

        /// <summary>
        /// Get the title for this narrative.
        /// </summary>
        public string Title => title;

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
            if (index < 0)
                index = 0;

            if ((sections?[index].IsComplete ?? false) && index < sections.Length - 1)
                index++;

            var entry = sections?[index].Next() ?? string.Empty;

            return entry;
        }

        /// <summary>
        /// Get all entries in the current section up to and including the current element.
        /// </summary>
        /// <returns>All elements in the current section up to and including the current element.</returns>
        public string[] AllUntilCurrent()
        {
            if (index < 0)
                Next();

            return sections?[index].AllUntilCurrent() ?? [string.Empty];
        }

        #endregion
    }
}
