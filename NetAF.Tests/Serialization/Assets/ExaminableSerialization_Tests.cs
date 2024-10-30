using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets;
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
        public void GivenNoAttributes_ThenAttributesLengthIs0()
        {
            var examinable = new Item(string.Empty, string.Empty);

            var result = new ExaminableSerialization(examinable);

            Assert.AreEqual(0, result.Attributes.Length);
        }

        [TestMethod]
        public void Given1Attribute_ThenAttributesLengthIs1()
        {
            var examinable = new Item(string.Empty, string.Empty);
            examinable.Attributes.Add(new NetAF.Assets.Attributes.Attribute(string.Empty, string.Empty, 0, 1), 1);

            var result = new ExaminableSerialization(examinable);

            Assert.AreEqual(1, result.Attributes.Length);
        }
    }
}
