namespace NetAF.Utilities
{
    /// <summary>
    /// Provides a marker in a string.
    /// </summary>
    /// <param name="Value">The value being marked.</param>
    /// <param name="StartIndex">The starting index of the marker.</param>
    public record StringMarker(string Value, int StartIndex)
    {
        /// <summary>
        /// Get the ending index of the marker.
        /// </summary>
        public int Length => Value.Length;

        /// <summary>
        /// Get the ending index of the marker.
        /// </summary>
        public int EndIndex => StartIndex + Length;
    }
}
