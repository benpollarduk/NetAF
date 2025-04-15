using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets;
using NetAF.Logging.Notes;
using NetAF.Targets.Console.Rendering;
using NetAF.Targets.Console.Rendering.FrameBuilders;

namespace NetAF.Tests.Targets.Console.Rendering.FrameBuilders
{
    [TestClass]
    public class ConsoleNoteFrameBuilder_Tests
    {
        [TestMethod]
        public void GivenDefaultsWith0Entries_WhenBuild_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var gridStringBuilder = new GridStringBuilder();
                var builder = new ConsoleNoteFrameBuilder(gridStringBuilder);

                builder.Build(string.Empty, string.Empty, [], new Size(80, 50));
            });
        }

        [TestMethod]
        public void GivenDefaultsWith2Entries_WhenBuild_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var gridStringBuilder = new GridStringBuilder();
                var builder = new ConsoleNoteFrameBuilder(gridStringBuilder);
                NoteEntry[] entries = [new("A", "B"), new("C", "D")];
                entries[0].Expire();

                builder.Build("C", "D", entries, new Size(80, 50));
            });
        }
    }
}
