using NetAF.Assets;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NetAF.Tests.Assets
{
    [TestClass]
    public class Item_Tests
    {
        [TestMethod]
        public void Given2Items_WhenInteract_ThenNoChange()
        {
            var item = new Item(string.Empty, string.Empty);
            var item2 = new Item(string.Empty, string.Empty);

            var result = item.Interact(item2);

            Assert.AreEqual(InteractionResult.NoChange, result.Result);
        }
    }
}
