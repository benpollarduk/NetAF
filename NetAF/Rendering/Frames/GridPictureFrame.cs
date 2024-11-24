using System;
using System.Text;
using NetAF.Rendering.FrameBuilders;
using NetAF.Rendering.FrameBuilders.Console;
using NetAF.Rendering.Presenters;
using NetAF.Utilities;

namespace NetAF.Rendering.Frames
{
    /// <summary>
    /// Provides a grid based frame for displaying a picture.
    /// </summary>
    /// <param name="builder">The builder that creates the frame.</param>
    public sealed class GridPictureFrame(GridPictureBuilder builder) : IFrame
    {
        #region Constants

        /// <summary>
        /// Get the value for the NO_COLOR environment variable.
        /// </summary>
        internal const string NO_COLOR = "NO_COLOR";

        /// <summary>
        /// Get the ANSI escape sequence to hide the cursor.
        /// </summary>
        private const string ANSI_HIDE_CURSOR = "\u001b[?25l";

        #endregion

        #region StaticMethods

        /// <summary>
        /// Determine if color is suppressed. If the NO_COLOR environment variable is present and set to anything other than '0' or 'false' this will return true.
        /// </summary>
        /// <returns>True if the NO_COLOR environment variable is present and set to anything other than '0' or 'false', else false.</returns>
        internal static bool IsColorSuppressed()
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
        private static string GetAnsiForegroundEscapeSequence(AnsiColor color)
        {
            return $"\u001B[{(int)color}m";
        }

        /// <summary>
        /// Get an ANSI escape sequence for a background color.
        /// </summary>
        /// <param name="color">The background color.</param>
        /// <returns>The ANSI escape sequence.</returns>
        private static string GetAnsiBackgroundEscapeSequence(AnsiColor color)
        {
            return $"\u001B[{(int)color + 10}m";
        }

        #endregion

        #region Overrides of Object

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            StringBuilder stringBuilder = new();

            for (var y = 0; y < builder.DisplaySize.Height; y++)
            {
                for (var x = 0; x < builder.DisplaySize.Width; x++)
                {
                    stringBuilder.Append(builder.GetCharacter(x, y));
                }

                stringBuilder.Append(StringUtilities.Newline);
            }

            return stringBuilder.ToString();
        }

        #endregion

        #region Implementation of IFrame

        /// <summary>
        /// Get the cursor left position.
        /// </summary>
        public int CursorLeft { get; } = 0;

        /// <summary>
        /// Get the cursor top position.
        /// </summary>
        public int CursorTop { get; } = 0;

        /// <summary>
        /// Get or set if the cursor should be shown.
        /// </summary>
        public bool ShowCursor { get; set; } = false;

        /// <summary>
        /// Render this frame on a presenter.
        /// </summary>
        /// <param name="presenter">The presenter.</param>
        public void Render(IFramePresenter presenter)
        {
            var suppressColor = IsColorSuppressed();

            presenter.Write(ANSI_HIDE_CURSOR);

            AnsiColor lastBackground = AnsiColor.Black;
            AnsiColor lastForeground = AnsiColor.White;

            for (var y = 0; y < builder.DisplaySize.Height; y++)
            {
                for (var x = 0; x < builder.DisplaySize.Width; x++)
                {
                    var c = builder.GetCharacter(x, y);

                    var backgroundColor = !suppressColor ? builder.GetCellBackgroundColor(x, y) : AnsiColor.Black;

                    if (x == 0 && y == 0 || backgroundColor != lastBackground)
                    {
                        lastBackground = backgroundColor;
                        presenter.Write(GetAnsiBackgroundEscapeSequence(backgroundColor));
                    }

                    if (c != 0)
                    {
                        var foregroundColor = !suppressColor ? builder.GetCellForegroundColor(x, y) : AnsiColor.White;

                        if (x == 0 && y == 0 || foregroundColor != lastForeground)
                        {
                            lastForeground = foregroundColor;
                            presenter.Write(GetAnsiForegroundEscapeSequence(foregroundColor));
                        }

                        presenter.Write(c);
                    }
                    else
                    {
                        presenter.Write(" ");
                    }
                }

                if (y < builder.DisplaySize.Height - 1)
                    presenter.Write(builder.LineTerminator);
            }
        }

        #endregion
    }
}