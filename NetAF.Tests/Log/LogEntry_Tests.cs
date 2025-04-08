using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Log;
using NetAF.Serialization.Assets;

namespace NetAF.Tests.Log
{
    [TestClass]
    public class LogEntry_Tests
    {
        [TestMethod]
        public void GivenSerialization_WhenFromSerialization_ThenRestoredCorrectly()
        {
            LogEntry entry = new("a", "b");
            entry.Expire();
            LogEntrySerialization serialization = LogEntrySerialization.FromLogEntry(entry);

            var result = LogEntry.FromSerialization(serialization);

            Assert.AreEqual("a", result.Name);
            Assert.AreEqual("b", result.Content);
            Assert.IsTrue(result.HasExpired);
        }
    }
}
