using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets;
using NetAF.Logging.History;
using NetAF.Targets.Markup.Rendering;
using NetAF.Targets.Markup.Rendering.FrameBuilders;

namespace NetAF.Tests.Targets.Markup.Rendering.FrameBuilders
{
    [TestClass]
    public class MarkupHistoryFrameBuilder_Tests
    {
        [TestMethod]
        public void GivenDefaultsWith0Entries_WhenBuild_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var htmlBuilder = new MarkupBuilder();
                var builder = new MarkupHistoryFrameBuilder(htmlBuilder);

                builder.Build(string.Empty, string.Empty, [], new Size(80, 50));
            });
        }

        [TestMethod]
        public void GivenDefaultsWith2Entries_WhenBuild_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var htmlBuilder = new MarkupBuilder();
                var builder = new MarkupHistoryFrameBuilder(htmlBuilder);
                HistoryEntry[] entries = [new("A", "B"), new("C", "D")];

                builder.Build("C", "D", entries, new Size(80, 50));
            });
        }

        [TestMethod]
        public void GivenDefaultsWith2EntriesTruncatedTo1_WhenBuild_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var htmlBuilder = new MarkupBuilder();
                var builder = new MarkupHistoryFrameBuilder(htmlBuilder) { MaxEntries = 1 };
                HistoryEntry[] entries = [new("A", "B"), new("C", "D")];
                builder.Build("C", "D", entries, new Size(80, 50));
            });
        }
    }
}
