﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Logging.History;

namespace NetAF.Tests.Logging.History
{
    [TestClass]
    public class HistoryManager_Tests
    {
        [TestMethod]
        public void GivenNoEntries_WhenGetAll_ThenEmptyArray()
        {
            var manager = new HistoryManager();

            var results = manager.GetAll();

            Assert.IsTrue(results.Length == 0);
        }

        [TestMethod]
        public void GivenNoEntries_WhenAddWithNameAndContent_ThenOneEntry()
        {
            var manager = new HistoryManager();
            manager.Add("A", "B");

            var result = manager.Count;

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void GivenNoEntries_WhenAdd_ThenOneEntry()
        {
            var manager = new HistoryManager();
            manager.Add(new("A", "B"));

            var result = manager.Count;

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void GivenOneEntry_WhenAddAdditional_ThenTwoEntries()
        {
            var manager = new HistoryManager();
            manager.Add(new("A", "B"));
            manager.Add(new("C", "D"));

            var result = manager.Count;

            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void GivenOneEntry_WhenClear_ThenNoEntries()
        {
            var manager = new HistoryManager();
            manager.Add(new("A", "B"));
            manager.Clear();

            var result = manager.Count;

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void GivenLimitOf1AndOneExistingEntry_WhenAdd_ThenOnlyNewEntry()
        {
            var manager = new HistoryManager { MaxEntries = 1 };
            manager.Add(new("A", "B"));
            manager.Add(new("C", "D"));

            var result = manager.Count;

            Assert.AreEqual(1, result);
            Assert.AreEqual("C", manager.GetAll()[0].Name);
        }

        [TestMethod]
        public void GivenEmptyEntry_WhenAdd_ThenNoEntryAdded()
        {
            var manager = new HistoryManager();
            manager.Add(new(string.Empty, string.Empty));

            var result = manager.Count;

            Assert.AreEqual(0, result);
        }
    }
}
