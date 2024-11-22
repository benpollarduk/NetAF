using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets;
using NetAF.Rendering.FrameBuilders;
using NetAF.Rendering.FrameBuilders.Console;

namespace NetAF.Tests.Rendering.FrameBuilders.Console
{
    [TestClass]
    public class ConsoleHelpFrameBuilder_Tests
    {
        [TestMethod]
        public void GivenDefaultsWithNoInstructions_WhenBuild_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var gridStringBuilder = new GridStringBuilder();
                var builder = new ConsoleHelpFrameBuilder(gridStringBuilder);

                builder.Build("Test", new NetAF.Commands.CommandHelp("Test", "Test 2"), new Size(80, 50));
            });
        }

        [TestMethod]
        public void GivenDefaultsWithInstructions_WhenBuild_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var gridStringBuilder = new GridStringBuilder();
                var builder = new ConsoleHelpFrameBuilder(gridStringBuilder);

                builder.Build("Test", new NetAF.Commands.CommandHelp("Test", "Test 2", "Test 3."), new Size(80, 50));
            });
        }
    }
}
