using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets;
using NetAF.Assets.Attributes;
using NetAF.Serialization.Assets;

namespace NetAF.Tests.Serialization.Assets
{
    [TestClass]
    public class ItemSerialization_Tests
    {
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
