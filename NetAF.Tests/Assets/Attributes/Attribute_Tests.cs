using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets.Attributes;
using NetAF.Serialization.Assets;

namespace NetAF.Tests.Assets.Attributes
{
    [TestClass]
    internal class Attribute_Tests
    {
        [TestMethod]
        public void GivenFromSerialization_ThenAttributeRestoredCorrectly()
        {
            var attribute = new Attribute("a", "b", 1, 10);
            var serialization = new AttributeSerialization(attribute);

            var result = Attribute.FromSerialization(serialization);

            Assert.AreEqual("a", result.Name);
            Assert.AreEqual("b", result.Description);
            Assert.AreEqual(1, result.Minimum);
            Assert.AreEqual(10, result.Maximum);
        }
    }
}
