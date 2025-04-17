using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets;
using NetAF.Logging.History;
using NetAF.Targets.Text.Rendering.FrameBuilders;
using System.Text;

namespace NetAF.Tests.Targets.Text.Rendering.FrameBuilders
{
    [TestClass]
    public class TextHistoryFrameBuilder_Tests
    {
        [TestMethod]
        public void GivenDefaultsWith0Entries_WhenBuild_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var stringBuilder = new StringBuilder();
                var builder = new TextHistoryFrameBuilder(stringBuilder);

                builder.Build(string.Empty, string.Empty, [], new Size(80, 50));
            });
        }

        [TestMethod]
        public void GivenDefaultsWith2Entries_WhenBuild_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var stringBuilder = new StringBuilder();
                var builder = new TextHistoryFrameBuilder(stringBuilder);
                HistoryEntry[] entries = [new("A", "B"), new("C", "D")];

                builder.Build("C", "D", entries, new Size(80, 50));
            });
        }

        [TestMethod]
        public void GivenDefaultsWith2EntriesTruncatedTo1_WhenBuild_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var stringBuilder = new StringBuilder();
                var builder = new TextHistoryFrameBuilder(stringBuilder) { MaxEntries = 1 };
                HistoryEntry[] entries = [new("A", "B"), new("C", "D")];
                builder.Build("C", "D", entries, new Size(80, 50));
            });
        }
    }
}
