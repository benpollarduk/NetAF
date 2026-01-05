using NetAF.Assets;
using NetAF.Utilities;

namespace NetAF.Targets.Console.Rendering
{
    /// <summary>
    /// Provides a class for building strings as part of a grid.
    /// </summary>
    /// <param name="leftBoundaryCharacter">The character to use for left boundaries.</param>
    /// <param name="rightBoundaryCharacter">The character to use for right boundaries.</param>
    /// <param name="horizontalDividerCharacter">The character to use for horizontal dividers.</param>
    public class GridStringBuilder(char leftBoundaryCharacter = (char)124, char rightBoundaryCharacter = (char)124, char horizontalDividerCharacter = (char)45)
    {
        #region Fields

        private AnsiColor[,] colors;
        private char[,] buffer;

        #endregion

        #region Properties

        /// <summary>
        /// Get or set the character used for left boundary.
        /// </summary>
        public char LeftBoundaryCharacter { get; set; } = leftBoundaryCharacter;

        /// <summary>
        /// Get or set the character used for right boundary.
        /// </summary>
        public char RightBoundaryCharacter { get; set; } = rightBoundaryCharacter;

        /// <summary>
        /// Get or set the character used for horizontal dividers.
        /// </summary>
        public char HorizontalDividerCharacter { get; set; } = horizontalDividerCharacter;

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
        /// Get if an x and y location is within the bounds of the DisplaySize.
        /// </summary>
        /// <param name="x">The x location.</param>
        /// <param name="y">The y location.</param>
        /// <returns>True if within bounds, else false.</returns>
        private bool IsWithinBounds(int x, int y)
        {
            if (x < 0 || x >= DisplaySize.Width || y < 0 || y >= DisplaySize.Height)
                return false;

            return true;
        }

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
        /// Get a color for a cell.
        /// </summary>
        /// <param name="x">The x position of the cell.</param>
        /// <param name="y">The y position of the cell.</param>
        /// <returns>The cell color.</returns>
        public AnsiColor GetCellColor(int x, int y)
        {
            return colors[x, y];
        }

        /// <summary>
        /// Flush the buffer.
        /// </summary>
        public void Flush()
        {
            buffer = new char[DisplaySize.Width, DisplaySize.Height];
            colors = new AnsiColor[DisplaySize.Width, DisplaySize.Height];
        }

        /// <summary>
        /// Set a cell.
        /// </summary>
        /// <param name="x">The x position of the cell.</param>
        /// <param name="y">The y position of the cell.</param>
        /// <param name="character">The character.</param>
        /// <param name="color">The color of the character.</param>
        public void SetCell(int x, int y, char character, AnsiColor color)
        {
            if (!IsWithinBounds(x, y))
                return;

            buffer[x, y] = character;
            colors[x, y] = color;
        }

        /// <summary>
        /// Draw the boundary.
        /// </summary>
        /// <param name="color">The color to draw the boundary.</param>
        public void DrawBoundary(AnsiColor color)
        {
            DrawHorizontalDivider(0, color);
            DrawHorizontalDivider(DisplaySize.Height - 1, color);

            for (var i = 0; i < DisplaySize.Height; i++)
            {
                SetCell(0, i, LeftBoundaryCharacter, color);
                SetCell(DisplaySize.Width - 1, i, RightBoundaryCharacter, color);
            }
        }

        /// <summary>
        /// Draw a horizontal divider.
        /// </summary>
        /// <param name="y">The y position of the divider.</param>
        /// <param name="color">The color to draw the boundary.</param>
        /// <returns>The divider.</returns>
        public void DrawHorizontalDivider(int y, AnsiColor color)
        {
            for (var i = 1; i < DisplaySize.Width - 1; i++)
                SetCell(i, y, HorizontalDividerCharacter, color);
        }

        /// <summary>
        /// Draw a wrapped string.
        /// </summary>
        /// <param name="value">The string.</param>
        /// <param name="startX">The start x position.</param>
        /// <param name="startY">The start y position.</param>
        /// <param name="maxWidth">The max width of the string.</param>
        /// <param name="color">The color to draw the text.</param>
        /// <param name="endX">The end x position.</param>
        /// <param name="endY">The end y position.</param>
        public void DrawWrapped(string value, int startX, int startY, int maxWidth, AnsiColor color, out int endX, out int endY)
        {
            endX = startX;
            endY = startY;

            value = StringUtilities.PreenOutput(value);

            while (value.Length > 0)
            {
                var chunk = StringUtilities.CutLineFromParagraph(ref value, maxWidth - startX);

                for (var i = 0; i < chunk.Length; i++)
                {
                    endX = startX + i;
                    SetCell(endX, endY, chunk[i], color);
                }

                if (value.Trim().Length > 0)
                    endY++;

                if (endY >= DisplaySize.Height - 1)
                    break;
            }
        }

