using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets.Attributes;
using NetAF.Serialization;
using NetAF.Serialization.Assets;
using System.Linq;

namespace NetAF.Tests.Serialization.Assets
{
    [TestClass]
    public class AttributeManagerSerialization_Tests
    {
        [TestMethod]
        public void Given1Attributes_WhenFromAttributeManager_ThenValuesHas1Element()
        {
            AttributeManager attributeManager = new();
            attributeManager.Add(new Attribute("A", "B", 5, 10), 0);

            var result = AttributeManagerSerialization.FromAttributeManager(attributeManager);

            Assert.AreEqual(1, result.Values.Count);
        }

        [TestMethod]
        public void Given0AttributesButARestorationWith1Attribute_WhenRestore_ThenValuesHas1Element()
        {
            AttributeManager attributeManager1 = new();
            AttributeManager attributeManager2 = new();
            attributeManager2.Add(new Attribute("a", "b", 1, 10), 5);
            var serialization = AttributeManagerSerialization.FromAttributeManager(attributeManager2);

            ((IObjectSerialization<AttributeManager>)serialization).Restore(attributeManager1);

            Assert.AreEqual(1, attributeManager1.Count);
        }

        [TestMethod]
        public void Given0Attributes_WhenRestoreFromARestorationWith1Attribute_When_Restore_ThenAttributeRestoredCorrectly()
        {
            AttributeManager attributeManager1 = new();
            AttributeManager attributeManager2 = new();
            attributeManager2.Add(new Attribute("a", "b", 1, 10), 5);
            var serialization = AttributeManagerSerialization.FromAttributeManager(attributeManager2);

            ((IObjectSerialization<AttributeManager>)serialization).Restore(attributeManager1);
            var attributeDictionary = attributeManager2.GetAsDictionary();
            var attribute = attributeDictionary.ElementAt(0).Key;
            var count = attributeDictionary.ElementAt(0).Value;

            Assert.AreEqual("a", attribute.Name);
            Assert.AreEqual("b", attribute.Description);
            Assert.AreEqual(1, attribute.Minimum);
            Assert.AreEqual(10, attribute.Maximum);
            Assert.AreEqual(5, count);
        }
    }
}
