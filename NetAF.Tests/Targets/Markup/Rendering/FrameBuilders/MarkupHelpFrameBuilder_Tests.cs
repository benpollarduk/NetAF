using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets;
using NetAF.Commands;
using NetAF.Targets.Markup.Rendering;
using NetAF.Targets.Markup.Rendering.FrameBuilders;

namespace NetAF.Tests.Targets.Markup.Rendering.FrameBuilders
{
    [TestClass]
    public class MarkupHelpFrameBuilder_Tests
    {
        [TestMethod]
        public void GivenDefaultsWithNoInstructions_WhenBuild_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var markupBuilder = new MarkupBuilder();
                var builder = new MarkupHelpFrameBuilder(markupBuilder);

                builder.Build("Test", new NetAF.Commands.CommandHelp("Test", "Test 2"), null, new Size(80, 50));
            });
        }

        [TestMethod]
        public void GivenDefaultsWithInstructions_WhenBuild_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var markupBuilder = new MarkupBuilder();
                var builder = new MarkupHelpFrameBuilder(markupBuilder);

                builder.Build("Test", new NetAF.Commands.CommandHelp("Test", "Test 2", CommandCategory.Uncategorized, "Test 3.", "Test 4.", "Test 5."), [], new Size(80, 50));
            });
        }

        [TestMethod]
        public void GivenNoCommand_WhenBuild_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var markupBuilder = new MarkupBuilder();
                var builder = new MarkupHelpFrameBuilder(markupBuilder);

                builder.Build("Test", null, [], new Size(80, 50));
            });
        }

        [TestMethod]
        public void GivenDefaultsWithPrompts_WhenBuild_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var markupBuilder = new MarkupBuilder();
                var builder = new MarkupHelpFrameBuilder(markupBuilder);

                builder.Build("Test", new NetAF.Commands.CommandHelp("Test", "Test 2"), [new("Prompt")], new Size(80, 50));
            });
        }
    }
}
