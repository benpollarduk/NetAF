using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets;
using NetAF.Logging.History;
using NetAF.Targets.Console.Rendering;
using NetAF.Targets.Console.Rendering.FrameBuilders;

namespace NetAF.Tests.Targets.Console.Rendering.FrameBuilders
{
    [TestClass]
    public class ConsoleHistoryFrameBuilder_Tests
    {
        [TestMethod]
        public void GivenDefaultsWith0Entries_WhenBuild_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var gridStringBuilder = new GridStringBuilder();
                var builder = new ConsoleHistoryFrameBuilder(gridStringBuilder);

                builder.Build(string.Empty, string.Empty, [], new Size(80, 50));
            });
        }

        [TestMethod]
        public void GivenDefaultsWith2Entries_WhenBuild_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var gridStringBuilder = new GridStringBuilder();
                var builder = new ConsoleHistoryFrameBuilder(gridStringBuilder);
                HistoryEntry[] entries = [new("A", "B"), new("C", "D")];
                builder.Build("C", "D", entries, new Size(80, 50));
            });
        }

        [TestMethod]
        public void GivenDefaultsWith2EntriesTruncatedTo1_WhenBuild_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var gridStringBuilder = new GridStringBuilder();
                var builder = new ConsoleHistoryFrameBuilder(gridStringBuilder) { MaxEntries = 1 };
                HistoryEntry[] entries = [new("A", "B"), new("C", "D")];
                builder.Build("C", "D", entries, new Size(80, 50));
            });
        }
    }
}
