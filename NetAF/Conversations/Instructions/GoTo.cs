namespace NetAF.Conversations.Instructions
{
    /// <summary>
    /// An end of paragraph instruction that shifts paragraphs based on an absolute index.
    /// </summary>
    /// <param name="index">The index of the next paragraph.</param>
    public sealed class GoTo(int index) : IEndOfPargraphInstruction
    {
        #region Properties

        /// <summary>
        /// Get the index.
        /// </summary>
        public int Index { get; } = index;

        #endregion

        #region Implementation of IEndOfPargraphInstruction

        /// <inheritdoc/>
        public int GetIndexOfNext(Paragraph current, Paragraph[] paragraphs)
        {
            if (Index < 0)
                return 0;

            if (Index >= paragraphs.Length)
                return paragraphs.Length - 1;

            return Index;
        }

        #endregion
    }
}
