using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Log;
using NetAF.Serialization;
using NetAF.Serialization.Assets;

namespace NetAF.Tests.Serialization.Assets
{
    [TestClass]
    public class LogEntry_Tests
    {
        [TestMethod]
        public void GivenNameA_WhenFromLogEntry_ThenNameIsA()
        {
            LogEntry entry = new("A", string.Empty);

            LogEntrySerialization result = LogEntrySerialization.FromLogEntry(entry);

            Assert.AreEqual("A", result.Name);
        }

        [TestMethod]
        public void GivenContentA_WhenFromLogEntry_ThenContentIsA()
        {
            LogEntry entry = new(string.Empty, "A");

            LogEntrySerialization result = LogEntrySerialization.FromLogEntry(entry);

            Assert.AreEqual("A", result.Content);
        }

        [TestMethod]
        public void GivenHasExpired_WhenFromLogEntry_ThenHasExpired()
        {
            LogEntry entry = new(string.Empty, string.Empty);
            entry.Expire();

            LogEntrySerialization result = LogEntrySerialization.FromLogEntry(entry);

            Assert.IsTrue(result.HasExpired);
        }

        [TestMethod]
        public void GivenHasExpired_WhenRestoreFromNotExpired_ThenHasExpiredIsFalse()
        {
            LogEntry entry = new(string.Empty, string.Empty);
            LogEntry entry2 = new(string.Empty, string.Empty);
            entry2.Expire();
            LogEntrySerialization serialization = LogEntrySerialization.FromLogEntry(entry);

            ((IObjectSerialization<LogEntry>)serialization).Restore(entry2);

            Assert.IsFalse(entry.HasExpired);
        }
    }
}
