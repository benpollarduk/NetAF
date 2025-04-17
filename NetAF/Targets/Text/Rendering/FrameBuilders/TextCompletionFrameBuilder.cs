﻿using NetAF.Assets;
using NetAF.Extensions;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;
using System.Text;

namespace NetAF.Targets.Text.Rendering.FrameBuilders
{
    /// <summary>
    /// Provides a builder of completion frames.
    /// </summary>
    /// <param name="builder">A builder to use for the text layout.</param>
    public sealed class TextCompletionFrameBuilder(StringBuilder builder) : ICompletionFrameBuilder
    {
        #region Implementation of ICompletionFrameBuilder

        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="message">The message to display to the user.</param>
        /// <param name="reason">The reason the game ended.</param>
        /// <param name="size">The size of the frame.</param>
        /// <returns>The frame.</returns>
        public IFrame Build(string message, string reason, Size size)
        {
            builder.Clear();

            builder.AppendLine(message);

            builder.AppendLine(reason.EnsureFinishedSentence());

            return new TextFrame(builder);
        }

        #endregion
    }
}
