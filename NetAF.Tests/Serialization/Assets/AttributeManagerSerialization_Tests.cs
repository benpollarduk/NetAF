using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets.Attributes;
using NetAF.Serialization.Assets;

namespace NetAF.Tests.Serialization.Assets
{
    [TestClass]
    public class AttributeManagerSerialization_Tests
    {
        [TestMethod]
        public void Given1Attributes_ThenValuesHas1Element()
        {
            var attributeManager = new AttributeManager();
            attributeManager.Add(new Attribute("A", "B", 5, 10), 0);

            var result = new AttributeManagerSerialization(attributeManager);

            Assert.AreEqual(1, result.Values.Count);
        }
    }
}
