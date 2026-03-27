using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Targets.Console.Rendering;

namespace NetAF.Tests.Targets.Console.Rendering
{
    [TestClass]
    public class GridVisualBuilder_Tests
    {
        [TestMethod]
        public void GivenBlank_WhenSetCell_ThenCharacterSetCorrectly()
        {
            var builder = new GridVisualBuilder(AnsiColor.Black, AnsiColor.White);
            builder.Resize(new(3, 3));

            builder.SetCell(0, 0, 'C', AnsiColor.Red);
            var result = builder.GetCharacter(0, 0);

            Assert.AreEqual('C', result);
        }

        [TestMethod]
        public void GivenBlank_WhenSetCell_ThenForegroundColorSetCorrectly()
        {
            var builder = new GridVisualBuilder(AnsiColor.Black, AnsiColor.White);
            builder.Resize(new(3, 3));

            builder.SetCell(0, 0, 'C', AnsiColor.Red, AnsiColor.Green);
            var foreground = builder.GetCellForegroundColor(0, 0);

            Assert.AreEqual(AnsiColor.Red, foreground);
        }

        [TestMethod]
        public void GivenBlank_WhenSetCell_ThenBackgroundColorSetCorrectly()
        {
            var builder = new GridVisualBuilder(AnsiColor.Black, AnsiColor.White);
            builder.Resize(new(3, 3));

            builder.SetCell(0, 0, AnsiColor.Green);
            var background = builder.GetCellBackgroundColor(0, 0);

            Assert.AreEqual(AnsiColor.Green, background);
        }

        [TestMethod]
        public void GivenBlank_WhenNotSet_ThenBackgroundColorSetCorrectly()
        {
            var builder = new GridVisualBuilder(AnsiColor.Black, AnsiColor.White);
            builder.Resize(new(3, 3));

            var background = builder.GetCellBackgroundColor(0, 0);

            Assert.AreEqual(AnsiColor.Black, background);
        }

        [TestMethod]
        public void GivenBlank_WhenNotSet_ThenForegroundColorSetCorrectly()
        {
            var builder = new GridVisualBuilder(AnsiColor.Black, AnsiColor.White);
            builder.Resize(new(3, 3));

            var foreground = builder.GetCellForegroundColor(0, 0);

            Assert.AreEqual(AnsiColor.White, foreground);
        }

        [TestMethod]
        public void GivenBlank_WhenDrawRectangle3x3_ThenCellsSetCorrectly()
        {
            var builder = new GridVisualBuilder(AnsiColor.Black, AnsiColor.White);
            builder.Resize(new(3, 3));

            builder.DrawRectangle(0, 0, 3, 3, AnsiColor.Black, AnsiColor.Red);

            Assert.AreEqual(AnsiColor.Black, builder.GetCellBackgroundColor(0, 0));
            Assert.AreEqual(AnsiColor.Black, builder.GetCellBackgroundColor(1, 0));
            Assert.AreEqual(AnsiColor.Black, builder.GetCellBackgroundColor(2, 0));
            Assert.AreEqual(AnsiColor.Black, builder.GetCellBackgroundColor(0, 1));
            Assert.AreEqual(AnsiColor.Black, builder.GetCellBackgroundColor(2, 1));
            Assert.AreEqual(AnsiColor.Black, builder.GetCellBackgroundColor(0, 2));
            Assert.AreEqual(AnsiColor.Black, builder.GetCellBackgroundColor(1, 2));
            Assert.AreEqual(AnsiColor.Black, builder.GetCellBackgroundColor(2, 2));
            Assert.AreEqual(AnsiColor.Red, builder.GetCellBackgroundColor(1, 1));
        }

