using NetAF.Assets;
using NetAF.Assets.Characters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NetAF.Tests.Assets.Characters
{
    [TestClass]
    public class PlayableCharacter_Tests
    {
        [TestMethod]
        public void GivenNewCharacter_WhenGetIsAlive_ThenReturnTrue()
        {
            var pc = new PlayableCharacter(string.Empty, string.Empty);

            Assert.IsTrue(pc.IsAlive);
        }

        [TestMethod]
        public void GivenNewCharacter_WhenKill_ThenIsAliveIsFalse()
        {
            var pc = new PlayableCharacter(string.Empty, string.Empty);

            pc.Kill();

            Assert.IsFalse(pc.IsAlive);
        }

        [TestMethod]
        public void GivenDoesNotHaveItem_WhenHasItem_ThenFalse()
        {
            var pc = new PlayableCharacter(string.Empty, string.Empty);
            var item = new Item(string.Empty, string.Empty);

            var result = pc.HasItem(item);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenHasItem_WhenHasItem_ThenTrue()
        {
            var pc = new PlayableCharacter(string.Empty, string.Empty);
            var item = new Item(string.Empty, string.Empty);
            pc.AddItem(item);

            var result = pc.HasItem(item);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenAnItem_WhenRemoveItem_ThenHasItemIsFalse()
        {
            var pc = new PlayableCharacter(string.Empty, string.Empty);
            var item = new Item(string.Empty, string.Empty);
            pc.AddItem(item);
            pc.RemoveItem(item);
            
            var result = pc.HasItem(item);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenNoItems_WhenExamine_ThenANonEmptyDescription()
        {
            var pc = new PlayableCharacter("A", "A");

            var result = pc.Examine(new ExaminationScene(null));

            Assert.AreNotEqual(string.Empty, result.Description);
        }

        [TestMethod]
        public void GivenSingleInvisibleItem_WhenExamine_ThenANonEmptyDescription()
        {
            var pc = new PlayableCharacter("A", "A");
            var item = new Item("B", "B") { IsPlayerVisible = false };
            pc.AddItem(item);

            var result = pc.Examine(new ExaminationScene(null));

            Assert.AreNotEqual(string.Empty, result.Description);
        }

        [TestMethod]
        public void GivenSingleItem_WhenExamine_ThenANonEmptyDescription()
        {
            var pc = new PlayableCharacter("A", "A");
            var item = new Item("B", "B");
            pc.AddItem(item);

            var result = pc.Examine(new ExaminationScene(null));

            Assert.AreNotEqual(string.Empty, result.Description);
        }

        [TestMethod]
        public void GivenMultipleItems_WhenExamine_ThenANonEmptyDescription()
        {
            var pc = new PlayableCharacter("A", "A");
            var item1 = new Item("B", "B");
            var item2 = new Item("C", "C");
            pc.AddItem(item1);
            pc.AddItem(item2);

            var result = pc.Examine(new ExaminationScene(null));

            Assert.AreNotEqual(string.Empty, result.Description);
        }
    }
}
