using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets;
using NetAF.Assets.Attributes;
using NetAF.Serialization;
using NetAF.Serialization.Assets;

namespace NetAF.Tests.Serialization.Assets
{
    [TestClass]
    public class ItemSerialization_Tests
    {
        [TestMethod]
        public void GivenAnExaminable_WhenRestoreFrom_ThenAttributesSetCorrectly()
        {
            Item item = new(string.Empty, string.Empty);
            Item item2 = new(string.Empty, string.Empty);
            item2.Attributes.Add(new Attribute(string.Empty, string.Empty, 0, 1, true), 1);
            ItemSerialization serialization = ItemSerialization.FromItem(item2);

            ((IObjectSerialization<Item>)serialization).Restore(item);

            Assert.AreEqual(1, item.Attributes.Count);
        }
    }
}
