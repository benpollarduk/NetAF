using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets;
using NetAF.Rendering;
using NetAF.Targets.Console.Rendering;

namespace NetAF.Tests.Rendering
{
    [TestClass]
    public class Visual_Tests
    {
        [TestMethod]
        public void Given10x10NewSize5x5_WhenCrop_ThenNewSizeIs5x5()
        {
            var builder = new GridVisualBuilder(AnsiColor.Black, AnsiColor.Blue);
            builder.Resize(new Size(10, 10));
            var value = new Visual(string.Empty, string.Empty, builder);

            var result = value.Crop(new Size(5, 5));

            Assert.AreEqual(5, result.VisualBuilder.DisplaySize.Width);
            Assert.AreEqual(5, result.VisualBuilder.DisplaySize.Height);
        }

        [TestMethod]
        public void Given10x10NewSize15x15_WhenCrop_ThenNewSizeIs10x10()
        {
            var builder = new GridVisualBuilder(AnsiColor.Black, AnsiColor.Blue);
            builder.Resize(new Size(10, 10));
            var value = new Visual(string.Empty, string.Empty, builder);

            var result = value.Crop(new Size(15, 15));

            Assert.AreEqual(10, result.VisualBuilder.DisplaySize.Width);
            Assert.AreEqual(10, result.VisualBuilder.DisplaySize.Height);
        }

        [TestMethod]
        public void Given10x10NewSize5x5_WhenScale_ThenNewSizeIs5x5()
        {
            var builder = new GridVisualBuilder(AnsiColor.Black, AnsiColor.Blue);
            builder.Resize(new Size(10, 10));
            var value = new Visual(string.Empty, string.Empty, builder);

            var result = value.Scale(new Size(5, 5));

            Assert.AreEqual(5, result.VisualBuilder.DisplaySize.Width);
            Assert.AreEqual(5, result.VisualBuilder.DisplaySize.Height);
        }

        [TestMethod]
        public void Given10x10NewSize15x15_WhenScale_ThenNewSizeIs15x15()
        {
            var builder = new GridVisualBuilder(AnsiColor.Black, AnsiColor.Blue);
            builder.Resize(new Size(10, 10));
            var value = new Visual(string.Empty, string.Empty, builder);

            var result = value.Scale(new Size(15, 15));

            Assert.AreEqual(15, result.VisualBuilder.DisplaySize.Width);
            Assert.AreEqual(15, result.VisualBuilder.DisplaySize.Height);
        }
    }
}
