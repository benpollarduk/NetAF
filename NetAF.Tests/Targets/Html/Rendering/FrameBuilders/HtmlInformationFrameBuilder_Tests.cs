using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets;
using NetAF.Targets.Html.Rendering;
using NetAF.Targets.Html.Rendering.FrameBuilders;

namespace NetAF.Tests.Targets.Html.Rendering.FrameBuilders
{
    [TestClass]
    public class HtmlInformationFrameBuilder_Tests
    {
        [TestMethod]
        public void GivenDefaultsWith0Entries_WhenBuild_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var htmlBuilder = new HtmlBuilder();
                var builder = new HtmlInformationFrameBuilder(htmlBuilder);

                builder.Build(string.Empty, string.Empty, [], new Size(80, 50));
            });
        }

        [TestMethod]
        public void GivenDefaultsWith1Entry_WhenBuild_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var htmlBuilder = new HtmlBuilder();
                var builder = new HtmlInformationFrameBuilder(htmlBuilder);

                builder.Build(string.Empty, string.Empty, [new("A", "B")], new Size(80, 50));
            });
        }
    }
}
