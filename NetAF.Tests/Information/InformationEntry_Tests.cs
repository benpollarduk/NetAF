using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Information;
using NetAF.Serialization.Assets;

namespace NetAF.Tests.Information
{
    [TestClass]
    public class InformationEntry_Tests
    {
        [TestMethod]
        public void GivenSerialization_WhenFromSerialization_ThenRestoredCorrectly()
        {
            InformationEntry entry = new("a", "b");
            entry.Expire();
            InformationEntrySerialization serialization = InformationEntrySerialization.FromInformationEntry(entry);

            var result = InformationEntry.FromSerialization(serialization);

            Assert.AreEqual("a", result.Name);
            Assert.AreEqual("b", result.Content);
            Assert.IsTrue(result.HasExpired);
        }
    }
}
