using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Rendering.FrameBuilders;
using NetAF.Targets.Console.Rendering.FrameBuilders;
using System;

namespace NetAF.Tests.Rendering.FrameBuilders
{
    [TestClass]
    public class FrameBuilderCollection_Tests
    {
        [TestMethod]
        public void GivenNoMatchingFrameBuilder_WhenGetFrameBuilder_ThenInvalidOperationExceptionThrown()
        {
            var result = false;

            try
            {

                var builders = new FrameBuilderCollection();

                builders.GetFrameBuilder<IFrameBuilder>();
            }
            catch (InvalidOperationException)
            {
                result = true;
            }

            Assert.IsTrue(result);
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
