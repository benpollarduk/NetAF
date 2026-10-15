using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets;
using NetAF.Targets.Console.Rendering;
using NetAF.Targets.Console.Rendering.FrameBuilders.Legacy;

namespace NetAF.Tests.Targets.Console.Rendering.FrameBuilders.Legacy
{
    [TestClass]
    public class ConsoleLegacyReactionFrameBuilder_Tests
    {
        [TestMethod]
        public void GivenDefaultsAndRenderErrorMessage_WhenBuild_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var gridStringBuilder = new GridStringBuilder();
                var builder = new ConsoleLegacyReactionFrameBuilder(gridStringBuilder, ConsoleRenderProperties.Default);

                builder.Build(string.Empty, string.Empty, true, null, null, new Size(80, 50));
            });
        }

        [TestMethod]
        public void GivenDefaultsAndRenderNonMessage_WhenBuild_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var gridStringBuilder = new GridStringBuilder();
                var builder = new ConsoleLegacyReactionFrameBuilder(gridStringBuilder, ConsoleRenderProperties.Default);

                builder.Build(string.Empty, string.Empty, false, null, null, new Size(80, 50));
            });
        }

        [TestMethod]
        public void GivenNonEmptyFields_WhenBuild_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var gridStringBuilder = new GridStringBuilder();
                var builder = new ConsoleLegacyReactionFrameBuilder(gridStringBuilder, ConsoleRenderProperties.Default);

                builder.Build("test", "test", false, null, null, new Size(80, 50));
            });
        }
    }
}
