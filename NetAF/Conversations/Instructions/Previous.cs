using System.Linq;

namespace NetAF.Conversations.Instructions
{
    /// <summary>
    /// An end of paragraph instruction that shifts paragraphs to the previous paragraph.
    /// </summary>
    public sealed class Previous : IEndOfPargraphInstruction
    {
        #region Implementation of IEndOfPargraphInstruction

        /// <inheritdoc/>
        public int GetIndexOfNext(Paragraph current, Paragraph[] paragraphs)
        {
            var currentIndex = paragraphs.ToList().IndexOf(current);
            var previous = currentIndex - 1;
            return previous >= 0 ? previous : 0;
        }

        #endregion
    }
}
