using System;

namespace NetAF.Targets.Console.Rendering.FrameBuilders.Legacy
{
    /// <summary>
    /// Provides an abstract base class for console frame builders.
    /// </summary>
    public abstract class ConsoleLegacyFrameBuilder
    {
        #region StaticMethods

        /// <summary>
        /// Get the effective width to use for text, constrained by MaxLineWidth.
        /// </summary>
        /// <param name="availableWidth">The total available width.</param>
        /// <param name="maxLineWidth">The maximum line width. For unlimited width, use a value less than 0.</param>
        /// <returns>The constrained width.</returns>
        public static int GetConstrainedWidth(int availableWidth, int maxLineWidth)
        {
            if (maxLineWidth < 0)
                return availableWidth;

            
            return Math.Min(availableWidth, maxLineWidth);
        }

        #endregion
    }
}
