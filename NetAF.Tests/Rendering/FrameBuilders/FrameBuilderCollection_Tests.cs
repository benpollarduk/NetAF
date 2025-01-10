using NetAF.Rendering.FrameBuilders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Rendering.Console.FrameBuilders;

namespace NetAF.Tests.Rendering.FrameBuilders
{
    [TestClass]
    public class FrameBuilderCollection_Tests
    {
        [TestMethod]
        public void GivenNoMatchingFrameBuilder_WhenGetFrameBuilder_ThenNull()
        {
            var builders = new FrameBuilderCollection();

            var result = builders.GetFrameBuilder<IFrameBuilder>();

            Assert.IsNull(result);
        }

        [TestMethod]
        public void GivenMatchingFrameBuilder_WhenGetFrameBuilder_ThenCorrectInstance()
        {
            var builder = new ConsoleSceneFrameBuilder(null, null);
            var builders = new FrameBuilderCollection(builder);

            var result = builders.GetFrameBuilder<ISceneFrameBuilder>();

            Assert.AreEqual(builder, result);
        }
    }
}
