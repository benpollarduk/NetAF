using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets;
using NetAF.Targets.Html.Rendering;
using NetAF.Targets.Html.Rendering.FrameBuilders;

namespace NetAF.Tests.Targets.Html.Rendering.FrameBuilders
{
    [TestClass]
    public class HtmlHelpFrameBuilder_Tests
    {
        [TestMethod]
        public void GivenDefaultsWithNoInstructions_WhenBuild_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var htmlBuilder = new HtmlBuilder();
                var builder = new HtmlHelpFrameBuilder(htmlBuilder);

                builder.Build("Test", new NetAF.Commands.CommandHelp("Test", "Test 2"), null, new Size(80, 50));
            });
        }

        [TestMethod]
        public void GivenDefaultsWithInstructions_WhenBuild_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var htmlBuilder = new HtmlBuilder();
                var builder = new HtmlHelpFrameBuilder(htmlBuilder);

                builder.Build("Test", new NetAF.Commands.CommandHelp("Test", "Test 2", "Test 3.", "Test 4.", "Test 5."), [], new Size(80, 50));
            });
        }

        [TestMethod]
        public void GivenNoCommand_WhenBuild_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var htmlBuilder = new HtmlBuilder();
                var builder = new HtmlHelpFrameBuilder(htmlBuilder);

                builder.Build("Test", null, [], new Size(80, 50));
            });
        }

        [TestMethod]
        public void GivenDefaultsWithPrompts_WhenBuild_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var htmlBuilder = new HtmlBuilder();
                var builder = new HtmlHelpFrameBuilder(htmlBuilder);

                builder.Build("Test", new NetAF.Commands.CommandHelp("Test", "Test 2"), [new("Prompt")], new Size(80, 50));
            });
        }
    }
}
