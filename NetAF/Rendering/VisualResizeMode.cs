namespace NetAF.Rendering
{
    /// <summary>
    /// Enumeration of resize modes for visuals.
    /// </summary>
    public enum VisualResizeMode
    {
        /// <summary>
        /// Crop the visual to the new size.
        /// </summary>
        Crop,
        /// <summary>
        /// Scale the visual to the new size.
        /// </summary>
        Scale,
        /// <summary>
        /// Scale the visual to the new size, only if the new size is smaller than the origial size.
        /// </summary>
        ScaleDown
    }
}
