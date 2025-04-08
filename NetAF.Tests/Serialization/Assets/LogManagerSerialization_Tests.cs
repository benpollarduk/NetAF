using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Log;
using NetAF.Serialization;
using NetAF.Serialization.Assets;

namespace NetAF.Tests.Serialization.Assets
{
    [TestClass]
    public class LogManagerSerialization_Tests
    {
        [TestMethod]
        public void Given1Entry_WhenFromAttributeManager_ThenValuesHas1Element()
        {
            LogManager manager = new();
            manager.Add(new("A", "B"));

            var result = LogManagerSerialization.FromLogManager(manager);

            Assert.AreEqual(1, result.Entries.Count);
        }

        [TestMethod]
        public void Given0EntriesButARestorationWith1Entry_WhenRestore_ThenValuesHas1Entry()
        {
            LogManager manager1 = new();
            LogManager manager2 = new();
            manager2.Add(new LogEntry("a", "b"));
            var serialization = LogManagerSerialization.FromLogManager(manager2);

            ((IObjectSerialization<LogManager>)serialization).Restore(manager1);

            Assert.AreEqual(1, manager1.Count);
        }
    }
}
