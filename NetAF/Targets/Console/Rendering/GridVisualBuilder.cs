using NetAF.Assets;
using NetAF.Utilities;
using System;
using System.Text;

namespace NetAF.Targets.Console.Rendering
{
    /// <summary>
    /// Provides a class for building visuals in a grid.
    /// </summary>
    /// <param name="background">The background color.</param>
    /// <param name="foreground">The foreground color.</param>
    public sealed class GridVisualBuilder(AnsiColor background, AnsiColor foreground)
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
        /// <param name="borderColorVarier">An option variation generator to use for the border color.</param>
        /// <param name="fillColorVarier">An option variation generator to use for the fill color.</param>
        public void DrawRectangle(int left, int top, int width, int height, AnsiColor borderColor, AnsiColor fillColor, IVariationGenerator borderColorVarier = null, IVariationGenerator fillColorVarier = null)
        {
            for (var x = left; x < left + width; x++)
            {
                SafeSetCellBackground(x, top, borderColorVarier?.Vary(borderColor) ?? borderColor);
                SafeSetCellBackground(x, top + height - 1, borderColorVarier?.Vary(borderColor) ?? borderColor);
                SafeSetCellCharacter(x, top, ' ');
                SafeSetCellCharacter(x, top + height - 1, ' ');
            }

            for (var y = top; y < top + height; y++)
            {
                SafeSetCellBackground(left, y, borderColorVarier?.Vary(borderColor) ?? borderColor);
                SafeSetCellBackground(left + width - 1, y, borderColorVarier?.Vary(borderColor) ?? borderColor);
                SafeSetCellCharacter(left, y, ' ');
                SafeSetCellCharacter(left + width - 1, y, ' ');
            }

            for (var y = top + 1; y < top + height - 1; y++)
            {
                for (var x = left + 1; x < left + width - 1; x++)
                {
                    SafeSetCellBackground(x, y, fillColorVarier?.Vary(fillColor) ?? fillColor);
                    SafeSetCellCharacter(x, y, ' ');
                }
            }
        }

        /// <summary>
        /// Draw a border.
        /// </summary>
        /// <param name="left">The left position of the border.</param>
        /// <param name="top">The top position of the border.</param>
        /// <param name="width">The width of the border.</param>
        /// <param name="height">The height of the border.</param>
        /// <param name="borderColor">The border color of the cell.</param>
        /// <param name="borderColorVarier">An option variation generator to use for the border color.</param>
        public void DrawBorder(int left, int top, int width, int height, AnsiColor borderColor, IVariationGenerator borderColorVarier = null)
        {
            for (var x = left; x < left + width; x++)
            {
                SafeSetCellBackground(x, top, borderColorVarier?.Vary(borderColor) ?? borderColor);
                SafeSetCellBackground(x, top + height - 1, borderColorVarier?.Vary(borderColor) ?? borderColor);
            }

            for (var y = top + 1; y < top + height - 1; y++)
            {
                SafeSetCellBackground(left, y, borderColorVarier?.Vary(borderColor) ?? borderColor);
                SafeSetCellBackground(left + width - 1, y, borderColorVarier?.Vary(borderColor) ?? borderColor);
            }
        }

        /// <summary>
        /// Draw a circular border.
        /// </summary>
        /// <param name="centerX">The center of the circle, x.</param>
        /// <param name="centerY">The center of the circle, y.</param>
        /// <param name="radius">The radius of the circle.</param>
        /// <param name="borderColor">The border color of the circle.</param>
        /// <param name="borderColorVarier">An option variation generator to use for the border color.</param>
        public void DrawBorder(int centerX, int centerY, int radius, AnsiColor borderColor, IVariationGenerator borderColorVarier = null)
        {
            var d = (5 - radius * 4) / 4;
            var x = 0;
            var y = radius;

            do
            {
                SafeSetCellBackground(centerX + x, centerY + y, borderColorVarier?.Vary(borderColor) ?? borderColor);
                SafeSetCellBackground(centerX + x, centerY - y, borderColorVarier?.Vary(borderColor) ?? borderColor);
                SafeSetCellBackground(centerX - x, centerY + y, borderColorVarier?.Vary(borderColor) ?? borderColor);
                SafeSetCellBackground(centerX - x, centerY - y, borderColorVarier?.Vary(borderColor) ?? borderColor);
                SafeSetCellBackground(centerX + y, centerY + x, borderColorVarier?.Vary(borderColor) ?? borderColor);
                SafeSetCellBackground(centerX + y, centerY - x, borderColorVarier?.Vary(borderColor) ?? borderColor);
                SafeSetCellBackground(centerX - y, centerY + x, borderColorVarier?.Vary(borderColor) ?? borderColor);
                SafeSetCellBackground(centerX - y, centerY - x, borderColorVarier?.Vary(borderColor) ?? borderColor);

                if (d < 0)
                {
                    d += 2 * x + 1;
                }
                else
                {
                    d += 2 * (x - y) + 1;
                    y--;
                }

                x++;

            } while (x <= y);
        }

