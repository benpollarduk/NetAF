﻿using System.Linq;
using NetAF.Extensions;

namespace NetAF.Conversations.Instructions
{
    /// <summary>
    /// An end of paragraph instruction that shifts paragraphs based on a name.
    /// </summary>
    public sealed class ToName : IEndOfPargraphInstruction
    {
        #region Properties

        /// <summary>
        /// Get the name of the paragraph to jump to.
        /// </summary>
        public string Name { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance of the ToName class.
        /// </summary>
        /// <param name="name">The name of the paragraph to jump to.</param>
        public ToName(string name)
        {
            Name = name;
        }

        #endregion

        #region Implementation of IEndOfPargraphInstruction

        /// <summary>
        /// Get the index of the next paragraph.
        /// </summary>
        /// <param name="current">The current paragraph.</param>
        /// <param name="paragraphs">The collection of paragraphs.</param>
        /// <returns>The index of the next paragraph.</returns>
        public int GetIndexOfNext(Paragraph current, Paragraph[] paragraphs)
        {
            var target = paragraphs.FirstOrDefault(x => x.Name.InsensitiveEquals(Name));
            return target == null ? 0 : paragraphs.ToList().IndexOf(target);
        }

        #endregion
    }
}
