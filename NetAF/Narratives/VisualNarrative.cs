using NetAF.Rendering;
using System.Collections.Generic;

namespace NetAF.Narratives
{
    /// <summary>
    /// Provides a visual narrative built up from visuals.
    /// </summary>
    /// <param name="visuals">The visuals that make up the narrative.</param>
    public class VisualNarrative(Visual[] visuals)
    {
        #region Fields

        private int index = -1;

        #endregion

        #region Properties

        /// <summary>
        /// Get if the narrative is complete.
        /// </summary>
        public bool IsComplete => visuals == null || index == visuals.Length - 1;

        #endregion

        #region Methods

        /// <summary>
        /// Get the next element.
        /// </summary>
        /// <returns>The next element</returns>
        public Visual Next()
        {
            if (index < 0)
                index = 0;

            if (index < visuals.Length - 1)
                index++;

            return visuals[index];
        }

        /// <summary>
        /// Get all entries in the current section up to and including the current element.
        /// </summary>
        /// <returns>All elements in the current section up to and including the current element.</returns>
        public Visual[] AllUntilCurrent()
        {
            List<Visual> list = []; 

            for (int i = 0; i <= index; i++)
                list.Add(Next());

            return [.. list];
        }

        #endregion
    }
}
