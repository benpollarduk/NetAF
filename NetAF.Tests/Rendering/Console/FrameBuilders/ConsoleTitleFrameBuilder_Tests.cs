using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets;
using NetAF.Rendering.Console;
using NetAF.Rendering.Console.FrameBuilders;

namespace NetAF.Tests.Rendering.Console.FrameBuilders
{
    [TestClass]
    public class ConsoleTitleFrameBuilder_Tests
    {
        [TestMethod]
        public void GivenDefaults_WhenBuild_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var gridStringBuilder = new GridStringBuilder();
                var builder = new ConsoleTitleFrameBuilder(gridStringBuilder);

                builder.Build(string.Empty, string.Empty, new Size(80, 50));
            });
        }
    }
}
