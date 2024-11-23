using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets.Attributes;
using NetAF.Serialization.Assets;

namespace NetAF.Tests.Assets.Attributes
{
    [TestClass]
    internal class Attribute_Tests
    {
        [TestMethod]
        public void GivenFromSerialization_WhenFromAttribute_ThenAttributeRestoredCorrectly()
        {
            Attribute attribute = new("a", "b", 1, 10);
            AttributeSerialization serialization = AttributeSerialization.FromAttribute(attribute);

            var result = Attribute.FromSerialization(serialization);

            Assert.AreEqual("a", result.Name);
            Assert.AreEqual("b", result.Description);
            Assert.AreEqual(1, result.Minimum);
            Assert.AreEqual(10, result.Maximum);
        }
    }
}
