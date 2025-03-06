using NetAF.Rendering.FrameBuilders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NetAF.Tests.Rendering.FrameBuilders
{
    [TestClass]
    public class FrameBuilderCollections_Tests
    {
        [TestMethod]
        public void GivenGetConsole_ThenPopulatedCollectionReturned()
        {
            var result = FrameBuilderCollections.Console;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GivenGetHtml_ThenPopulatedCollectionReturned()
        {
            var result = FrameBuilderCollections.Html;

            Assert.IsNotNull(result);
        }
    }
}