        [TestMethod]
        public void GivenBlank_WhenDrawTexture3x3_ThenCellsSetCorrectly()
        {
            var builder = new GridVisualBuilder(AnsiColor.Black, AnsiColor.White);
            builder.Resize(new(3, 3));
            Texture texture = new("ABC\nDEF\nGHI");

            builder.DrawTexture(0, 0, 3, 3, texture, AnsiColor.Green);

            Assert.AreEqual('A', builder.GetCharacter(0, 0));
            Assert.AreEqual('B', builder.GetCharacter(1, 0));
            Assert.AreEqual('C', builder.GetCharacter(2, 0));
            Assert.AreEqual('D', builder.GetCharacter(0, 1));
            Assert.AreEqual('E', builder.GetCharacter(1, 1));
            Assert.AreEqual('F', builder.GetCharacter(2, 1));
            Assert.AreEqual('G', builder.GetCharacter(0, 2));
            Assert.AreEqual('H', builder.GetCharacter(1, 2));
            Assert.AreEqual('I', builder.GetCharacter(2, 2));

            Assert.AreEqual(AnsiColor.Green, builder.GetCellForegroundColor(0, 0));
            Assert.AreEqual(AnsiColor.Green, builder.GetCellForegroundColor(1, 0));
            Assert.AreEqual(AnsiColor.Green, builder.GetCellForegroundColor(2, 0));
            Assert.AreEqual(AnsiColor.Green, builder.GetCellForegroundColor(0, 1));
            Assert.AreEqual(AnsiColor.Green, builder.GetCellForegroundColor(1, 1));
            Assert.AreEqual(AnsiColor.Green, builder.GetCellForegroundColor(2, 1));
            Assert.AreEqual(AnsiColor.Green, builder.GetCellForegroundColor(0, 2));
            Assert.AreEqual(AnsiColor.Green, builder.GetCellForegroundColor(1, 2));
            Assert.AreEqual(AnsiColor.Green, builder.GetCellForegroundColor(2, 2));
        }

        [TestMethod]
        public void GivenBlank_WhenDrawTextureOverBackgroundColor3x3OverRed_ThenOnlyRedCellsModified()
        {
            var builder = new GridVisualBuilder(AnsiColor.Black, AnsiColor.White);
            builder.Resize(new(3, 3));
            char unset = '\0';
            Texture texture = new("ABC\nDEF\nGHI");
            builder.SetCell(1, 1, AnsiColor.Red);

            builder.DrawTextureOverBackgroundColor(0, 0, 3, 3, AnsiColor.Red, texture, AnsiColor.Green);

            Assert.AreEqual(unset, builder.GetCharacter(0, 0));
            Assert.AreEqual(unset, builder.GetCharacter(1, 0));
            Assert.AreEqual(unset, builder.GetCharacter(2, 0));
            Assert.AreEqual(unset, builder.GetCharacter(0, 1));
            Assert.AreEqual('E', builder.GetCharacter(1, 1));
            Assert.AreEqual(unset, builder.GetCharacter(2, 1));
            Assert.AreEqual(unset, builder.GetCharacter(0, 2));
            Assert.AreEqual(unset, builder.GetCharacter(1, 2));
            Assert.AreEqual(unset, builder.GetCharacter(2, 2));

            Assert.AreEqual(AnsiColor.White, builder.GetCellForegroundColor(0, 0));
            Assert.AreEqual(AnsiColor.White, builder.GetCellForegroundColor(1, 0));
            Assert.AreEqual(AnsiColor.White, builder.GetCellForegroundColor(2, 0));
            Assert.AreEqual(AnsiColor.White, builder.GetCellForegroundColor(0, 1));
            Assert.AreEqual(AnsiColor.Green, builder.GetCellForegroundColor(1, 1));
            Assert.AreEqual(AnsiColor.White, builder.GetCellForegroundColor(2, 1));
            Assert.AreEqual(AnsiColor.White, builder.GetCellForegroundColor(0, 2));
            Assert.AreEqual(AnsiColor.White, builder.GetCellForegroundColor(1, 2));
            Assert.AreEqual(AnsiColor.White, builder.GetCellForegroundColor(2, 2));
        }

