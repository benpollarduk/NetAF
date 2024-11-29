﻿using NetAF.Assets;
using NetAF.Rendering.Console;

namespace NetAF.Rendering.FrameBuilders
{
    /// <summary>
    /// Represents any object that can build visual frames.
    /// </summary>
    public interface IVisualFrameBuilder
    {
        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="description">The description.</param>
        /// <param name="gridVisualBuilder">The grid visual builder.</param>
        /// <param name="size">The size of the frame.</param>
        /// <returns>The frame.</returns>
        IFrame Build(string title, string description, GridVisualBuilder gridVisualBuilder, Size size);
    }
}
