using NetAF.Rendering;
using System.Text;

namespace NetAF.Targets.Console.Rendering
{
    /// <summary>
    /// Provides a grid based frame for displaying a visual.
    /// </summary>
    /// <param name="builder">The builder that creates the frame.</param>
    /// <param name="cursorLeft">The cursor left position.</param>
    /// <param name="cursorTop">The cursor top position.</param>
    public sealed class GridVisualFrame(GridVisualBuilder builder, int cursorLeft = 0, int cursorTop = 0) : IConsoleFrame
    {
        #region Properties

        /// <summary>
        /// Get the width of the frame.
        /// </summary>
        public int Width => builder.DisplaySize.Width;

        /// <summary>
        /// Get the height of the frame.
        /// </summary>
        public int Height => builder.DisplaySize.Height;

        #endregion

        #region Methods

        /// <summary>
        /// Get the foreground color for a cell.
        /// </summary>
        /// <param name="x">The x position of the cell.</param>
        /// <param name="y">The x position of the cell.</param>
        /// <param name="suppressColor">Specify if color should be suppressed.</param>
        /// <returns>The color.</returns>
        private AnsiColor GetForegroundColor(int x, int y, bool suppressColor)
        {
            return !suppressColor ? builder.GetCellForegroundColor(x, y) : AnsiColor.BrightWhite;
        }

        /// <summary>
        /// Get the background color for a cell.
        /// </summary>
        /// <param name="x">The x position of the cell.</param>
        /// <param name="y">The x position of the cell.</param>
        /// <param name="suppressColor">Specify if color should be suppressed.</param>
        /// <returns>The color.</returns>
        private AnsiColor GetBackgroundColor(int x, int y, bool suppressColor)
        {
            return !suppressColor ? builder.GetCellBackgroundColor(x, y) : builder.GetCellBackgroundColor(x, y).ToGray();
        }

        /// <summary>
        /// Render a single character.
        /// </summary>
        /// <param name="stringBuilder">The string builder.</param>
        /// <param name="x">The x position of the cell.</param>
        /// <param name="y">The x position of the cell.</param>
        /// <param name="suppressColor">True if color should be suppressed, else false.</param>
        /// <param name="lastForeground">The last foreground color.</param>
        /// <param name="lastBackground">The last background color.</param>
        private void RenderCharacter(StringBuilder stringBuilder, int x, int y, bool suppressColor, ref AnsiColor lastForeground, ref AnsiColor lastBackground)
        {
            var c = builder.GetCharacter(x, y);
            var backgroundColor = GetBackgroundColor(x, y, suppressColor);

            UpdateBackgroundColor(stringBuilder, x, y, backgroundColor, ref lastBackground);

            if (c != 0)
            {
                var foregroundColor = GetForegroundColor(x, y, suppressColor);
                UpdateForegroundColor(stringBuilder, x, y, foregroundColor, ref lastForeground);
                stringBuilder.Append(c);
            }
            else
            {
                stringBuilder.Append(' ');
            }
        }

        #endregion

        #region StaticMethods

        /// <summary>
        /// Update the background color, if required.
        /// </summary>
        /// <param name="stringBuilder">The string builder.</param>
        /// <param name="x">The x position of the cell.</param>
        /// <param name="y">The x position of the cell.</param>
        /// <param name="backgroundColor">The background color.</param>
        /// <param name="lastBackgroundColor">The last background color.</param>
        private static void UpdateBackgroundColor(StringBuilder stringBuilder, int x, int y, AnsiColor backgroundColor, ref AnsiColor lastBackgroundColor)
        {
            if (RequiresColorChange(x, y, lastBackgroundColor, backgroundColor))
            {
                lastBackgroundColor = backgroundColor;
                stringBuilder.Append(Ansi.GetAnsiBackgroundEscapeSequence(backgroundColor));
            }
        }

        /// <summary>
        /// Update the foreground color, if required.
        /// </summary>
        /// <param name="stringBuilder">The string builder.</param>
        /// <param name="x">The x position of the cell.</param>
        /// <param name="y">The x position of the cell.</param>
        /// <param name="foregroundColor">The foreground color.</param>
        /// <param name="lastForegroundColor">The last foreground color.</param>
        private static void UpdateForegroundColor(StringBuilder stringBuilder, int x, int y, AnsiColor foregroundColor, ref AnsiColor lastForegroundColor)
        {
            if (RequiresColorChange(x, y, lastForegroundColor, foregroundColor))
            {
                lastForegroundColor = foregroundColor;
                stringBuilder.Append(Ansi.GetAnsiForegroundEscapeSequence(foregroundColor));
            }
        }

        /// <summary>
        /// Get if a cell is the first cell.
        /// </summary>
        /// <param name="x">The x position of the cell.</param>
        /// <param name="y">The x position of the cell.</param>
        /// <returns>True if the first cell, else false.</returns>
        private static bool IsFirstCell(int x, int y)
        {
            return x == 0 && y == 0;
        }

        /// <summary>
        /// Get if a color change is required.
        /// </summary>
        /// <param name="x">The x position of the cell.</param>
        /// <param name="y">The x position of the cell.</param>
        /// <param name="current">The current color.</param>
        /// <param name="next">The next color.</param>
        /// <returns>True if a color change is required, else false.</returns>
        private static bool RequiresColorChange(int x, int y, AnsiColor current, AnsiColor next)
        {
            return IsFirstCell(x, y) || !next.Equals(current);
        }

        #endregion

        #region Implementation of IFrame

        /// <inheritdoc/>
        public int CursorLeft { get; } = cursorLeft;

        /// <inheritdoc/>
        public int CursorTop { get; } = cursorTop;

        /// <inheritdoc/>
        public bool ShowCursor { get; set; } = false;

        ///  <inheritdoc/>
        public AnsiColor InputColor { get; } = AnsiColor.White;

        /// <inheritdoc/>
        public void Render(IFramePresenter presenter)
        {
            var stringBuilder = new StringBuilder();
            var suppressColor = Ansi.IsColorSuppressed();

            stringBuilder.Append(Ansi.ANSI_HIDE_CURSOR);

            AnsiColor lastBackground = AnsiColor.Black;
            AnsiColor lastForeground = AnsiColor.White;

            for (var y = 0; y < builder.DisplaySize.Height; y++)
            {
                for (var x = 0; x < builder.DisplaySize.Width; x++)
                    RenderCharacter(stringBuilder, x, y, suppressColor, ref lastForeground, ref lastBackground);

                if (y < builder.DisplaySize.Height - 1)
                    stringBuilder.Append(builder.LineTerminator);
            }

            presenter.Present(stringBuilder.ToString());
        }

        #endregion

        #region Implementation of IAnsiGridFrame

        /// <inheritdoc/>
        public AnsiCell GetCell(int x, int y)
        {
            return new AnsiCell(builder.GetCharacter(x, y), GetForegroundColor(x, y, false), GetBackgroundColor(x, y, false));
        }

        #endregion
    }
}