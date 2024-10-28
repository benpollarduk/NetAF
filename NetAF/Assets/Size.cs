namespace NetAF.Assets
{
    /// <summary>
    /// Represents a size.
    /// </summary>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    public readonly struct Size(int width, int height)
    {
        #region Properties

        /// <summary>
        /// Get the width.
        /// </summary>
        public int Width { get; } = width;

        /// <summary>
        /// Get the height.
        /// </summary>
        public int Height { get; } = height;

        #endregion
    }
}
