using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets;
using NetAF.Logging.Notes;
using NetAF.Targets.Markup.Rendering;
using NetAF.Targets.Markup.Rendering.FrameBuilders;

namespace NetAF.Tests.Targets.Markup.Rendering.FrameBuilders
{
    [TestClass]
    public class MarkupNoteFrameBuilder_Tests
    {
        [TestMethod]
        public void GivenDefaultsWith0Entries_WhenBuild_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var htmlBuilder = new MarkupBuilder();
                var builder = new MarkupNoteFrameBuilder(htmlBuilder);

                builder.Build(string.Empty, string.Empty, [], new Size(80, 50));
            });
        }

        [TestMethod]
        public void GivenDefaultsWith2Entries_WhenBuild_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var htmlBuilder = new MarkupBuilder();
                var builder = new MarkupNoteFrameBuilder(htmlBuilder);
                NoteEntry[] entries = [new("A", "B"), new("C", "D")];
                entries[0].Expire();

                builder.Build("C", "D", entries, new Size(80, 50));
            });
        }
    }
}
