using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Information;
using NetAF.Serialization;
using NetAF.Serialization.Assets;

namespace NetAF.Tests.Serialization.Assets
{
    [TestClass]
    public class InformationManagerSerialization_Tests
    {
        [TestMethod]
        public void Given1Entry_WhenFromAttributeManager_ThenValuesHas1Element()
        {
            InformationManager informationManager = new();
            informationManager.Add(new("A", "B"));

            var result = InformationManagerSerialization.FromInformationManager(informationManager);

            Assert.AreEqual(1, result.Entries.Count);
        }

        [TestMethod]
        public void Given0EntriesButARestorationWith1Entry_WhenRestore_ThenValuesHas1Entry()
        {
            InformationManager informationManager1 = new();
            InformationManager informationManager2 = new();
            informationManager2.Add(new InformationEntry("a", "b"));
            var serialization = InformationManagerSerialization.FromInformationManager(informationManager2);

            ((IObjectSerialization<InformationManager>)serialization).Restore(informationManager1);

            Assert.AreEqual(1, informationManager1.Count);
        }
    }
}
