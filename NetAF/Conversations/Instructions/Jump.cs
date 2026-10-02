using System.Linq;

namespace NetAF.Conversations.Instructions
{
    /// <summary>
    /// An end of paragraph instruction that shifts paragraphs based on a delta.
    /// </summary>
    /// <param name="delta">The delta to shift paragraphs by.</param>
    public sealed class Jump(int delta) : IEndOfPargraphInstruction
    {
        #region Properties

        /// <summary>
        /// Get the delta.
        /// </summary>
        public int Delta { get; } = delta;

        #endregion

        #region Implementation of IEndOfPargraphInstruction

        /// <inheritdoc/>
        public int GetIndexOfNext(Paragraph current, Paragraph[] paragraphs)
        {
            var currentIndex = paragraphs.ToList().IndexOf(current);
            var offset = currentIndex + Delta;

            if (offset < 0)
                return 0;

            if (offset >= paragraphs.Length)
                return paragraphs.Length - 1;

            return offset;
        }

        #endregion
    }
}
