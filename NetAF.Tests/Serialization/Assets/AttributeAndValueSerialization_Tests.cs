using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets.Attributes;
using NetAF.Serialization.Assets;

namespace NetAF.Tests.Serialization.Assets
{
    [TestClass]
    public class AttributeAndValueSerialization_Tests
    {
        [TestMethod]
        public void GivenAttributeWhenNameIsA_WhenFromAttribute_ThenNameIsA()
        {
            Attribute attribute = new("A", "B", 5, 10);

            AttributeAndValueSerialization result = AttributeAndValueSerialization.FromAttributeAndValue(new(attribute, 0));

            Assert.AreEqual("A", result.Name);
        }

        [TestMethod]
        public void GivenAttributeWhenDescriptionIsB_WhenFromAttribute_ThenDescriptionIsB()
        {
            Attribute attribute = new("A", "B", 5, 10);

            AttributeAndValueSerialization result = AttributeAndValueSerialization.FromAttributeAndValue(new(attribute, 0));

            Assert.AreEqual("B", result.Description);
        }

        [TestMethod]
        public void GivenAttributeWhenMinimumIs5_WhenFromAttribute_ThenMinimumIs5()
        {
            Attribute attribute = new("A", "B", 5, 10);

            AttributeAndValueSerialization result = AttributeAndValueSerialization.FromAttributeAndValue(new(attribute, 0));

            Assert.AreEqual(5, result.Minimum);
        }

        [TestMethod]
        public void GivenAttributeWhenMaximumIs10_WhenFromAttribute_ThenMaximumIs10()
        {
            Attribute attribute = new("A", "B", 5, 10);

            AttributeAndValueSerialization result = AttributeAndValueSerialization.FromAttributeAndValue(new(attribute, 0));

            Assert.AreEqual(10, result.Maximum);
        }

        [TestMethod]
        public void GivenAttributeWhenValueIs3_WhenFromAttribute_ThenValueIs3()
        {
            Attribute attribute = new("A", "B", 5, 10);

            AttributeAndValueSerialization result = AttributeAndValueSerialization.FromAttributeAndValue(new(attribute, 3));

            Assert.AreEqual(3, result.Value);
        }

        [TestMethod]
        public void GivenRestore_ThenNoExceptionThrown()
        {
            Assertions.NoExceptionThrown(() =>
            {
                AttributeAndValueSerialization value = new();
                value.Restore(new System.Collections.Generic.KeyValuePair<Attribute, int>());
            });
        }
    }
}
