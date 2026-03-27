using NetAF.Assets;
using NetAF.Utilities;
using System;
using System.IO;
using System.Text;

namespace NetAF.Targets.Console.Rendering
{
    /// <summary>
    /// Provides a class for building visuals in a grid.
    /// </summary>
    /// <param name="background">The background color.</param>
    /// <param name="foreground">The foreground color.</param>
    public class GridVisualBuilder(AnsiColor background, AnsiColor foreground)
    {
        #region Constants

        private const string Null = "null";
        private const char Delimiter = ',';

        #endregion

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
        /// Draw a border.
        /// </summary>
        /// <param name="left">The left position of the border.</param>
        /// <param name="top">The top position of the border.</param>
        /// <param name="width">The width of the border.</param>
        /// <param name="height">The height of the border.</param>
        /// <param name="borderColor">The border color of the cell.</param>
        public void DrawBorder(int left, int top, int width, int height, AnsiColor borderColor)
        {
            for (var x = left; x < left + width; x++)
            {
                SafeSetCellBackground(x, top, borderColor);
                SafeSetCellBackground(x, top + height - 1, borderColor);
            }

            for (var y = top + 1; y < top + height - 1; y++)
            {
                SafeSetCellBackground(left, y, borderColor);
                SafeSetCellBackground(left + width - 1, y, borderColor);
            }
        }

        /// <summary>
        /// Draw a circular border.
        /// </summary>
        /// <param name="centerX">The center of the circle, x.</param>
        /// <param name="centerY">The center of the circle, y.</param>
        /// <param name="radius">The radius of the circle.</param>
        /// <param name="borderColor">The border color of the circle.</param>
        public void DrawBorder(int centerX, int centerY, int radius, AnsiColor borderColor)
        {
            var d = (5 - radius * 4) / 4;
            var x = 0;
            var y = radius;

            do
            {
                SafeSetCellBackground(centerX + x, centerY + y, borderColor);
                SafeSetCellBackground(centerX + x, centerY - y, borderColor);
                SafeSetCellBackground(centerX - x, centerY + y, borderColor);
                SafeSetCellBackground(centerX - x, centerY - y, borderColor);
                SafeSetCellBackground(centerX + y, centerY + x, borderColor);
                SafeSetCellBackground(centerX + y, centerY - x, borderColor);
                SafeSetCellBackground(centerX - y, centerY + x, borderColor);
                SafeSetCellBackground(centerX - y, centerY - x, borderColor);

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
        public void DrawCircle(int centerX, int centerY, int radius, AnsiColor borderColor, AnsiColor fillColor)
        {
            // draw filled circle
            for (var y_rel = -radius; y_rel <= radius; y_rel++)
            {
                var x_extent = (int)Math.Sqrt(radius * radius - y_rel * y_rel);
                for (var x_rel = -x_extent; x_rel <= x_extent; x_rel++)
                {
                    SafeSetCellBackground(centerX + x_rel, centerY + y_rel, fillColor);
                    SafeSetCellCharacter(centerX + x_rel, centerY + y_rel, ' ');
                }
            }

            // draw border
            DrawBorder(centerX, centerY, radius, borderColor);
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

        #region Overrides of Object

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.AppendLine($"{DisplaySize.Width},{DisplaySize.Height}");
            builder.AppendLine($"{background},{foreground}");

            var charBuffer = new char[DisplaySize.Width * DisplaySize.Height];

            for (var y = 0; y < DisplaySize.Height; y++)
                for (var x = 0; x < DisplaySize.Width; x++)
                    charBuffer[y * DisplaySize.Width + x] = buffer[x, y];

            builder.AppendLine(Convert.ToBase64String(Encoding.UTF8.GetBytes(charBuffer)));

            for (var y = 0; y < DisplaySize.Height; y++)
            {
                for (var x = 0; x < DisplaySize.Width; x++)
                {
                    builder.Append(foregroundColors[x, y]?.ToString() ?? Null);

                    if (x < DisplaySize.Width - 1)
                    {
                        builder.Append(Delimiter);
                    }
                }

                builder.AppendLine();
            }

            for (var y = 0; y < DisplaySize.Height; y++)
            {
                for (var x = 0; x < DisplaySize.Width; x++)
                {
                    builder.Append(backgroundColors[x, y]?.ToString() ?? Null);

                    if (x < DisplaySize.Width - 1)
                        builder.Append(Delimiter);
                }

                builder.AppendLine();
            }

            return builder.ToString();
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
        /// </summary>
        /// <param name="data">The string representation of the GridVisualBuilder.</param>
        /// <returns>A new GridVisualBuilder instance.</returns>
        public static GridVisualBuilder FromString(string data)
        {
            using var reader = new StringReader(data);

            var sizeLine = reader.ReadLine().Split(Delimiter);
            var width = int.Parse(sizeLine[0]);
            var height = int.Parse(sizeLine[1]);

            var colorLine = reader.ReadLine().Split(Delimiter);
            var defaultBackground = AnsiColor.FromString(colorLine[0]);
            var defaultForeground = AnsiColor.FromString(colorLine[1]);

            var builder = new GridVisualBuilder(defaultBackground, defaultForeground);
            builder.Resize(new Size(width, height));

            var base64Chars = reader.ReadLine();
            var charBytes = Convert.FromBase64String(base64Chars);
            var chars = Encoding.UTF8.GetChars(charBytes);

            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    builder.buffer[x, y] = chars[y * width + x];
                }
            }

            for (var y = 0; y < height; y++)
            {
                var line = reader.ReadLine();

                if (line == null)
                    continue;

                var colors = line.Split(Delimiter);

                for (var x = 0; x < width; x++)
                {
                    if (colors[x] != Null)
                        builder.foregroundColors[x, y] = AnsiColor.FromString(colors[x]);
                }
            }

            for (var y = 0; y < height; y++)
            {
                var line = reader.ReadLine();

                if (line == null)
                    continue;

                var colors = line.Split(Delimiter);

                for (var x = 0; x < width; x++)
                {
                    if (colors[x] != Null)
                        builder.backgroundColors[x, y] = AnsiColor.FromString(colors[x]);
                }
            }

            return builder;
        }

        #endregion
    }
}
