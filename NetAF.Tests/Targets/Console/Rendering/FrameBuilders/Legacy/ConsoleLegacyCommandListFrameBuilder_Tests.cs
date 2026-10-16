using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets;
using NetAF.Commands.Scene;
using NetAF.Targets.Console.Rendering;
using NetAF.Targets.Console.Rendering.FrameBuilders.Legacy;

namespace NetAF.Tests.Targets.Console.Rendering.FrameBuilders.Legacy
{
    [TestClass]
    public class ConsoleLegacyCommandListFrameBuilder_Tests
    {
        [TestMethod]
        public void GivenDefaults_WhenBuild_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var gridStringBuilder = new GridStringBuilder();
                var builder = new ConsoleLegacyCommandListFrameBuilder(gridStringBuilder, ConsoleRenderProperties.Default);

                builder.Build(string.Empty, string.Empty, [Take.CommandHelp], new Size(80, 50));
            });
        }
    }
}
