using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets;
using NetAF.Assets.Locations;
using NetAF.Logic.Arrangement;

namespace NetAF.Tests.Logic.Arrangement
{
    [TestClass]
    public class ItemFromTo_Tests
    {
        [TestMethod]
        public void GivenItem_WhenConstructed_ThenItemIsSet()
        {
            Item item = new("ITEM", string.Empty);
            Room from = new("FROM", string.Empty);
            Room to = new("TO", string.Empty);

            ItemFromTo result = new(item, from, to);

            Assert.AreEqual(item, result.Item);
        }

        [TestMethod]
        public void GivenFrom_WhenConstructed_ThenFromIsSet()
        {
            Item item = new("ITEM", string.Empty);
            Room from = new("FROM", string.Empty);
            Room to = new("TO", string.Empty);

            ItemFromTo result = new(item, from, to);

            Assert.AreEqual((IItemContainer)from, result.From);
        }

        [TestMethod]
        public void GivenTo_WhenConstructed_ThenToIsSet()
        {
            Item item = new("ITEM", string.Empty);
            Room from = new("FROM", string.Empty);
            Room to = new("TO", string.Empty);

            ItemFromTo result = new(item, from, to);

            Assert.AreEqual((IItemContainer)to, result.To);
        }
    }
}
