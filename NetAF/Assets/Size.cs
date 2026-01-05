namespace NetAF.Assets
{
    /// <summary>
    /// Represents a size.
    /// </summary>
    /// <param name="Width">The width of the size.</param>
    /// <param name="Height">The height of the size.</param>
    public record Size(int Width, int Height)
    {
        #region Constants

        /// <summary>
        /// Get a value representing a dynamic size.
        /// </summary>
        public static readonly Size Dynamic = new(0, 0);

        #endregion
    }
}
