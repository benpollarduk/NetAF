using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Information;
using NetAF.Serialization.Assets;

namespace NetAF.Tests.Information
{
    [TestClass]
    public class InformationManager_Tests
    {
        [TestMethod]
        public void GivenNoEntries_WhenGetAll_ThenEmptyArray()
        {
            var manager = new InformationManager();

            var results = manager.GetAll();

            Assert.IsTrue(results.Length == 0);
        }

        [TestMethod]
        public void GivenNoEntries_WhenAdd_ThenOneEntry()
        {
            var manager = new InformationManager();
            manager.Add(new("A", "B"));

            var result = manager.Count;

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void GivenOneEntry_WhenAddDuplicate_ThenOneEntry()
        {
            var manager = new InformationManager();
            manager.Add(new("A", "B"));
            manager.Add(new("A", "B"));

            var result = manager.Count;

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void GivenOneEntry_WhenAddNonDuplicate_ThenTwoEntries()
        {
            var manager = new InformationManager();
            manager.Add(new("A", "B"    ));
            manager.Add(new("C", "D"));

            var result = manager.Count;

            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void GivenOneEntry_WhenRemove_ThenNoEntries()
        {
            var manager = new InformationManager();
            manager.Add(new("A", "B"));
            manager.Remove("A");

            var result = manager.Count;

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void GivenOneEntry_WhenRemoveNonExisting_ThenOneEntry()
        {
            var manager = new InformationManager();
            manager.Add(new("A", "B"));
            manager.Remove("C");

            var result = manager.Count;

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void GivenOneEntry_WhenClear_ThenNoEntries()
        {
            var manager = new InformationManager();
            manager.Add(new("A", "B"));
            manager.Clear();

            var result = manager.Count;

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void GivenSerialization_WhenFromSerialization_ThenRestoredCorrectly()
        {
            InformationManager manager = new();
            manager.Add(new("a", "b"));
            InformationManagerSerialization serialization = InformationManagerSerialization.FromInformationManager(manager);

            var result = InformationManager.FromSerialization(serialization);

            Assert.AreEqual(1, result.Count);
        }
    }
}
