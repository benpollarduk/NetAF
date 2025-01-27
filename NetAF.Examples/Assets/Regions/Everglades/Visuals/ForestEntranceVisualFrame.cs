using NetAF.Assets;
using NetAF.Extensions;
using NetAF.Rendering;
using NetAF.Targets.Console.Rendering;
using NetAF.Targets.Console.Rendering.FrameBuilders;
using NetAF.Utilities;

namespace NetAF.Examples.Assets.Regions.Everglades.Visuals
{
    internal class ForestEntranceVisualFrame(string name, Size size) : IAssetTemplate<IFrame>
    {
        #region StaticProperties

        public static readonly AnsiColor Sky = new(20, 20, 125);
        public static readonly AnsiColor Trunk = new(127, 50, 50);
        public static readonly AnsiColor DarkTrunk = new(120, 40, 40);
        public static readonly AnsiColor Canopy = new(50, 200, 50);
        public static readonly AnsiColor DarkCanopy = new(30, 150, 20);
        public static readonly AnsiColor Grass = new(20, 220, 50);
        public static readonly AnsiColor GrassHighlights = new(0, 235, 0);
        public static readonly AnsiColor Path = new(130, 20, 20);
        public static readonly AnsiColor PathHighlights = new(130, 130, 130);

        #endregion

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
            builder.DrawRectangle(0, 25, 80, 25, Grass, Grass);
            builder.DrawTextureOverBackgroundColor(0, 25, 80, 25, Grass, "^  v . ' :\n  # ~ ^ . ~ '".ToTexture(), GrassHighlights);
        }

        private static void DrawTree1(GridVisualBuilder builder, int x, int y)
        {
            builder.SetCell(x + 5, y, Canopy);
            builder.DrawRectangle(x + 4, y + 1, 3, 1, Canopy, Canopy);
            builder.DrawRectangle(x + 3, y + 2, 5, 1, Canopy, Canopy);
            builder.DrawRectangle(x + 2, y + 3, 7, 1, Canopy, Canopy);
            builder.DrawRectangle(x + 1, y + 4, 9, 1, Canopy, Canopy);
            builder.DrawRectangle(x, y + 5, 11, 1, Canopy, Canopy);
            builder.DrawRectangle(x + 5, y + 5, 1, 6, Trunk, Trunk);
        }

        private static void DrawTree2(GridVisualBuilder builder, int x, int y)
        {
            builder.SetCell(x + 5, y, DarkCanopy);
            builder.DrawRectangle(x + 3, y + 1, 5, 1, DarkCanopy, DarkCanopy);
            builder.DrawRectangle(x + 3, y + 2, 6, 1, DarkCanopy, DarkCanopy);
            builder.DrawRectangle(x + 2, y + 3, 7, 1, DarkCanopy, DarkCanopy);
            builder.DrawRectangle(x + 1, y + 4, 9, 1, DarkCanopy, DarkCanopy);
            builder.DrawRectangle(x, y + 5, 11, 1, DarkCanopy, DarkCanopy);
            builder.DrawRectangle(x + 5, y + 5, 1, 6, DarkTrunk, DarkTrunk);
        }

        private static void DrawPath(GridVisualBuilder builder)
        {
            builder.DrawRectangle(35, 25, 10, 25, Path, Path);
            builder.DrawRectangle(33, 28, 2, 10, Path, Path);
            builder.DrawRectangle(32, 31, 3, 10, Path, Path);
            builder.DrawRectangle(43, 29, 4, 11, Path, Path);
            builder.DrawTextureOverBackgroundColor(30, 25, 20, 25, Path, "@ , } ., @~.+\n  .% :;  @\n+- { $ '#".ToTexture(), PathHighlights);
        }

        #endregion

        #region Implementation of IAssetTemplate<IFrame>

        /// <summary>
        /// Instantiate a new instance of the asset.
        /// </summary>
        /// <returns>The asset.</returns>
        public IFrame Instantiate()
        {
            var builder = new GridVisualBuilder(Sky, AnsiColor.BrightWhite);
            builder.Resize(new(size.Width - 4, size.Height - 10));
            DrawSun(builder);
            DrawGrass(builder);
            DrawPath(builder);
            DrawTree1(builder, 5, 15);
            DrawTree2(builder, 20, 15);
            DrawTree1(builder, 35, 15);
            DrawTree1(builder, 50, 15);
            DrawTree2(builder, 65, 15);
            DrawTree1(builder, 2, 16);
            DrawTree2(builder, 12, 18);
            DrawTree1(builder, 28, 17);
            DrawTree1(builder, 39, 16);
            DrawTree2(builder, 56, 18);
            DrawTree1(builder, 70, 17);

            var frameBuilder = new ConsoleVisualFrameBuilder(new GridStringBuilder());
            return frameBuilder.Build(name, string.Empty, builder, size);
        }

        #endregion
    }
}
