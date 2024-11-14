using System;
using System.Text;
using NetAF.Rendering.FrameBuilders;
using NetAF.Rendering.FrameBuilders.Console;
using NetAF.Rendering.Presenters;
using NetAF.Utilities;

namespace NetAF.Rendering.Frames
{
    /// <summary>
    /// Provides a grid based frame for displaying a command based interface.
    /// </summary>
    /// <param name="builder">The builder that creates the frame.</param>
    /// <param name="cursorLeft">The cursor left position.</param>
    /// <param name="cursorTop">The cursor top position.</param>
    /// <param name="backgroundColor">The background color.</param>
    public sealed class GridTextFrame(GridStringBuilder builder, int cursorLeft, int cursorTop, AnsiColor backgroundColor) : IFrame
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

        /// <summary>
        /// Get the ANSI escape sequence to show the cursor.
        /// </summary>
        private const string ANSI_SHOW_CURSOR = "\u001b[?25h";

        #endregion

        #region Fields

        private readonly GridStringBuilder builder = builder;

        #endregion

        #region Properties

        /// <summary>
        /// Get the background color.
        /// </summary>
        public AnsiColor BackgroundColor { get; } = backgroundColor;

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
        public int CursorLeft { get; } = cursorLeft;

        /// <summary>
        /// Get the cursor top position.
        /// </summary>
        public int CursorTop { get; } = cursorTop;

        /// <summary>
        /// Get or set if the cursor should be shown.
        /// </summary>
        public bool ShowCursor { get; set; } = true;

        /// <summary>
        /// Render this frame on a presenter.
        /// </summary>
        /// <param name="presenter">The presenter.</param>
        public void Render(IFramePresenter presenter)
        {
            var suppressColor = IsColorSuppressed();

            if (!suppressColor)
                presenter.Write(GetAnsiBackgroundEscapeSequence(BackgroundColor));

            presenter.Write(ANSI_HIDE_CURSOR);

            for (var y = 0; y < builder.DisplaySize.Height; y++)
            {
                for (var x = 0; x < builder.DisplaySize.Width; x++)
                {
                    var c = builder.GetCharacter(x, y);

                    if (c != 0)
                    {
                        if (!suppressColor)
                        {
                            presenter.Write(GetAnsiForegroundEscapeSequence(builder.GetCellColor(x, y)));
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

            presenter.Write(ANSI_SHOW_CURSOR);
        }

        #endregion
    }
}