using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Logging.Notes;
using NetAF.Serialization.Assets;

namespace NetAF.Tests.Logging.Notes
{
    [TestClass]
    public class NoteManager_Tests
    {
        [TestMethod]
        public void GivenNoEntries_WhenGetAll_ThenEmptyArray()
        {
            var manager = new NoteManager();

            var results = manager.GetAll();

            Assert.IsTrue(results.Length == 0);
        }

        [TestMethod]
        public void GivenNoEntries_WhenAddWithNameAndContent_ThenOneEntry()
        {
            var manager = new NoteManager();
            manager.Add("A", "B");

            var result = manager.Count;

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void GivenNoEntries_WhenAdd_ThenOneEntry()
        {
            var manager = new NoteManager();
            manager.Add(new("A", "B"));

            var result = manager.Count;

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void GivenOneEntry_WhenAddDuplicate_ThenOneEntry()
        {
            var manager = new NoteManager();
            manager.Add(new("A", "B"));
            manager.Add(new("A", "B"));

            var result = manager.Count;

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void GivenOneEntry_WhenAddNonDuplicate_ThenTwoEntries()
        {
            var manager = new NoteManager();
            manager.Add(new("A", "B"));
            manager.Add(new("C", "D"));

            var result = manager.Count;

            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void GivenOneEntry_WhenRemove_ThenNoEntries()
        {
            var manager = new NoteManager();
            manager.Add(new("A", "B"));
            manager.Remove("A");

            var result = manager.Count;

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void GivenOneEntry_WhenRemoveNonExisting_ThenOneEntry()
        {
            var manager = new NoteManager();
            manager.Add(new("A", "B"));
            manager.Remove("C");

            var result = manager.Count;

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void GivenOneEntry_WhenExpire_ThenOneEntry()
        {
            var manager = new NoteManager();
            manager.Add(new("A", "B"));
            manager.Expire("A");

            var result = manager.Count;

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void GivenOneEntry_WhenExpireNonExisting_ThenOneEntry()
        {
            var manager = new NoteManager();
            manager.Add(new("A", "B"));
            manager.Expire("C");

            var result = manager.Count;

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void GivenOneEntry_WhenClear_ThenNoEntries()
        {
            var manager = new NoteManager();
            manager.Add(new("A", "B"));
            manager.Clear();

            var result = manager.Count;

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void GivenSerialization_WhenFromSerialization_ThenRestoredCorrectly()
        {
            NoteManager manager = new();
            manager.Add(new("a", "b"));
            NoteManagerSerialization serialization = NoteManagerSerialization.FromNoteManager(manager);

            var result = NoteManager.FromSerialization(serialization);

            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void GivenOneEntryWithMatch_WhenContainsEntry_ThenTrue()
        {
            var manager = new NoteManager();
            manager.Add(new("A", "B"));

            var result = manager.ContainsEntry("A");

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenOneEntryWithNoMatch_WhenContainsEntry_ThenFalse()
        {
            var manager = new NoteManager();
            manager.Add(new("A", "B"));

            var result = manager.ContainsEntry("C");

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenOneEntryThatHasExpired_WhenHasExpired_ThenTrue()
        {
            var manager = new NoteManager();
            manager.Add(new("A", "B"));
            manager.Expire("A");

            var result = manager.HasExpired("A");

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenOneEntryThatHasNotExpired_WhenHasExpired_ThenFalse()
        {
            var manager = new NoteManager();
            manager.Add(new("A", "B"));

            var result = manager.HasExpired("A");

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenNoEntries_WhenHasExpired_ThenFalse()
        {
            var manager = new NoteManager();

            var result = manager.HasExpired("A");

            Assert.IsFalse(result);
        }
    }
}
