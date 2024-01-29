﻿using System.Linq;

namespace BP.AdventureFramework.Conversations.Instructions
{
    /// <summary>
    /// An end of paragraph instruction that repeats.
    /// </summary>
    public sealed class Repeat : IEndOfPargraphInstruction
    {
        #region Implementation of IEndOfPargraphInstruction

        /// <summary>
        /// Get the index of the next paragraph.
        /// </summary>
        /// <param name="current">The current paragraph.</param>
        /// <param name="collection">The collection of paragraphs.</param>
        /// <returns>The index of the next paragraph.</returns>
        public int GetIndexOfNext(Paragraph current, Paragraph[] collection)
        {
            return collection.ToList().IndexOf(current);
        }

        #endregion
    }
}
