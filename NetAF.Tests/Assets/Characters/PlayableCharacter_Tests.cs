using NetAF.Assets;
using NetAF.Assets.Characters;
using NetAF.Assets.Interaction;
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
        public void GivenUseItem_WhenFatalEffect_ThenIsAliveIsFalse()
        {
            var item = new Item("Test", string.Empty);
            var pc = new PlayableCharacter(string.Empty, string.Empty, interaction: i =>
            {
                if (i == null)
                    return new InteractionResult(InteractionEffect.NoEffect, null);

                if (i.Identifier.Name == "Test")
                    return new InteractionResult(InteractionEffect.FatalEffect, i, "");

                return new InteractionResult(InteractionEffect.NoEffect, i);
            });

            pc.UseItem(item, pc);

            Assert.IsFalse(pc.IsAlive);
        }
    }
}
