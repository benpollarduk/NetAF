namespace NetAF.Assets
{
    /// <summary>
    /// Represents a two-dimensional point.
    /// </summary>
    /// <param name="x">The x position.</param>
    /// <param name="y">The y position.</param>
    public readonly struct Point2D(int x, int y)
    {
        #region Properties

        /// <summary>
        /// Get the X position.
        /// </summary>
        public int X { get; } = x;

        /// <summary>
        /// Get the Y position.
        /// </summary>
        public int Y { get; } = y;

        #endregion
    }
}
