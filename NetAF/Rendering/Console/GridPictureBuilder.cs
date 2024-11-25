using NetAF.Assets;
using NetAF.Utilities;

namespace NetAF.Rendering.Console
{
    /// <summary>
    /// Provides a class for building pictures in a grid.
    /// </summary>
    /// <param name="background">The background color.</param>
    /// <param name="foreground">The foreground color.</param>
    public class GridPictureBuilder(AnsiColor background, AnsiColor foreground)
    {
        #region Fields

        private AnsiColor?[,] foregroundColors;
        private AnsiColor?[,] backgroundColors;
        private char[,] buffer;

        #endregion

        #region Properties

        /// <summary>
        /// Get or set the line terminator.
        /// </summary>
        public char LineTerminator { get; set; } = StringUtilities.Newline;

        /// <summary>
        /// Get the display size.
        /// </summary>
        public Size DisplaySize { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// Resize this builder.
        /// </summary>
        /// <param name="displaySize">The new size.</param>
        public void Resize(Size displaySize)
        {
            DisplaySize = displaySize;
            Flush();
        }

        /// <summary>
        /// Get a character from the buffer.
        /// </summary>
        /// <param name="x">The x position of the character.</param>
        /// <param name="y">The y position of the character.</param>
        /// <returns>The character.</returns>
        public char GetCharacter(int x, int y)
        {
            return buffer[x, y];
        }

        /// <summary>
        /// Get a foreground color for a cell.
        /// </summary>
        /// <param name="x">The x position of the cell.</param>
        /// <param name="y">The y position of the cell.</param>
        /// <returns>The cell foreground color.</returns>
        public AnsiColor GetCellForegroundColor(int x, int y)
        {
            return foregroundColors[x, y] ?? foreground;
        }

        /// <summary>
        /// Get a background color for a cell.
        /// </summary>
        /// <param name="x">The x position of the cell.</param>
        /// <param name="y">The y position of the cell.</param>
        /// <returns>The cell background color.</returns>
        public AnsiColor GetCellBackgroundColor(int x, int y)
        {
            return backgroundColors[x, y] ?? background;
        }

        /// <summary>
        /// Flush the buffer.
        /// </summary>
        public void Flush()
        {
            buffer = new char[DisplaySize.Width, DisplaySize.Height];
            backgroundColors = new AnsiColor?[DisplaySize.Width, DisplaySize.Height];
            foregroundColors = new AnsiColor?[DisplaySize.Width, DisplaySize.Height];
        }

        /// <summary>
        /// Safe set a cell background.
        /// </summary>
        /// <param name="x">The x position of the cell.</param>
        /// <param name="y">The y position of the cell.</param>
        /// <param name="backgroundColor">The background color of the cell.</param>
        private void SafeSetCellBackground(int x, int y, AnsiColor backgroundColor)
        {
            if (IsCellSafe(x, y))
                backgroundColors[x, y] = backgroundColor;
        }

        /// <summary>
        /// Safe set a cell foreground.
        /// </summary>
        /// <param name="x">The x position of the cell.</param>
        /// <param name="y">The y position of the cell.</param>
        /// <param name="foregroundColor">The foreground color of the cell.</param>
        private void SafeSetCellForeground(int x, int y, AnsiColor foregroundColor)
        {
            if (IsCellSafe(x, y))
                foregroundColors[x, y] = foregroundColor;
        }

        /// <summary>
        /// Safe set a cell character.
        /// </summary>
        /// <param name="x">The x position of the cell.</param>
        /// <param name="y">The y position of the cell.</param>
        /// <param name="character">The character.</param>
        private void SafeSetCellCharacter(int x, int y, char character)
        {
            if (IsCellSafe(x, y))
                buffer[x, y] = character;
        }

        /// <summary>
        /// Determine if a cell is safe.
        /// </summary>
        /// <param name="x">The x position of the cell.</param>
        /// <param name="y">The y position of the cell.</param>
        /// <returns>True if the cell if safe, else false.</returns>
        private bool IsCellSafe(int x, int y)
        {
            return x >= 0 && x < DisplaySize.Width && y >= 0 && y < DisplaySize.Height;
        }

        /// <summary>
        /// Set a cell.
        /// </summary>
        /// <param name="x">The x position of the cell.</param>
        /// <param name="y">The y position of the cell.</param>
        /// <param name="backgroundColor">The backgroundColor color of the cell.</param>
        public void SetCell(int x, int y, AnsiColor backgroundColor)
        {
            SafeSetCellBackground(x, y, backgroundColor);
        }

        /// <summary>
        /// Set a cell.
        /// </summary>
        /// <param name="x">The x position of the cell.</param>
        /// <param name="y">The y position of the cell.</param>
        /// <param name="character">The character.</param>
        /// <param name="foregroundColor">The foreground color of the cell.</param>
        public void SetCell(int x, int y, char character, AnsiColor foregroundColor)
        {
            SafeSetCellCharacter(x, y, character);
            SafeSetCellForeground(x, y, foregroundColor);
        }

        /// <summary>
        /// Set a cell.
        /// </summary>
        /// <param name="x">The x position of the cell.</param>
        /// <param name="y">The y position of the cell.</param>
        /// <param name="character">The character.</param>
        /// <param name="foregroundColor">The foreground color of the cell.</param>
        /// <param name="backgroundColor">The backgroundColor color of the cell.</param>
        public void SetCell(int x, int y, char character, AnsiColor foregroundColor, AnsiColor backgroundColor)
        {
            SafeSetCellCharacter(x, y, character);
            SafeSetCellForeground(x, y, foregroundColor);
            SafeSetCellBackground(x, y, backgroundColor);
        }

        /// <summary>
        /// Draw a rectangle.
        /// </summary>
        /// <param name="left">The left position of the rectangle.</param>
        /// <param name="top">The top position of the rectangle.</param>
        /// <param name="width">The width of the rectangle.</param>
        /// <param name="height">The height of the rectangle.</param>
        /// <param name="borderColor">The border color of the cell.</param>
        /// <param name="fillColor">The fill color of the cell.</param>
        public void DrawRectangle(int left, int top, int width, int height, AnsiColor borderColor, AnsiColor fillColor)
        {
            for (var x = left; x < left + width; x++)
            {
                SafeSetCellBackground(x, top, borderColor);
                SafeSetCellBackground(x, top + height - 1, borderColor);
                SafeSetCellCharacter(x, top, ' ');
                SafeSetCellCharacter(x, top + height - 1, ' ');
            }

            for (var y = top; y < top + height; y++)
            {
                SafeSetCellBackground(left, y, borderColor);
                SafeSetCellBackground(left + width - 1, y, borderColor);
                SafeSetCellCharacter(left, y, ' ');
                SafeSetCellCharacter(left + width - 1, y, ' ');
            }

            for (var y = top + 1; y < top + height - 1; y++)
            {
                for (var x = left + 1; x < left + width - 1; x++)
                {
                    SafeSetCellBackground(x, y, fillColor);
                    SafeSetCellCharacter(x, y, ' ');
                }
            }
        }

        /// <summary>
        /// Draw a rectangle.
        /// </summary>
        /// <param name="left">The left position of the rectangle.</param>
        /// <param name="top">The top position of the rectangle.</param>
        /// <param name="width">The width of the rectangle.</param>
        /// <param name="height">The height of the rectangle.</param>
        /// <param name="character">The character to use for the texture.</param>
        /// <param name="foregroundColor">The foregroundColor color of the texture.</param>
        public void DrawTexture(int left, int top, int width, int height, char character, AnsiColor foregroundColor)
        {
            for (var y = top; y < top + height; y++)
            {
                for (var x = left; x < left + width; x++)
                {
                    SafeSetCellForeground(x, y, foregroundColor);
                    SafeSetCellCharacter(x, y, character);
                }
            }
        }

        #endregion
    }
}