        /// <summary>
        /// Draw a texture.
        /// </summary>
        /// <param name="left">The left position of the area to draw within.</param>
        /// <param name="top">The top position of the area to draw within.</param>
        /// <param name="width">The width of the area to draw within.</param>
        /// <param name="height">The height of the area to draw within.</param>
        /// <param name="texture">The texture.</param>
        /// <param name="foregroundColor">The foregroundColor color of the texture.</param>
        public void DrawTexture(int left, int top, int width, int height, Texture texture, AnsiColor foregroundColor)
        {
            for (var y = top; y < top + height; y++)
            {
                for (var x = left; x < left + width; x++)
                {
                    SafeSetCellForeground(x, y, foregroundColor);
                    SafeSetCellCharacter(x, y, GetCharacterFromTexture(x, y, texture));
                }
            }
        }

        /// <summary>
        /// Draw a texture over all cells where the background color matches the specified color.
        /// </summary>
        /// <param name="left">The left position of the area to draw within.</param>
        /// <param name="top">The top position of the area to draw within.</param>
        /// <param name="width">The width of the area to draw within.</param>
        /// <param name="height">The height of the area to draw within.</param>
        /// <param name="backgroundColor">The background color.</param>
        /// <param name="texture">The texture.</param>
        /// <param name="foregroundColor">The foregroundColor color of the texture.</param>
        public void DrawTextureOverBackgroundColor(int left, int top, int width, int height, AnsiColor backgroundColor, Texture texture, AnsiColor foregroundColor)
        {
            for (var y = top; y < top + height; y++)
            {
                for (var x = left; x < left + width; x++)
                {
                    if (IsCellSafe(x, y) && GetCellBackgroundColor(x, y) == backgroundColor)
                    {
                        SafeSetCellForeground(x, y, foregroundColor);
                        SafeSetCellCharacter(x, y, GetCharacterFromTexture(x, y, texture));
                    }
                }
            }
        }

        /// <summary>
        /// Draw text.
        /// </summary>
        /// <param name="left">The left position of the text.</param>
        /// <param name="top">The top position of the text.</param>
        /// <param name="text">The text.</param>
        /// <param name="foregroundColor">The foreground color of the text.</param>
        public void DrawText(int left, int top, string text, AnsiColor foregroundColor)
        {
            for (var x = 0; x < text.Length; x++)
            {
                SafeSetCellCharacter(left + x, top, text[x]);
                SafeSetCellForeground(left + x, top, foregroundColor);
            }
        }

        /// <summary>
        /// Draw a filled circle with a border.
        /// </summary>
        /// <param name="centerX">The center of the circle, x.</param>
        /// <param name="centerY">The center of the circle, y.</param>
        /// <param name="radius">The radius of the circle.</param>
        /// <param name="borderColor">The border color of the circle.</param>
        /// <param name="fillColor">The fill color of the circle.</param>
        /// <param name="borderColorVarier">An option variation generator to use for the border color.</param>
        /// <param name="fillColorVarier">An option variation generator to use for the fill color.</param>
        public void DrawCircle(int centerX, int centerY, int radius, AnsiColor borderColor, AnsiColor fillColor, IVariationGenerator borderColorVarier = null, IVariationGenerator fillColorVarier = null)
        {
            // draw filled circle
            for (var y_rel = -radius; y_rel <= radius; y_rel++)
            {
                var x_extent = (int)Math.Sqrt(radius * radius - y_rel * y_rel);

                for (var x_rel = -x_extent; x_rel <= x_extent; x_rel++)
                {
                    SafeSetCellBackground(centerX + x_rel, centerY + y_rel, fillColorVarier?.Vary(fillColor) ?? fillColor);
                    SafeSetCellCharacter(centerX + x_rel, centerY + y_rel, ' ');
                }
            }

            // draw border
            DrawBorder(centerX, centerY, radius, borderColor, borderColorVarier);
        }

