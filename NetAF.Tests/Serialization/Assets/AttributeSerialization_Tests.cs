using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets.Attributes;
using NetAF.Serialization.Assets;

namespace NetAF.Tests.Serialization.Assets
{
    [TestClass]
    public class AttributeSerialization_Tests
    {
        [TestMethod]
        public void GivenAttributeWhenNameIsA_ThenNameIsA()
        {
            var attribute = new NetAF.Assets.Attributes.Attribute("A", "B", 5, 10);

            var result = new AttributeSerialization(attribute);

            Assert.AreEqual("A", result.Name);
        }

        [TestMethod]
        public void GivenAttributeWhenDescriptionIsB_ThenDescriptionIsB()
        {
            var attribute = new NetAF.Assets.Attributes.Attribute("A", "B", 5, 10);

            var result = new AttributeSerialization(attribute);

            Assert.AreEqual("B", result.Description);
        }

        [TestMethod]
        public void GivenAttributeWhenMinimumIs5_ThenMinimumIs5()
        {
            var attribute = new NetAF.Assets.Attributes.Attribute("A", "B", 5, 10);

            var result = new AttributeSerialization(attribute);

            Assert.AreEqual(5, result.Minimum);
        }

        [TestMethod]
        public void GivenAttributeWhenMaximumIs10_ThenMaximumIs10()
        {
            var attribute = new NetAF.Assets.Attributes.Attribute("A", "B", 5, 10);

            var result = new AttributeSerialization(attribute);

            Assert.AreEqual(10, result.Maximum);
        }

        [TestMethod]
        public void GivenEmptyAttribute_WhenRestoreFrom_ThenAttributeRestoredCorrectly()
        {
            var attribute = new Attribute("a", "b", 1, 10);
            var attribute2 = new Attribute(string.Empty, string.Empty, 0, 0);
            var serialization = new AttributeSerialization(attribute);

            attribute2.RestoreFrom(serialization);

            Assert.AreEqual("a", attribute2.Name);
            Assert.AreEqual("b", attribute2.Description);
            Assert.AreEqual(1, attribute2.Minimum);
            Assert.AreEqual(10, attribute2.Maximum);
        }
    }
}
