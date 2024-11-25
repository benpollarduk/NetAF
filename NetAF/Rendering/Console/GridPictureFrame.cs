using System.Text;
using NetAF.Utilities;

namespace NetAF.Rendering.Console
{
    /// <summary>
    /// Provides a grid based frame for displaying a picture.
    /// </summary>
    /// <param name="builder">The builder that creates the frame.</param>
    public sealed class GridPictureFrame(GridPictureBuilder builder) : IFrame
    {
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
            return !suppressColor ? builder.GetCellForegroundColor(x, y) : AnsiColor.White;
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
            return !suppressColor ? builder.GetCellBackgroundColor(x, y) : AnsiColor.White;
        }

        /// <summary>
        /// Render a single character.
        /// </summary>
        /// <param name="presenter">The presenter.</param>
        /// <param name="x">The x position of the cell.</param>
        /// <param name="y">The x position of the cell.</param>
        /// <param name="suppressColor">True if color should be suppressed, else false.</param>
        /// <param name="lastForeground">The last foreground color.</param>
        /// <param name="lastBackground">The last background color.</param>
        private void RenderCharacter(IFramePresenter presenter, int x, int y, bool suppressColor, ref AnsiColor lastForeground, ref AnsiColor lastBackground)
        {
            var c = builder.GetCharacter(x, y);
            var backgroundColor = GetBackgroundColor(x, y, suppressColor);

            UpdateBackgroundColor(presenter, x, y, backgroundColor, ref lastBackground);

            if (c != 0)
            {
                var foregroundColor = GetForegroundColor(x, y, suppressColor);
                UpdateForegroundColor(presenter, x, y, foregroundColor, ref lastForeground);
                presenter.Write(c);
            }
            else
            {
                presenter.Write(" ");
            }
        }

        /// <summary>
        /// Update the background color, if required.
        /// </summary>
        /// <param name="presenter">The presenter.</param>
        /// <param name="x">The x position of the cell.</param>
        /// <param name="y">The x position of the cell.</param>
        /// <param name="backgroundColor">The background color.</param>
        /// <param name="lastBackgroundColor">The last background color.</param>
        private void UpdateBackgroundColor(IFramePresenter presenter, int x, int y, AnsiColor backgroundColor, ref AnsiColor lastBackgroundColor)
        {
            if (RequiresColorChange(x, y, lastBackgroundColor, backgroundColor))
            {
                lastBackgroundColor = backgroundColor;
                presenter.Write(Ansi.GetAnsiBackgroundEscapeSequence(backgroundColor));
            }
        }

        /// <summary>
        /// Update the foreground color, if required.
        /// </summary>
        /// <param name="presenter">The presenter.</param>
        /// <param name="x">The x position of the cell.</param>
        /// <param name="y">The x position of the cell.</param>
        /// <param name="foregroundColor">The foreground color.</param>
        /// <param name="lastForegroundColor">The last foreground color.</param>
        private void UpdateForegroundColor(IFramePresenter presenter, int x, int y, AnsiColor foregroundColor, ref AnsiColor lastForegroundColor)
        {
            if (RequiresColorChange(x, y, lastForegroundColor, foregroundColor))
            {
                lastForegroundColor = foregroundColor;
                presenter.Write(Ansi.GetAnsiForegroundEscapeSequence(foregroundColor));
            }
        }

        #endregion

        #region StaticMethods

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
            return IsFirstCell(x, y) || next != current;
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
            var suppressColor = Ansi.IsColorSuppressed();

            presenter.Write(Ansi.ANSI_HIDE_CURSOR);

            AnsiColor lastBackground = AnsiColor.Black;
            AnsiColor lastForeground = AnsiColor.White;

            for (var y = 0; y < builder.DisplaySize.Height; y++)
            {
                for (var x = 0; x < builder.DisplaySize.Width; x++)
                {
                    RenderCharacter(presenter, x, y, suppressColor, ref lastForeground, ref lastBackground);
                }

                if (y < builder.DisplaySize.Height - 1)
                    presenter.Write(builder.LineTerminator);
            }
        }

        #endregion
    }
}