        /// <summary>
        /// Overlay another GridVisualBuilder on top of this.
        /// </summary>
        /// <param name="x">The x position to begin overlaying the GridVisualBuilder at.</param>
        /// <param name="y">The y position to begin overlaying the GridVisualBuilder at.</param>
        /// <param name="gridGridVisualBuilder">The GridVisualBuilder to overlay.</param>
        public void Overlay(int x, int y, GridVisualBuilder gridGridVisualBuilder)
        {
            for (var top = 0; top < gridGridVisualBuilder.DisplaySize.Height; top++)
            {
                for (var left = 0; left < gridGridVisualBuilder.DisplaySize.Width; left++)
                {
                    var transposedX = left + x;
                    var transposedY = top + y;
                    SafeSetCellBackground(transposedX, transposedY, gridGridVisualBuilder.GetCellBackgroundColor(left, top));
                    SafeSetCellForeground(transposedX, transposedY, gridGridVisualBuilder.GetCellForegroundColor(left, top));
                    SafeSetCellCharacter(transposedX, transposedY, gridGridVisualBuilder.GetCharacter(left, top));
                }
            }
        }

        /// <summary>
        /// Overlay a GridStringBuilder on top of this.
        /// </summary>
        /// <param name="x">The x position to begin overlaying the GridStringBuilder at.</param>
        /// <param name="y">The y position to begin overlaying the GridStringBuilder at.</param>
        /// <param name="gridStringBuilder">The GridStringBuilder to overlay.</param>
        public void Overlay(int x, int y, GridStringBuilder gridStringBuilder)
        {
            for (var top = 0; top < gridStringBuilder.DisplaySize.Height; top++)
            {
                for (var left = 0; left < gridStringBuilder.DisplaySize.Width; left++)
                {
                    var transposedX = left + x;
                    var transposedY = top + y;
                    SafeSetCellForeground(transposedX, transposedY, gridStringBuilder.GetCellColor(left, top));
                    SafeSetCellCharacter(transposedX, transposedY, gridStringBuilder.GetCharacter(left, top));
                }
            }
        }

        #endregion

        #region StaticMethods

        /// <summary>
        /// Get a character to use from a texture at a specified position within a region of interest.
        /// </summary>
        /// <param name="x">The x position within the region of interest.</param>
        /// <param name="y">The y position within the region of interest.</param>
        /// <param name="texture">The texture.</param>
        /// <returns>The character from the texture to use at the specified position.</returns>
        private static char GetCharacterFromTexture(int x, int y, Texture texture)
        {
            var textureX = x % texture.Width;
            var textureY = y % texture.Height;
            return texture[textureX, textureY];
        }

        /// <summary>
        /// Creates a GridVisualBuilder from its string representation.
        /// Each cell is represented in hex as RRGGBBRRGGBBCC where 
        /// RRGGBB is the background color,
        /// RRGGBB is the foreground color and
        /// CC is the character. 
        /// Each line is terminated with StringUtilities.Newline.
        /// </summary>
        /// <param name="data">The string representation of the GridVisualBuilder.</param>
        /// <returns>A new GridVisualBuilder instance.</returns>
        public static GridVisualBuilder FromString(string data)
        {
            var lines = data.Split(StringUtilities.Newline, StringSplitOptions.RemoveEmptyEntries);

            // each cell is represented in hex as RRGGBBRRGGBBCC where
            // RRGGBB is the background color
            // RRGGBB is the foreground color
            // CC is the character
            const int cellRepresentationLength = 14;

            var height = lines.Length;
            var width = lines[0].Length / cellRepresentationLength;

            var builder = new GridVisualBuilder(AnsiColor.Black, AnsiColor.White);
            builder.Resize(new Size(width, height));

            for (var y = 0; y < height; y++)
            {
                var line = lines[y];

                for (var x = 0; x < width; x++)
                {
                    var cellRepresentation = line.Substring(x * cellRepresentationLength, cellRepresentationLength);

                    var backgroundColorHex = cellRepresentation[..6];
                    var foregroundColorHex = cellRepresentation[6..12];
                    var characterHex = cellRepresentation[12..14];

                    var backgroundColor = AnsiColor.FromString(backgroundColorHex);
                    var foregroundColor = AnsiColor.FromString(foregroundColorHex);
                    var character = (char)Convert.ToInt32(characterHex, 16);

                    builder.SetCell(x, y, character, foregroundColor, backgroundColor);
                }
            }

            return builder;
        }

        #endregion

        #region Overrides of Object

        /// <inheritdoc/>
        public override string ToString()
        {
            var builder = new StringBuilder();

            for (var y = 0; y < DisplaySize.Height; y++)
            {
                for (var x = 0; x < DisplaySize.Width; x++)
                {
                    builder.Append(GetCellBackgroundColor(x, y).ToString());
                    builder.Append(GetCellForegroundColor(x, y).ToString());
                    builder.Append($"{Convert.ToInt32(buffer[x, y]):X2}");
                }

                builder.Append(StringUtilities.Newline);
            }

            return builder.ToString();
        }

        #endregion
    }
}
