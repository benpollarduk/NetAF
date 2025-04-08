using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Information;
using NetAF.Serialization;
using NetAF.Serialization.Assets;

namespace NetAF.Tests.Serialization.Assets
{
    [TestClass]
    public class InformationEntry_Tests
    {
        [TestMethod]
        public void GivenNameA_WhenFromInformationEntry_ThenNameIsA()
        {
            InformationEntry entry = new("A", string.Empty);

            InformationEntrySerialization result = InformationEntrySerialization.FromInformationEntry(entry);

            Assert.AreEqual("A", result.Name);
        }

        [TestMethod]
        public void GivenContentA_WhenFromInformationEntry_ThenContentIsA()
        {
            InformationEntry entry = new(string.Empty, "A");

            InformationEntrySerialization result = InformationEntrySerialization.FromInformationEntry(entry);

            Assert.AreEqual("A", result.Content);
        }

        [TestMethod]
        public void GivenHasExpired_WhenFromInformationEntry_ThenHasExpired()
        {
            InformationEntry entry = new(string.Empty, string.Empty);
            entry.Expire();

            InformationEntrySerialization result = InformationEntrySerialization.FromInformationEntry(entry);

            Assert.IsTrue(result.HasExpired);
        }

        [TestMethod]
        public void GivenHasExpired_WhenRestoreFromNotExpired_ThenHasExpiredIsFalse()
        {
            InformationEntry entry = new(string.Empty, string.Empty);
            InformationEntry entry2 = new(string.Empty, string.Empty);
            entry2.Expire();
            InformationEntrySerialization serialization = InformationEntrySerialization.FromInformationEntry(entry);

            ((IObjectSerialization<InformationEntry>)serialization).Restore(entry2);

            Assert.IsFalse(entry.HasExpired);
        }
    }
}
