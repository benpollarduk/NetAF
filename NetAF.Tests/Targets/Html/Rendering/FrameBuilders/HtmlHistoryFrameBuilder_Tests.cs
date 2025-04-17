using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets;
using NetAF.Logging.History;
using NetAF.Targets.Html.Rendering;
using NetAF.Targets.Html.Rendering.FrameBuilders;

namespace NetAF.Tests.Targets.Html.Rendering.FrameBuilders
{
    [TestClass]
    public class HtmlHistoryFrameBuilder_Tests
    {
        [TestMethod]
        public void GivenDefaultsWith0Entries_WhenBuild_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var htmlBuilder = new HtmlBuilder();
                var builder = new HtmlHistoryFrameBuilder(htmlBuilder);

                builder.Build(string.Empty, string.Empty, [], new Size(80, 50));
            });
        }

        [TestMethod]
        public void GivenDefaultsWith2Entries_WhenBuild_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var htmlBuilder = new HtmlBuilder();
                var builder = new HtmlHistoryFrameBuilder(htmlBuilder);
                HistoryEntry[] entries = [new("A", "B"), new("C", "D")];

                builder.Build("C", "D", entries, new Size(80, 50));
            });
        }

        [TestMethod]
        public void GivenDefaultsWith2EntriesTruncatedTo1_WhenBuild_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var htmlBuilder = new HtmlBuilder();
                var builder = new HtmlHistoryFrameBuilder(htmlBuilder) { MaxEntries = 1 };
                HistoryEntry[] entries = [new("A", "B"), new("C", "D")];
                builder.Build("C", "D", entries, new Size(80, 50));
            });
        }
    }
}
