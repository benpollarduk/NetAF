using System;
using System.Collections.Generic;

namespace NetAF.Rendering.Console
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

        #region StaticProperties

        /// <summary>
        /// Get a mapping between ANSI colors and their RGB values.
        /// </summary>
        private static readonly Dictionary<AnsiColor, (byte R, byte G, byte B)> AnsiColorMap = new()
        {
            { AnsiColor.Black, (0, 0, 0) },
            { AnsiColor.Red, (128, 0, 0) },
            { AnsiColor.Green, (0, 128, 0) },
            { AnsiColor.Yellow, (128, 128, 0) },
            { AnsiColor.Blue, (0, 0, 128) },
            { AnsiColor.Magenta, (128, 0, 128) },
            { AnsiColor.Cyan, (0, 128, 128) },
            { AnsiColor.White, (192, 192, 192) },
            { AnsiColor.BrightBlack, (128, 128, 128) },
            { AnsiColor.BrightRed, (255, 0, 0) },
            { AnsiColor.BrightGreen, (0, 255, 0) },
            { AnsiColor.BrightYellow, (255, 255, 0) },
            { AnsiColor.BrightBlue, (0, 0, 255) },
            { AnsiColor.BrightMagenta, (255, 0, 255) },
            { AnsiColor.BrightCyan, (0, 255, 255) },
            { AnsiColor.BrightWhite, (255, 255, 255) }
        };

        #endregion

        #region StaticMethods

        /// <summary>
        /// Find the nearest AnsiColor to a RGB color using Euclidean distance.
        /// </summary>
        /// <param name="r">The value of the red channel.</param>
        /// <param name="g">The value of the green channel.</param>
        /// <param name="b">The value of the blue channel.</param>
        /// <returns>The nearest AnsiColor.</returns>
        public static AnsiColor FindNearestAnsiColor(byte r, byte g, byte b)
        {
            var nearestColor = AnsiColor.Black;
            var smallestDistance = double.MaxValue;

            foreach (var (ansiColor, rgb) in AnsiColorMap)
            {
                var distance = Math.Sqrt(
                    Math.Pow(r - rgb.R, 2) +
                    Math.Pow(g - rgb.G, 2) +
                    Math.Pow(b - rgb.B, 2)
                );

                if (distance < smallestDistance)
                {
                    smallestDistance = distance;
                    nearestColor = ansiColor;
                }
            }

            return nearestColor;
        }

        /// <summary>
        /// Determine if color is suppressed. If the NO_COLOR environment variable is present and set to anything other than '0' or 'false' this will return true.
        /// </summary>
        /// <returns>True if the NO_COLOR environment variable is present and set to anything other than '0' or 'false', else false.</returns>
        public static bool IsColorSuppressed()
        {
            var value = Environment.GetEnvironmentVariable(NO_COLOR)?.ToLower() ?? string.Empty;

            return value switch
            {
                "" or "0" or "false" => false,
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
            return $"\u001B[{(int)color}m";
        }

        /// <summary>
        /// Get an ANSI escape sequence for a background color.
        /// </summary>
        /// <param name="color">The background color.</param>
        /// <returns>The ANSI escape sequence.</returns>
        public static string GetAnsiBackgroundEscapeSequence(AnsiColor color)
        {
            return $"\u001B[{(int)color + 10}m";
        }

        #endregion
    }
}
