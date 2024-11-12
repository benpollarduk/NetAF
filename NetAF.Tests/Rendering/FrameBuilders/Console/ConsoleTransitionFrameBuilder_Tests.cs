using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Rendering.FrameBuilders;
using NetAF.Rendering.FrameBuilders.Console;

namespace NetAF.Tests.Rendering.FrameBuilders.Console
{
    [TestClass]
    public class ConsoleTransitionFrameBuilder_Tests
    {
        [TestMethod]
        public void GivenDefaults_WhenBuild_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var gridStringBuilder = new GridStringBuilder();
                var builder = new ConsoleTransitionFrameBuilder(gridStringBuilder);

                builder.Build(string.Empty, string.Empty, 80, 50);
            });
        }
    }
}
