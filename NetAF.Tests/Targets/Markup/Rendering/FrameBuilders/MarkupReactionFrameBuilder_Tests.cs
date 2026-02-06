using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets;
using NetAF.Targets.Markup.Rendering;
using NetAF.Targets.Markup.Rendering.FrameBuilders;

namespace NetAF.Tests.Targets.Markup.Rendering.FrameBuilders
{
    [TestClass]
    public class MarkupReactionFrameBuilder_Tests
    {
        [TestMethod]
        public void GivenDefaults_WhenBuild_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var markupBuilder = new MarkupBuilder();
                var builder = new MarkupReactionFrameBuilder(markupBuilder);

                builder.Build(string.Empty, string.Empty, false, new Size(80, 50));
            });
        }

        [TestMethod]
        public void GivenNonEmptyFields_WhenBuild_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var markupBuilder = new MarkupBuilder();
                var builder = new MarkupReactionFrameBuilder(markupBuilder);

                builder.Build("test", "test", false, new Size(80, 50));
            });
        }
    }
}
