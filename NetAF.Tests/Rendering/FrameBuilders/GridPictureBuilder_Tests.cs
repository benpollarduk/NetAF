using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Rendering.FrameBuilders;
using NetAF.Rendering.FrameBuilders.Console;

namespace NetAF.Tests.Rendering.FrameBuilders
{
    [TestClass]
    public class GridPictureBuilder_Tests
    {
        [TestMethod]
        public void GivenBlank_WhenSetCell_ThenCharacterSetCorrectly()
        {
            var builder = new GridPictureBuilder(AnsiColor.Black, AnsiColor.White);
            builder.Resize(new(10, 10));

            builder.SetCell(0, 0, 'C', AnsiColor.Red);
            var result = builder.GetCharacter(0, 0);

            Assert.AreEqual('C', result);
        }

        [TestMethod]
        public void GivenBlank_WhenSetCell_ThenForegroundColorSetCorrectly()
        {
            var builder = new GridPictureBuilder(AnsiColor.Black, AnsiColor.White);
            builder.Resize(new(10, 10));

            builder.SetCell(0, 0, 'C', AnsiColor.Red, AnsiColor.Green);
            var foreground = builder.GetCellForegroundColor(0, 0);

            Assert.AreEqual(AnsiColor.Red, foreground);
        }

        [TestMethod]
        public void GivenBlank_WhenSetCell_ThenBackgroundColorSetCorrectly()
        {
            var builder = new GridPictureBuilder(AnsiColor.Black, AnsiColor.White);
            builder.Resize(new(10, 10));

            builder.SetCell(0, 0, AnsiColor.Green);
            var background = builder.GetCellBackgroundColor(0, 0);

            Assert.AreEqual(AnsiColor.Green, background);
        }

        [TestMethod]
        public void GivenBlank_WhenNotSer_ThenBackgroundColorSetCorrectly()
        {
            var builder = new GridPictureBuilder(AnsiColor.Black, AnsiColor.White);
            builder.Resize(new(10, 10));

            var background = builder.GetCellBackgroundColor(0, 0);

            Assert.AreEqual(AnsiColor.Black, background);
        }

        [TestMethod]
        public void GivenBlank_WhenNotSer_ThenForegroundColorSetCorrectly()
        {
            var builder = new GridPictureBuilder(AnsiColor.Black, AnsiColor.White);
            builder.Resize(new(10, 10));

            var foreground = builder.GetCellForegroundColor(0, 0);

            Assert.AreEqual(AnsiColor.White, foreground);
        }
    }
}
