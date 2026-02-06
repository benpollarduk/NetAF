namespace NetAF.Targets.Markup
{
    /// <summary>
    /// Represents styling for text.
    /// </summary>
    /// <param name="Bold">Set to true if the text should be bold, else false.</param>
    /// <param name="Italic">Set to true if the text should be italic, else false.</param>
    /// <param name="Strikethrough">Set to true if the text should have a strike through, else false.</param>
    /// <param name="Underline">Set to true if the text should be underlined, else false.</param>
    /// <param name="Monospace">Set to true if the text should be displayed using a monospace font, else false.</param>
    /// <param name="Foreground">Specify a foreground color, else use null specify the default color.</param>
    /// <param name="Background">Specify a background color, else use null specify the default color.</param>
    public record TextStyle(bool Bold = false, 
                            bool Italic = false, 
                            bool Strikethrough = false, 
                            bool Underline = false, 
                            bool Monospace = false, 
                            Color Foreground = null, 
                            Color Background = null)
    {
        #region StaticProperties

        /// <summary>
        /// Get the default style.
        /// </summary>
        public static TextStyle Default => new(false, false, false, false, false, null, null);

        #endregion
    }
}