        /// <summary>
        /// Draw a wrapped string.
        /// </summary>
        /// <param name="value">The string.</param>
        /// <param name="startY">The start y position.</param>
        /// <param name="maxWidth">The max width of the string.</param>
        /// <param name="color">The color to draw the text.</param>
        /// <param name="endX">The end x position.</param>
        /// <param name="endY">The end y position.</param>
        public void DrawCentralisedWrapped(string value, int startY, int maxWidth, AnsiColor color, out int endX, out int endY)
        {
            endX = 0;
            endY = startY;

            value = StringUtilities.PreenOutput(value);

            while (value.Length > 0)
            {
                var chunk = StringUtilities.CutLineFromParagraph(ref value, maxWidth);
                var startX = maxWidth / 2 - chunk.Length / 2;

                for (var i = 0; i < chunk.Length; i++)
                {
                    endX = startX + i;
                    SetCell(endX, endY, chunk[i], color);
                }

                if (value.Trim().Length > 0)
                    endY++;
            }
        }

        /// <summary>
        /// Draw an underline.
        /// </summary>
        /// <param name="x">The position of the underline, in x.</param>
        /// <param name="y">The position of the underline, in y.</param>
        /// <param name="length">The length of the underline.</param>
        /// <param name="color">The color of the underline.</param>
        public void DrawUnderline(int x, int y, int length, AnsiColor color)
        {
            var underline = '-';

            for (var i = 0; i < length; i++)
                SetCell(x + i, y, underline, color);
        }

        /// <summary>
        /// Find the used width of this GridStringBuilder.
        /// </summary>
        /// <returns>The used width.</returns>
        private int FindUsedWidth()
        {
            var width = DisplaySize.Width;

            for (var column = DisplaySize.Width - 1; column >= 0; column--)
            {
                width = column + 1;
                var used = false;

                for (var row = 0; row < DisplaySize.Height; row++)
                {
                    if (GetCharacter(column, row) != 0)
                    {
                        used = true;
                        break;
                    }
                }

                if (used)
                    break;
            }

            return width;
        }

        /// <summary>
        /// Find the used height of this GridStringBuilder.
        /// </summary>
        /// <returns>The used height.</returns>
        private int FindUsedHeight()
        {
            var height = DisplaySize.Height;

            for (var row = DisplaySize.Height - 1; row >= 0; row--)
            {
                height = row + 1;
                var used = false;

                for (var column = 0; column < DisplaySize.Width; column++)
                {
                    if (GetCharacter(column, row) != 0)
                    {
                        used = true;
                        break;
                    }
                }

                if (used)
                    break;
            }

            return height;
        }

        /// <summary>
        /// Crop this GridStringBuilder so that a duplicate is returned that is sized to only take up the used required width and height.
        /// </summary>
        /// <param name="cropWidth">Specify if the width should be cropped.</param>
        /// <param name="cropHeight">Specify if the height should be cropped.</param>
        /// <returns>A duplicate, cropped, GridStringBuilder.</returns>
        public GridStringBuilder ToCropped(bool cropWidth = true, bool cropHeight = true)
        {
            // determine width and height
            var width = cropWidth ? FindUsedWidth() : DisplaySize.Width;
            var height = cropHeight ? FindUsedHeight() : DisplaySize.Height;

            // create duplicate 
            GridStringBuilder duplicate = new();
            duplicate.Resize(new(width, height));

            for (var row = 0; row < height; row++)
                for (var column = 0; column < width; column++)
                    duplicate.SetCell(column, row, GetCharacter(column, row), GetCellColor(column, row));

            return duplicate;
        }

        #endregion

        #region StaticMethods

        /// <summary>
        /// Get the number of lines a string will take up.
        /// </summary>
        /// <param name="value">The string.</param>
        /// <param name="startY">The start y position.</param>
        /// <param name="maxWidth">The max width of the string.</param>
        /// <returns>The number of lines the string will take up.</returns>
        public static int GetNumberOfLines(string value, int startY, int maxWidth)
        {
            var endY = startY;
            var copy = value.Clone().ToString();

            while (copy.Length > 0)
            {
                StringUtilities.CutLineFromParagraph(ref copy, maxWidth);

                if (copy.Trim().Length > 0)
                    endY++;
            }

            return endY - startY + 1;
        }

        #endregion
    }
}
