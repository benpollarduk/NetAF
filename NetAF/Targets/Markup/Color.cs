namespace NetAF.Targets.Markup
{
    /// <summary>
    /// Represents a 24-bit color.
    /// </summary>
    /// <param name="Red">The red channel.</param>
    /// <param name="Green">The green channel.</param>
    /// <param name="Blue">The blue channel.</param>
    public record Color(byte Red, byte Green, byte Blue)
    {
        #region Properties

        /// <summary>
        /// Translates this Color to an HTML string color representation.
        /// </summary>
        /// <returns>The string that represents the HTML color.</returns>
        public string ToHtml() => $"#{Red:X2}{Green:X2}{Blue:X2}";

        #endregion

        #region StaticMethods

        /// <summary>
        /// Create a new instance of a Color from an HTML string representation.
        /// </summary>
        /// <param name="html">The string that represents the HTML color.</param>
        /// <returns>The Color.</returns>
        public static Color FromHtml(string html)
        {
            if (string.IsNullOrWhiteSpace(html))
                throw new System.ArgumentException("HTML color string cannot be null or empty.", nameof(html));

            // remove the '#' if it exists
            string hex = html.StartsWith('#') ? html.Substring(1) : html;

            // ensure exactly 6 characters (RRGGBB)
            if (hex.Length != 6)
                throw new System.ArgumentException("HTML color must be in RRGGBB hex format.", nameof(html));

            // parse the hex parts into bytes
            byte r = System.Convert.ToByte(hex.Substring(0, 2), 16);
            byte g = System.Convert.ToByte(hex.Substring(2, 2), 16);
            byte b = System.Convert.ToByte(hex.Substring(4, 2), 16);

            return new Color(r, g, b);
        }

        #endregion
    }
}