        [TestMethod]
        public void GivenBlank_WhenOverlayGridPictureBuilder_ThenOverlayAppliedCorrectly()
        {
            var overlay = new GridVisualBuilder(AnsiColor.Black, AnsiColor.White);
            overlay.Resize(new(1, 1));
            var builder = new GridVisualBuilder(AnsiColor.Black, AnsiColor.White);
            builder.Resize(new(3, 3));
            overlay.SetCell(0, 0, 'C', AnsiColor.Cyan, AnsiColor.Red);

            builder.Overlay(1, 1, overlay);

            Assert.AreEqual('C', builder.GetCharacter(1, 1));
            Assert.AreEqual(AnsiColor.Cyan, builder.GetCellForegroundColor(1, 1));
            Assert.AreEqual(AnsiColor.Red, builder.GetCellBackgroundColor(1, 1));
        }

        [TestMethod]
        public void GivenBlank_WhenOverlayGridStringBuilder_ThenOverlayAppliedCorrectly()
        {
            var overlay = new GridStringBuilder();
            overlay.Resize(new(1, 1));
            var builder = new GridVisualBuilder(AnsiColor.Black, AnsiColor.White);
            builder.Resize(new(3, 3));
            overlay.SetCell(0, 0, 'C', AnsiColor.Cyan);

            builder.Overlay(1, 1, overlay);

            Assert.AreEqual('C', builder.GetCharacter(1, 1));
            Assert.AreEqual(AnsiColor.Cyan, builder.GetCellForegroundColor(1, 1));
        }

        [TestMethod]
        public void GivenBlank_WhenDrawText_ThenCellsSetCorrectly()
        {
            var builder = new GridVisualBuilder(AnsiColor.Black, AnsiColor.White);
            builder.Resize(new(3, 3));

            builder.DrawText(0, 0, "ABC", AnsiColor.Red);

            Assert.AreEqual(AnsiColor.Red, builder.GetCellForegroundColor(0, 0));
            Assert.AreEqual(AnsiColor.Red, builder.GetCellForegroundColor(1, 0));
            Assert.AreEqual(AnsiColor.Red, builder.GetCellForegroundColor(2, 0));
            Assert.AreEqual('A', builder.GetCharacter(0, 0));
            Assert.AreEqual('B', builder.GetCharacter(1, 0));
            Assert.AreEqual('C', builder.GetCharacter(2, 0));
        }

        [TestMethod]
        public void GivenBlank_WhenDrawBorder3x3_ThenCellsSetCorrectly()
        {
            var builder = new GridVisualBuilder(AnsiColor.Black, AnsiColor.White);
            builder.Resize(new(3, 3));

            builder.DrawBorder(0, 0, 3, 3, AnsiColor.Red);

            Assert.AreEqual(AnsiColor.Red, builder.GetCellBackgroundColor(0, 0));
            Assert.AreEqual(AnsiColor.Red, builder.GetCellBackgroundColor(1, 0));
            Assert.AreEqual(AnsiColor.Red, builder.GetCellBackgroundColor(2, 0));
            Assert.AreEqual(AnsiColor.Red, builder.GetCellBackgroundColor(0, 1));
            Assert.AreEqual(AnsiColor.Red, builder.GetCellBackgroundColor(2, 1));
            Assert.AreEqual(AnsiColor.Red, builder.GetCellBackgroundColor(0, 2));
            Assert.AreEqual(AnsiColor.Red, builder.GetCellBackgroundColor(1, 2));
            Assert.AreEqual(AnsiColor.Red, builder.GetCellBackgroundColor(2, 2));
            Assert.AreEqual(AnsiColor.Black, builder.GetCellBackgroundColor(1, 1));
        }

