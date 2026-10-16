using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Targets.Console.Rendering.FrameBuilders.Legacy;

namespace NetAF.Tests.Targets.Console.Rendering.FrameBuilders.Legacy
{
    [TestClass]
    public class ConsoleLegacyFrameBuilder_Tests
    {
        [TestMethod]
        public void GivenZeroMaxLineWidth_WhenGetConstrainedWidth_Then0Returned()
        {
            var result = ConsoleLegacyFrameBuilder.GetConstrainedWidth(80, 0);

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void GivenNegativeMaxLineWidth_WhenGetConstrainedWidth_ThenAvailableWidthReturned()
        {
            var result = ConsoleLegacyFrameBuilder.GetConstrainedWidth(80, -1);

            Assert.AreEqual(80, result);
        }

        [TestMethod]
        public void GivenMaxLineWidthConstrainsWidth_WhenGetConstrainedWidth_ThenConstrainedWidthReturned()
        {
            var result = ConsoleLegacyFrameBuilder.GetConstrainedWidth(80, 40);

            Assert.AreEqual(40, result);
        }

        [TestMethod]
        public void GivenAvailableWidthSmallerThanConstraint_WhenGetConstrainedWidth_ThenAvailableWidthReturned()
        {
            var result = ConsoleLegacyFrameBuilder.GetConstrainedWidth(30, 80);

            Assert.AreEqual(30, result);
        }
    }
}
