namespace NetAF.Rendering.Console
{
    /// <summary>
    /// Provides a palette of ANSI colors.
    /// </summary>
    public static class NetAFPalette
    {
        #region StaticProperties

        /// <summary>
        /// Get the default NetAF red.
        /// </summary>
        public static AnsiColor NetAFRed { get; } = new(255, 128, 128);

        /// <summary>
        /// Get the default NetAF green.
        /// </summary>
        public static AnsiColor NetAFGreen { get; } = new(128, 255, 128);

        /// <summary>
        /// Get the default NetAF blue.
        /// </summary>
        public static AnsiColor NetAFBlue { get; } = new(128, 128, 255);

        /// <summary>
        /// Get the default NetAF yellow.
        /// </summary>
        public static AnsiColor NetAFYellow { get; } = new(220, 220, 85);

        #endregion
    }
}
