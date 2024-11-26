using NetAF.Rendering.Console;

namespace NetAF.Imaging.Tests
{
    [TestClass]
    public class GridVisualBuilderHelper_Tests
    {
        [TestMethod]
        public void GivenWhiteImage_WhenFromImage_ThenImageIsBrightWhite()
        {
            var path = "Resources/FullWhite.bmp";
            var frame = GridVisualBuilderHelper.FromImage(path, new(5, 5));

            var result = frame.GetCellBackgroundColor(0, 0);

            Assert.AreEqual(AnsiColor.BrightWhite, result);
        }

        [TestMethod]
        public void GivenBlackImage_WhenFromImage_ThenImageIsBlack()
        {
            var path = "Resources/FullBlack.bmp";
            var frame = GridVisualBuilderHelper.FromImage(path, new(5, 5));
            
            var result = frame.GetCellBackgroundColor(0, 0);

            Assert.AreEqual(AnsiColor.Black, result);
        }
    }
}