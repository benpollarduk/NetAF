using NetAF.Assets;
using NetAF.Extensions;
using NetAF.Rendering;
using NetAF.Rendering.Console;
using NetAF.Rendering.Console.FrameBuilders;
using NetAF.Utilities;

namespace NetAF.Examples.Assets.Regions.Hub.Drawings
{
    internal class ClearingVisualFrame(string name, Size size) : IAssetTemplate<IFrame>
    {
        #region StaticMethods

        private static void DrawSun(GridVisualBuilder builder)
        {
            builder.SetCell(8, 5, AnsiColor.BrightYellow);
            builder.SetCell(4, 7, AnsiColor.BrightYellow);
            builder.SetCell(5, 7, AnsiColor.BrightYellow);
            builder.SetCell(11, 7, AnsiColor.BrightYellow);
            builder.SetCell(12, 7, AnsiColor.BrightYellow);
            builder.SetCell(8, 9, AnsiColor.BrightYellow);
            builder.DrawRectangle(6, 6, 5, 3, AnsiColor.BrightYellow, AnsiColor.BrightYellow);
            builder.DrawTexture(6, 6, 5, 3, ":".ToTexture(), AnsiColor.Yellow);
        }

        private static void DrawGrass(GridVisualBuilder builder)
        {
            builder.DrawRectangle(0, 25, 80, 25, AnsiColor.BrightGreen, AnsiColor.BrightGreen);
            builder.DrawTexture(0, 25, 80, 25, "^  v . ' :\n  # ~ ^ . : '".ToTexture(), AnsiColor.Green);
        }

        private static void DrawTree(GridVisualBuilder builder, int x, int y)
        {
            builder.SetCell(x + 5, y, AnsiColor.Green);
            builder.DrawRectangle(x + 4, y + 1, 3, 1, AnsiColor.Green, AnsiColor.Green);
            builder.DrawRectangle(x + 3, y + 2, 5, 1, AnsiColor.Green, AnsiColor.Green);
            builder.DrawRectangle(x + 2, y + 3, 7, 1, AnsiColor.Green, AnsiColor.Green);
            builder.DrawRectangle(x + 1, y + 4, 9, 1, AnsiColor.Green, AnsiColor.Green);
            builder.DrawRectangle(x, y + 5, 11, 1, AnsiColor.Green, AnsiColor.Green);
            builder.DrawRectangle(x + 5, y + 5, 1, 6, AnsiColor.Red, AnsiColor.Red);
        }

        #endregion

        #region Implementation of IAssetTemplate<IFrame>

        /// <summary>
        /// Instantiate a new instance of the asset.
        /// </summary>
        /// <returns>The asset.</returns>
        public IFrame Instantiate()
        {
            var builder = new GridVisualBuilder(AnsiColor.Blue, AnsiColor.BrightWhite);
            builder.Resize(new(size.Width - 4, size.Height - 10));
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

            var frameBuilder = new ConsoleVisualFrameBuilder(new GridStringBuilder());

            return frameBuilder.Build(name, string.Empty, builder, size);
        }

        #endregion
    }
}