        [TestMethod]
        public void GivenBlank_WhenDrawCircle3x3_ThenCellsSetCorrectly()
        {
            var builder = new GridVisualBuilder(AnsiColor.Black, AnsiColor.White);
            builder.Resize(new(3, 3));

            builder.DrawCircle(1, 1, 1, AnsiColor.Red, AnsiColor.Blue);

            Assert.AreEqual(AnsiColor.Black, builder.GetCellBackgroundColor(0, 0));
            Assert.AreEqual(AnsiColor.Red, builder.GetCellBackgroundColor(1, 0));
            Assert.AreEqual(AnsiColor.Black, builder.GetCellBackgroundColor(2, 0));
            Assert.AreEqual(AnsiColor.Red, builder.GetCellBackgroundColor(0, 1));
            Assert.AreEqual(AnsiColor.Blue, builder.GetCellBackgroundColor(1, 1));
            Assert.AreEqual(AnsiColor.Red, builder.GetCellBackgroundColor(2, 1));
            Assert.AreEqual(AnsiColor.Black, builder.GetCellBackgroundColor(0, 2));
            Assert.AreEqual(AnsiColor.Red, builder.GetCellBackgroundColor(1, 2));
            Assert.AreEqual(AnsiColor.Black, builder.GetCellBackgroundColor(2, 2));
        }

        [TestMethod]
        public void GivenGrid_WhenToStringAndFromString_ThenValuesAreCorrect()
        {
            var builder = new GridVisualBuilder(AnsiColor.Black, AnsiColor.White);
            builder.Resize(new(3, 3));

            builder.SetCell(0, 0, AnsiColor.Red);
            builder.SetCell(1, 0, AnsiColor.Green);
            builder.SetCell(2, 0, AnsiColor.Blue);
            builder.SetCell(0, 1, AnsiColor.Yellow);
            builder.SetCell(1, 1, AnsiColor.Magenta);
            builder.SetCell(2, 1, AnsiColor.Cyan);
            builder.SetCell(0, 2, AnsiColor.Red);
            builder.SetCell(1, 2, AnsiColor.Green);
            builder.SetCell(2, 2, AnsiColor.Blue);
            builder.SetCell(0, 0, 'A', AnsiColor.White);
            builder.SetCell(1, 1, 'B', AnsiColor.Black);

            var serialized = builder.ToString();
            var newBuilder = GridVisualBuilder.FromString(serialized);

            Assert.AreEqual(builder.DisplaySize, newBuilder.DisplaySize);

            Assert.AreEqual(AnsiColor.Red, newBuilder.GetCellBackgroundColor(0, 0));
            Assert.AreEqual(AnsiColor.Green, newBuilder.GetCellBackgroundColor(1, 0));
            Assert.AreEqual(AnsiColor.Blue, newBuilder.GetCellBackgroundColor(2, 0));
            Assert.AreEqual(AnsiColor.Yellow, newBuilder.GetCellBackgroundColor(0, 1));
            Assert.AreEqual(AnsiColor.Magenta, newBuilder.GetCellBackgroundColor(1, 1));
            Assert.AreEqual(AnsiColor.Cyan, newBuilder.GetCellBackgroundColor(2, 1));
            Assert.AreEqual(AnsiColor.Red, newBuilder.GetCellBackgroundColor(0, 2));
            Assert.AreEqual(AnsiColor.Green, newBuilder.GetCellBackgroundColor(1, 2));
            Assert.AreEqual(AnsiColor.Blue, newBuilder.GetCellBackgroundColor(2, 2));

            Assert.AreEqual(AnsiColor.White, newBuilder.GetCellForegroundColor(0, 0));
            Assert.AreEqual(AnsiColor.Black, newBuilder.GetCellForegroundColor(1, 1));
            Assert.AreEqual(AnsiColor.White, newBuilder.GetCellForegroundColor(2, 2));

            Assert.AreEqual('A', newBuilder.GetCharacter(0, 0));
            Assert.AreEqual('B', newBuilder.GetCharacter(1, 1));
            Assert.AreEqual('\0', newBuilder.GetCharacter(2, 2));
        }
    }
}
