using System.Linq;

namespace NetAF.Conversations.Instructions
{
    /// <summary>
    /// An end of paragraph instruction that repeats.
    /// </summary>
    public sealed class Repeat : IEndOfPargraphInstruction
    {
        #region Implementation of IEndOfPargraphInstruction

        /// <inheritdoc/>
        public int GetIndexOfNext(Paragraph current, Paragraph[] paragraphs)
        {
            return paragraphs.ToList().IndexOf(current);
        }

        #endregion
    }
}
