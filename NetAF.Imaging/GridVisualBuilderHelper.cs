using NetAF.Assets;
using NetAF.Rendering.Console;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace NetAF.Imaging
{
    /// <summary>
    /// Provides helper functions for GridVisualBuilder.
    /// </summary>
    public static class GridVisualBuilderHelper
    {
        #region StaticMethods

        /// <summary>
        /// Create a new GridVisualBuilder from an image.
        /// </summary>
        /// <param name="path">The path to the image.</param>
        /// <param name="size">The target size of the GridVisualBuilder.</param>
        /// <returns>An approximation of the image as a GridVisualBuilder.</returns>
        public static GridVisualBuilder FromImage(string path, Assets.Size size)
        {
            Rgba32[] pixelArray = new Rgba32[size.Width * size.Height];

            using var image = Image.Load<Rgba32>(path);
            image.Mutate(x => x.Resize(size.Width, size.Height));
            image.CopyPixelDataTo(pixelArray);

            GridVisualBuilder builder = new(AnsiColor.Black, AnsiColor.White);
            builder.Resize(size);

            for (var y = 0; y < size.Height; y++)
            {
                for (var x = 0; x < size.Width; x++)
                {
                    Rgba32 pixel = pixelArray[y * size.Width + x];
                    builder.SetCell(x, y, new(pixel.R, pixel.G, pixel.B));
                }
            }

            return builder;
        }

        #endregion;
    }
}
