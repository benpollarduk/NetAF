using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets;
using NetAF.Assets.Characters;
using NetAF.Serialization.Assets;

namespace NetAF.Tests.Serialization.Assets
{
    [TestClass]
    public class CharacterSerialization_Tests
    {
        [TestMethod]
        public void GivenIsAliveIsFalse_ThenIsAliveIsFalse()
        {
            var character = new PlayableCharacter(string.Empty, string.Empty);
            character.Kill();

            var result = new CharacterSerialization(character);

            Assert.IsFalse(result.IsAlive);
        }

        [TestMethod]
        public void GivenIsAliveIsTrue_ThenIsAliveIsTrue()
        {
            var character = new PlayableCharacter(string.Empty, string.Empty);

            var result = new CharacterSerialization(character);

            Assert.IsTrue(result.IsAlive);
        }

        [TestMethod]
        public void GivenNoItems_ThenItemsLengthIs0()
        {
            var character = new PlayableCharacter(string.Empty, string.Empty);

            var result = new CharacterSerialization(character);

            Assert.AreEqual(0, result.Items.Length);
        }

        [TestMethod]
        public void Given1Item_ThenItemsLengthIs1()
        {
            var character = new PlayableCharacter(string.Empty, string.Empty);
            character.AcquireItem(new Item(string.Empty, string.Empty));

            var result = new CharacterSerialization(character);

            Assert.AreEqual(1, result.Items.Length);
        }

        [TestMethod]
        public void GivenCharacterThatIsAlive_WhenRestoreFromCharacterThatIsNotAlive_ThenCharacterIsNotAlive()
        {
            var character = new PlayableCharacter(string.Empty, string.Empty, new Item("a", "b"));
            var character2 = new PlayableCharacter(string.Empty, string.Empty, new Item("a", "b"));
            character2.Kill();
            var serialization = new CharacterSerialization(character2);

            character.RestoreFrom(serialization);

            Assert.IsFalse(character.IsAlive);
        }
    }
}
