using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets;
using NetAF.Targets.Html.Rendering;
using NetAF.Targets.Html.Rendering.FrameBuilders;

namespace NetAF.Tests.Targets.Html.Rendering.FrameBuilders
{
    [TestClass]
    public class HtmlReactionFrameBuilder_Tests
    {
        [TestMethod]
        public void GivenDefaults_WhenBuild_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var htmlBuilder = new HtmlBuilder();
                var builder = new HtmlReactionFrameBuilder(htmlBuilder, HtmlRenderProperties.Default);

                builder.Build(string.Empty, string.Empty, false, null, new Size(80, 50));
            });
        }

        [TestMethod]
        public void GivenNonEmptyFields_WhenBuild_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var htmlBuilder = new HtmlBuilder();
                var builder = new HtmlReactionFrameBuilder(htmlBuilder, HtmlRenderProperties.Default);

                builder.Build("test", "test", false, null, new Size(80, 50));
            });
        }
    }
}
