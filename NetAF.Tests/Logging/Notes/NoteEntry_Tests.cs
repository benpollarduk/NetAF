using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Logging.Notes;
using NetAF.Serialization.Assets;

namespace NetAF.Tests.Logging.Notes
{
    [TestClass]
    public class NoteEntry_Tests
    {
        [TestMethod]
        public void GivenSerialization_WhenFromSerialization_ThenRestoredCorrectly()
        {
            NoteEntry entry = new("a", "b");
            entry.Expire();
            NoteEntrySerialization serialization = NoteEntrySerialization.FromNoteEntry(entry);

            var result = NoteEntry.FromSerialization(serialization);

            Assert.AreEqual("a", result.Name);
            Assert.AreEqual("b", result.Content);
            Assert.IsTrue(result.HasExpired);
        }
    }
}
