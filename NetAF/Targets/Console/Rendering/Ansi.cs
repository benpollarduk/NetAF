using System;

namespace NetAF.Targets.Console.Rendering
{
    /// <summary>
    /// Provides helper functions for ANSI colors.
    /// </summary>
    public static class Ansi
    {
        #region Constants

        /// <summary>
        /// Get the value for the NO_COLOR environment variable.
        /// </summary>
        public const string NO_COLOR = "NO_COLOR";

        /// <summary>
        /// Get the ANSI escape sequence to hide the cursor.
        /// </summary>
        public const string ANSI_HIDE_CURSOR = "\u001b[?25l";

        /// <summary>
        /// Get the ANSI escape sequence to show the cursor.
        /// </summary>
        public const string ANSI_SHOW_CURSOR = "\u001b[?25h";

        #endregion

        #region StaticMethods

        /// <summary>
        /// Determine if color is suppressed. If the NO_COLOR environment variable is present and set to anything other than '0' or 'false' this will return true.
        /// </summary>
        /// <returns>True if the NO_COLOR environment variable is present and set to anything other than an empty string, else false.</returns>
        public static bool IsColorSuppressed()
        {
            var value = Environment.GetEnvironmentVariable(NO_COLOR)?.ToLower() ?? string.Empty;

            return value switch
            {
                "" => false,
                _ => true,
            };
        }

        /// <summary>
        /// Get an ANSI escape sequence for a foreground color.
        /// </summary>
        /// <param name="color">The foreground color.</param>
        /// <returns>The ANSI escape sequence.</returns>
        public static string GetAnsiForegroundEscapeSequence(AnsiColor color)
        {
            return GetAnsiForegroundEscapeSequence(color.R, color.G, color.B);
        }

        /// <summary>
        /// Get an ANSI escape sequence for a foreground color.
        /// </summary>
        /// <param name="r">The red channel.</param>
        /// <param name="g">The green channel.</param>
        /// <param name="b">The blue channel.</param>
        /// <returns>The ANSI escape sequence.</returns>
        public static string GetAnsiForegroundEscapeSequence(byte r, byte g, byte b)
        {
            return $"\u001b[38;2;{r};{g};{b}m";
        }

        /// <summary>
        /// Get an ANSI escape sequence for a background color.
        /// </summary>
        /// <param name="color">The background color.</param>
        /// <returns>The ANSI escape sequence.</returns>
        public static string GetAnsiBackgroundEscapeSequence(AnsiColor color)
        {
            return GetAnsiBackgroundEscapeSequence(color.R, color.G, color.B);
        }

        /// <summary>
        /// Get an ANSI escape sequence for a background color.
        /// </summary>
        /// <param name="r">The red channel.</param>
        /// <param name="g">The green channel.</param>
        /// <param name="b">The blue channel.</param>
        /// <returns>The ANSI escape sequence.</returns>
        public static string GetAnsiBackgroundEscapeSequence(byte r, byte g, byte b)
        {
            return $"\u001b[48;2;{r};{g};{b}m";
        }

        #endregion
    }
}
