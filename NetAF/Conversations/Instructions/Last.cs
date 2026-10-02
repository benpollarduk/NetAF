using System.Linq;

namespace NetAF.Conversations.Instructions
{
    /// <summary>
    /// An end of paragraph instruction that shifts paragraphs to the end.
    /// </summary>
    public sealed class Last : IEndOfPargraphInstruction
    {
        #region Implementation of IEndOfPargraphInstruction

        /// <inheritdoc/>
        public int GetIndexOfNext(Paragraph current, Paragraph[] paragraphs)
        {
            return paragraphs.Any() ? paragraphs.Length - 1 : 0;
        }

        #endregion
    }
}
