namespace NetAF.Assets
{
    /// <summary>
    /// Represents a three-dimensional point.
    /// </summary>
    /// <param name="x">The x position.</param>
    /// <param name="y">The y position.</param>
    /// <param name="z">The z position.</param>
    public readonly struct Point3D(int x, int y, int z)
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

        /// <summary>
        /// Get the Z position.
        /// </summary>
        public int Z { get; } = z;

        #endregion
    }
}
