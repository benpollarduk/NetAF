using System;

namespace NetAF.Rendering.FrameBuilders
{
    /// <summary>
    /// Provides a collection of all of the frame builders required to run a game.
    /// </summary>
    /// <param name="frameBuilders">The frame builders.</param>
    public class FrameBuilderCollection(params IFrameBuilder[] frameBuilders)
    { 
        #region Methods

        /// <summary>
        /// Get a frame builder for a specified type.
        /// </summary>
        /// <typeparam name="T">The type of frame builder.</typeparam>
        /// <returns>The frame builder.</returns>
        public T GetFrameBuilder<T>()
        {
            var match = Array.Find(frameBuilders, x => x is T);

            if (match == null)
                throw new InvalidOperationException($"There is no frame builder registered for {typeof(T)}.");

            return (T)match;
        }

        #endregion
    }
}
