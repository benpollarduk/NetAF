using NetAF.Utilities;
using System.Linq;
using System.Text;

namespace NetAF.Targets.Console.Rendering
{
    /// <summary>
    /// Provides texture made from a 2D array of characters.
    /// </summary>
    /// <param name="texture">The texture as a string. The string will be split at newlines to create the 2D texture.</param>
    public class Texture(string texture)
    {
        #region Fields

        private char[,] kernel;

        #endregion

        #region Properties

        /// <summary>
        /// Get a character from the texture.
        /// </summary>
        /// <param name="x">The x position of the character within the texture.</param>
        /// <param name="y">The y position of the character within the texture.</param>
        /// <returns>The character. If the specified position is outside of the kernel then ' ' will be returned.</returns>
        public char this[int x, int y] => IsPositionInKernel(x, y) ? Kernel[x, y] : ' ';

        /// <summary>
        /// Get the kernel, as a 2D array of characters.
        /// </summary>
        public char[,] Kernel
        {
            get
            {
                if (kernel == null)
                    CreateKernel(texture);

                return kernel;
            }
        }

        /// <summary>
        /// Get the width of the texture.
        /// </summary>
        public int Width => Kernel.GetLength(0);

        /// <summary>
        /// Get the height of the texture.
        /// </summary>
        public int Height => Kernel.GetLength(1);

        #endregion

        #region Methods

        /// <summary>
        /// Get if a position is within the kernel.
        /// </summary>
        /// <param name="x">The x position.</param>
        /// <param name="y">The y position.</param>
        /// <returns>True if the position is within the kernel, else </returns>
        private bool IsPositionInKernel(int x, int y)
        {
            return x >= 0 && x < Width && y >= 0 && y < Height;
        }

        /// <summary>
        /// Create the kernel.
        /// </summary>
        /// <param name="texture">The texture to create the kernel from. The string will be split at newlines to create the 2D texture.</param>
        private void CreateKernel(string texture)
        {
            var lines = texture.Split(StringUtilities.Newline);
            int width = lines.Max(x => x.Length);
            int height = lines.Length;
            kernel = new char[width, height];

            for (var y = 0; y < height; y++)
                for (var x = 0; x < width; x++)
                    kernel[x, y] = x < lines[y].Length ? lines[y][x] : ' ';
        }

        #endregion

        #region Overrides of Object

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            StringBuilder builder = new();

            for (var y = 0; y < Height; y++)
            {
                for (var x = 0; x < Width; x++)
                {
                    if (IsPositionInKernel(x, y))
                        builder.Append(this[x, y]);
                }

                if (y < Height - 1)
                    builder.Append(StringUtilities.Newline);
            }

            return builder.ToString();
        }

        #endregion
    }
}
