using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Logging.Notes;
using NetAF.Serialization;
using NetAF.Serialization.Assets;

namespace NetAF.Tests.Serialization.Assets
{
    [TestClass]
    public class NoteManagerSerialization_Tests
    {
        [TestMethod]
        public void Given1Entry_WhenFromAttributeManager_ThenValuesHas1Element()
        {
            NoteManager manager = new();
            manager.Add(new("A", "B"));

            var result = NoteManagerSerialization.FromNoteManager(manager);

            Assert.AreEqual(1, result.Entries.Count);
        }

        [TestMethod]
        public void Given0EntriesButARestorationWith1Entry_WhenRestore_ThenValuesHas1Entry()
        {
            NoteManager manager1 = new();
            NoteManager manager2 = new();
            manager2.Add(new NoteEntry("a", "b"));
            var serialization = NoteManagerSerialization.FromNoteManager(manager2);

            ((IObjectSerialization<NoteManager>)serialization).Restore(manager1);

            Assert.AreEqual(1, manager1.Count);
        }
    }
}
