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

        [TestMethod]
        public void GivenDefaultItem_WhenGetIsTakeable_ThenFalse()
        {
            var item = new Item(string.Empty, string.Empty);

            Assert.IsFalse(item.IsTakeable);
        }

        [TestMethod]
        public void GivenTakeableItem_WhenGetIsTakeable_ThenTrue()
        {
            var item = new Item(string.Empty, string.Empty, true);

            Assert.IsTrue(item.IsTakeable);
        }

        [TestMethod]
        public void GivenCustomInteraction_WhenInteract_ThenCustomResultReturned()
        {
            var item = new Item(string.Empty, string.Empty, interaction: i => new Interaction(InteractionResult.ItemExpires, i));
            var item2 = new Item(string.Empty, string.Empty);

            var result = item.Interact(item2);

            Assert.AreEqual(InteractionResult.ItemExpires, result.Result);
        }

        [TestMethod]
        public void GivenItemWithIdentifier_WhenGetIdentifier_ThenIdentifierIsCorrect()
        {
            var item = new Item("Sword", string.Empty);

            Assert.AreEqual("SWORD", item.Identifier.IdentifiableName);
        }

        [TestMethod]
        public void GivenItemWithDescription_WhenGetDescription_ThenDescriptionIsCorrect()
        {
            var item = new Item(string.Empty, "A sharp blade.");

            var result = item.Description.GetDescription();

            Assert.AreEqual("A sharp blade.", result);
        }

        [TestMethod]
        public void GivenDefaultItem_WhenGetCommands_ThenCommandsIsEmpty()
        {
            var item = new Item(string.Empty, string.Empty);

            Assert.AreEqual(0, item.Commands.Length);
        }
    }
}
