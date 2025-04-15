using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Logging.Notes;
using NetAF.Serialization;
using NetAF.Serialization.Assets;

namespace NetAF.Tests.Serialization.Assets
{
    [TestClass]
    public class NoteEntry_Tests
    {
        [TestMethod]
        public void GivenNameA_WhenFromNoteEntry_ThenNameIsA()
        {
            NoteEntry entry = new("A", string.Empty);

            NoteEntrySerialization result = NoteEntrySerialization.FromNoteEntry(entry);

            Assert.AreEqual("A", result.Name);
        }

        [TestMethod]
        public void GivenContentA_WhenFromNoteEntry_ThenContentIsA()
        {
            NoteEntry entry = new(string.Empty, "A");

            NoteEntrySerialization result = NoteEntrySerialization.FromNoteEntry(entry);

            Assert.AreEqual("A", result.Content);
        }

        [TestMethod]
        public void GivenHasExpired_WhenFromNoteEntry_ThenHasExpired()
        {
            NoteEntry entry = new(string.Empty, string.Empty);
            entry.Expire();

            NoteEntrySerialization result = NoteEntrySerialization.FromNoteEntry(entry);

            Assert.IsTrue(result.HasExpired);
        }

        [TestMethod]
        public void GivenHasExpired_WhenRestoreFromNotExpired_ThenHasExpiredIsFalse()
        {
            NoteEntry entry = new(string.Empty, string.Empty);
            NoteEntry entry2 = new(string.Empty, string.Empty);
            entry2.Expire();
            NoteEntrySerialization serialization = NoteEntrySerialization.FromNoteEntry(entry);

            ((IObjectSerialization<NoteEntry>)serialization).Restore(entry2);

            Assert.IsFalse(entry.HasExpired);
        }
    }
}
