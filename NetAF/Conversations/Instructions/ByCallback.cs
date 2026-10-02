using System;

namespace NetAF.Conversations.Instructions
{
    /// <summary>
    /// An end of paragraph instruction that shifts paragraphs based on a callback.
    /// </summary>
    /// <param name="callback">The callback that decides the instruction to use.</param>
    public sealed class ByCallback(Func<IEndOfPargraphInstruction> callback) : IEndOfPargraphInstruction
    {
        #region Properties

        /// <summary>
        /// Get the callback that decides the instruction to use.
        /// </summary>
        public Func<IEndOfPargraphInstruction> Callback { get; } = callback;

        #endregion

        #region Implementation of IEndOfPargraphInstruction

        /// <inheritdoc/>
        public int GetIndexOfNext(Paragraph current, Paragraph[] paragraphs)
        {
            return Callback.Invoke().GetIndexOfNext(current, paragraphs);
        }

        #endregion
    }
}
