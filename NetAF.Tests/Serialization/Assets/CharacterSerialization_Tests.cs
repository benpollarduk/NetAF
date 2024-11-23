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
        public void GivenIsAliveIsFalse_WhenFromCharacter_ThenIsAliveIsFalse()
        {
            PlayableCharacter character = new(string.Empty, string.Empty);
            character.Kill();

            CharacterSerialization result = CharacterSerialization.FromCharacter(character);

            Assert.IsFalse(result.IsAlive);
        }

        [TestMethod]
        public void GivenIsAliveIsTrue_WhenFromCharacter_ThenIsAliveIsTrue()
        {
            PlayableCharacter character = new(string.Empty, string.Empty);

            CharacterSerialization result = CharacterSerialization.FromCharacter(character);

            Assert.IsTrue(result.IsAlive);
        }

        [TestMethod]
        public void GivenNoItems_WhenFromCharacter_ThenItemsLengthIs0()
        {
            PlayableCharacter character = new(string.Empty, string.Empty);

            CharacterSerialization result = CharacterSerialization.FromCharacter(character);

            Assert.AreEqual(0, result.Items.Length);
        }

        [TestMethod]
        public void Given1Item_WhenFromCharacter_ThenItemsLengthIs1()
        {
            PlayableCharacter character = new(string.Empty, string.Empty);
            character.AddItem(new Item(string.Empty, string.Empty));

            CharacterSerialization result = CharacterSerialization.FromCharacter(character);

            Assert.AreEqual(1, result.Items.Length);
        }

        [TestMethod]
        public void GivenCharacterThatIsAlive_WhenRestoreFromCharacterThatIsNotAlive_ThenCharacterIsNotAlive()
        {
            PlayableCharacter character = new(string.Empty, string.Empty, [new Item("a", "b")]);
            PlayableCharacter character2 = new(string.Empty, string.Empty, [new Item("a", "b")]);
            character2.Kill();
            CharacterSerialization serialization = CharacterSerialization.FromCharacter(character2);

            serialization.Restore(character);

            Assert.IsFalse(character.IsAlive);
        }
    }
}
