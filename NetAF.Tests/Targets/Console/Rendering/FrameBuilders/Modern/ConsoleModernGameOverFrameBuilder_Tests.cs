using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets;
using NetAF.Targets.Console.Rendering;
using NetAF.Targets.Console.Rendering.FrameBuilders.Modern;

namespace NetAF.Tests.Targets.Console.Rendering.FrameBuilders.Modern
{
    [TestClass]
    public class ConsoleModernGameOverFrameBuilder_Tests
    {
        [TestMethod]
        public void GivenDefaults_WhenBuild_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var gridStringBuilder = new GridStringBuilder();
                var builder = new ConsoleModernGameOverFrameBuilder(gridStringBuilder, ConsoleRenderProperties.Default);

                builder.Build(string.Empty, string.Empty, new Size(80, 50));
            });
        }
    }
}
