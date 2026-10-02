namespace NetAF.Conversations.Instructions
{
    /// <summary>
    /// An end of paragraph instruction that shifts paragraphs to the start.
    /// </summary>
    public sealed class First : IEndOfPargraphInstruction
    {
        #region Implementation of IEndOfPargraphInstruction

        /// <inheritdoc/>
        public int GetIndexOfNext(Paragraph current, Paragraph[] paragraphs)
        {
            return 0;
        }

        #endregion
    }
}
