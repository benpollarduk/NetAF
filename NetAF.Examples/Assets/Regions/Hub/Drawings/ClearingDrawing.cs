using NetAF.Assets;
using NetAF.Rendering.Console;
using NetAF.Utilities;

namespace NetAF.Examples.Assets.Regions.Hub.Drawings
{
    internal class ClearingDrawing(Size size) : IAssetTemplate<GridPictureFrame>
    {
        private static void DrawSun(GridPictureBuilder builder)
        {
            builder.SetCell(8, 5, AnsiColor.BrightYellow);
            builder.SetCell(4, 7, AnsiColor.BrightYellow);
            builder.SetCell(5, 7, AnsiColor.BrightYellow);
            builder.SetCell(11, 7, AnsiColor.BrightYellow);
            builder.SetCell(12, 7, AnsiColor.BrightYellow);
            builder.SetCell(8, 9, AnsiColor.BrightYellow);
            builder.DrawRectangle(6, 6, 5, 3, AnsiColor.BrightYellow, AnsiColor.BrightYellow);
            builder.DrawTexture(6, 6, 5, 3, ':', AnsiColor.Yellow);
        }

        private static void DrawGrass(GridPictureBuilder builder)
        {
            builder.DrawRectangle(0, 25, 80, 25, AnsiColor.BrightGreen, AnsiColor.BrightGreen);
            builder.DrawTexture(0, 25, 80, 25, '#', AnsiColor.Green);
        }

        private static void DrawTree(GridPictureBuilder builder, int x, int y)
        {
            builder.SetCell(x + 5, y, AnsiColor.Green);
            builder.DrawRectangle(x + 4, y + 1, 3, 1, AnsiColor.Green, AnsiColor.Green);
            builder.DrawRectangle(x + 3, y + 2, 5, 1, AnsiColor.Green, AnsiColor.Green);
            builder.DrawRectangle(x + 2, y + 3, 7, 1, AnsiColor.Green, AnsiColor.Green);
            builder.DrawRectangle(x + 1, y + 4, 9, 1, AnsiColor.Green, AnsiColor.Green);
            builder.DrawRectangle(x, y + 5, 11, 1, AnsiColor.Green, AnsiColor.Green);
            builder.DrawRectangle(x + 5, y + 5, 1, 6, AnsiColor.Red, AnsiColor.Red);
        }

        public GridPictureFrame Instantiate()
        {
            var builder = new GridPictureBuilder(AnsiColor.Blue, AnsiColor.BrightWhite);
            builder.Resize(size);
            DrawSun(builder);
            DrawGrass(builder);
            DrawTree(builder, 5, 15);
            DrawTree(builder, 20, 15);
            DrawTree(builder, 35, 15);
            DrawTree(builder, 50, 15);
            DrawTree(builder, 65, 15);
            DrawTree(builder, 2, 16);
            DrawTree(builder, 12, 18);
            DrawTree(builder, 28, 17);
            DrawTree(builder, 39, 16);
            DrawTree(builder, 56, 18);
            DrawTree(builder, 70, 17);
            return new GridPictureFrame(builder);
        }
    }
}
