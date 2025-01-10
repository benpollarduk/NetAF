using System;
using System.Collections.Generic;

namespace NetAF.Rendering.FrameBuilders
{
    /// <summary>
    /// Provides a collection of all of the frame builders required to run a game.
    /// </summary>
    /// <param name="frameBuilders">A dictionary that specifies which instance of frame builder should be returned for which type.</param>
    public class FrameBuilderCollection(Dictionary<Type, IFrameBuilder> frameBuilders)
    { 
        #region Methods

        /// <summary>
        /// Get a frame builder for a specified type.
        /// </summary>
        /// <typeparam name="T">The type of frame builder.</typeparam>
        /// <returns>The frame builder.</returns>
        public T GetFrameBuilder<T>()
        {
            if (frameBuilders?.ContainsKey(typeof(T)) ?? false)
                return (T)frameBuilders[typeof(T)];

            return default;
        }

        #endregion
    }
}
