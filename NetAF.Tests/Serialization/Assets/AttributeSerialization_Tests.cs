using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets.Attributes;
using NetAF.Serialization.Assets;

namespace NetAF.Tests.Serialization.Assets
{
    [TestClass]
    public class AttributeSerialization_Tests
    {
        [TestMethod]
        public void GivenAttributeWhenNameIsA_WhenFromAttribute_ThenNameIsA()
        {
            Attribute attribute = new("A", "B", 5, 10);

            AttributeSerialization result = AttributeSerialization.FromAttribute(attribute);

            Assert.AreEqual("A", result.Name);
        }

        [TestMethod]
        public void GivenAttributeWhenDescriptionIsB_WhenFromAttribute_ThenDescriptionIsB()
        {
            Attribute attribute = new("A", "B", 5, 10);

            AttributeSerialization result = AttributeSerialization.FromAttribute(attribute);

            Assert.AreEqual("B", result.Description);
        }

        [TestMethod]
        public void GivenAttributeWhenMinimumIs5_WhenFromAttribute_ThenMinimumIs5()
        {
            Attribute attribute = new("A", "B", 5, 10);

            AttributeSerialization result = AttributeSerialization.FromAttribute(attribute);

            Assert.AreEqual(5, result.Minimum);
        }

        [TestMethod]
        public void GivenAttributeWhenMaximumIs10_WhenFromAttribute_ThenMaximumIs10()
        {
            Attribute attribute = new("A", "B", 5, 10);

            AttributeSerialization result = AttributeSerialization.FromAttribute(attribute);

            Assert.AreEqual(10, result.Maximum);
        }

        [TestMethod]
        public void GivenEmptyAttribute_WhenRestoreFrom_ThenAttributeRestoredCorrectly()
        {
            Attribute attribute = new("a", "b", 1, 10);
            Attribute attribute2 = new(string.Empty, string.Empty, 0, 0);
            AttributeSerialization serialization = AttributeSerialization.FromAttribute(attribute);

            serialization.Restore(attribute2);

            Assert.AreEqual("a", attribute2.Name);
            Assert.AreEqual("b", attribute2.Description);
            Assert.AreEqual(1, attribute2.Minimum);
            Assert.AreEqual(10, attribute2.Maximum);
        }
    }
}
