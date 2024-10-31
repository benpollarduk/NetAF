using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets;
using NetAF.Assets.Attributes;
using NetAF.Serialization.Assets;

namespace NetAF.Tests.Serialization.Assets
{
    [TestClass]
    public class ExaminableSerialization_Tests
    {
        [TestMethod]
        public void GivenIdentifierIsA_ThenIdentifierIsA()
        {
            var examinable = new Item("A", string.Empty);

            var result = new ExaminableSerialization(examinable);

            Assert.AreEqual("A", result.Identifier);
        }

        [TestMethod]
        public void GivenIsPlayerVisibleIsFalse_ThenIsPlayerVisibleIsFalse()
        {
            var examinable = new Item(string.Empty, string.Empty) { IsPlayerVisible = false };

            var result = new ExaminableSerialization(examinable);

            Assert.IsFalse(result.IsPlayerVisible);
        }

        [TestMethod]
        public void GivenIsPlayerVisibleIsTrue_ThenIsPlayerVisibleIsTrue()
        {
            var examinable = new Item(string.Empty, string.Empty) { IsPlayerVisible = true };

            var result = new ExaminableSerialization(examinable);

            Assert.IsTrue(result.IsPlayerVisible);
        }

        [TestMethod]
        public void GivenNoAttributes_ThenAttributeNotNull()
        {
            var examinable = new Item(string.Empty, string.Empty);

            var result = new ExaminableSerialization(examinable);

            Assert.IsNotNull(result.AttributeManager);
        }

        [TestMethod]
        public void GivenAnExaminable_WhenRestoreFrom_ThenIsPlayerVisibleSetCorrectly()
        {
            var item = new Item(string.Empty, string.Empty) { IsPlayerVisible = false };
            var item2 = new Item(string.Empty, string.Empty) { IsPlayerVisible = true };
            var serialization = new ItemSerialization(item2);

            item.RestoreFrom(serialization);

            Assert.IsTrue(item.IsPlayerVisible);
        }

        [TestMethod]
        public void GivenAnExaminable_WhenRestoreFrom_ThenAttributesSetCorrectly()
        {
            var item = new Item(string.Empty, string.Empty);
            var item2 = new Item(string.Empty, string.Empty);
            item2.Attributes.Add(new Attribute(string.Empty, string.Empty, 0, 1), 1);
            var serialization = new ItemSerialization(item2);

            item.RestoreFrom(serialization);

            Assert.AreEqual(1, item.Attributes.Count);
        }
    }
}
