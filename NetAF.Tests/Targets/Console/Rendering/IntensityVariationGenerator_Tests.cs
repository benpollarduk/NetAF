using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Targets.Console.Rendering;

namespace NetAF.Tests.Targets.Console.Rendering
{
    [TestClass]
    public class IntensityVariationGenerator_Tests
    {
        [TestMethod]
        public void GivenMinimum10Maximum10_WhenVary_ThenValueIsWithinRange()
        {
            var color = new AnsiColor(100, 100, 100);

            var result = new IntensityVariationGenerator(10, 10).Vary(color);

            Assert.IsInRange<byte>(90, 110, result.R);
            Assert.IsInRange<byte>(90, 110, result.G);
            Assert.IsInRange<byte>(90, 110, result.B);
        }
    }
}
