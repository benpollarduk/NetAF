using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets;
using NetAF.Commands.Scene;
using NetAF.Rendering.Console;
using NetAF.Rendering.Console.FrameBuilders;

namespace NetAF.Tests.Rendering.Console.FrameBuilders
{
    [TestClass]
    public class ConsoleCommandListFrameBuilder_Tests
    {
        [TestMethod]
        public void GivenDefaults_WhenBuild_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var gridStringBuilder = new GridStringBuilder();
                var builder = new ConsoleCommandListFrameBuilder(gridStringBuilder);

                builder.Build(string.Empty, string.Empty, [Take.CommandHelp], new Size(80, 50));
            });
        }
    }
}
