﻿using NetAF.Assets;
using NetAF.Logging.Notes;

namespace NetAF.Rendering.FrameBuilders
{
    /// <summary>
    /// Represents any object that can build note frames.
    /// </summary>
    public interface INoteFrameBuilder : IFrameBuilder
    {
        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="description">The description.</param>
        /// <param name="entries">The note entries.</param>
        /// <param name="size">The size of the frame.</param>
        /// <returns>The frame.</returns>
        IFrame Build(string title, string description, NoteEntry[] entries, Size size);
    }
}
