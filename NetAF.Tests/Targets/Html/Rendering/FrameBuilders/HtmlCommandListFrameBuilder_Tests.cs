using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets;
using NetAF.Commands.Scene;
using NetAF.Targets.Html.Rendering;
using NetAF.Targets.Html.Rendering.FrameBuilders;

namespace NetAF.Tests.Targets.Html.Rendering.FrameBuilders
{
    [TestClass]
    public class HtmlCommandListFrameBuilder_Tests
    {
        [TestMethod]
        public void GivenDefaults_WhenBuild_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var htmlBuilder = new HtmlBuilder();
                var builder = new HtmlCommandListFrameBuilder(htmlBuilder);

                builder.Build(string.Empty, string.Empty, [Take.CommandHelp], new Size(80, 50));
            });
        }
    }
}
