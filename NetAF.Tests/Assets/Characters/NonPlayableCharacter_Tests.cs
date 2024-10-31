using NetAF.Assets;
using NetAF.Assets.Characters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NetAF.Tests.Assets.Characters
{
    [TestClass]
    public class NonPlayableCharacter_Tests
    {
        [TestMethod]
        public void GivenNewCharacter_WhenGetIsAlive_ThenReturnTrue()
        {
            var npc = new NonPlayableCharacter(string.Empty, string.Empty);

            Assert.IsTrue(npc.IsAlive);
        }

        [TestMethod]
        public void GivenNewCharacter_WhenKill_ThenIsAliveIsFalse()
        {
            var npc = new NonPlayableCharacter(string.Empty, string.Empty);

            npc.Kill();

            Assert.IsFalse(npc.IsAlive);
        }

        [TestMethod]
        public void GivenDoesNotHaveItem_WhenHasItem_ThenFalse()
        {
            var npc = new NonPlayableCharacter(string.Empty, string.Empty);
            var item = new Item(string.Empty, string.Empty);

            var result = npc.HasItem(item);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenHasItem_WhenHasItem_ThenTrue()
        {
            var npc = new NonPlayableCharacter(string.Empty, string.Empty);
            var item = new Item(string.Empty, string.Empty);
            npc.AddItem(item);

            var result = npc.HasItem(item);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenAnItem_WhenRemoveItem_ThenHasItemIsFalse()
        {
            var npc = new NonPlayableCharacter(string.Empty, string.Empty);
            var item = new Item(string.Empty, string.Empty);
            npc.AddItem(item);
            npc.RemoveItem(item);

            var result = npc.HasItem(item);

            Assert.IsFalse(result);
        }
    }
}
