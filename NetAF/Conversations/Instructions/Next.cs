using System.Linq;

namespace NetAF.Conversations.Instructions
{
    /// <summary>
    /// An end of paragraph instruction that shifts paragraphs to the next paragraph.
    /// </summary>
    public sealed class Next : IEndOfPargraphInstruction
    {
        #region Implementation of IEndOfPargraphInstruction

        /// <inheritdoc/>
        public int GetIndexOfNext(Paragraph current, Paragraph[] paragraphs)
        {
            var currentIndex = paragraphs.ToList().IndexOf(current);
            var next = currentIndex + 1;
            var last = paragraphs.Length - 1;
            return next < last ? next : last;
        }

        #endregion
    }
}
