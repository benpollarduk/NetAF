using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets.Characters;
using NetAF.Assets.Locations;
using NetAF.Logic.Arrangement;

namespace NetAF.Tests.Logic.Arrangement
{
    [TestClass]
    public class NonPlayableCharacterFromTo_Tests
    {
        [TestMethod]
        public void GivenCharacter_WhenConstructed_ThenCharacterIsSet()
        {
            NonPlayableCharacter character = new("NPC", string.Empty);
            Room from = new("FROM", string.Empty);
            Room to = new("TO", string.Empty);

            NonPlayableCharacterFromTo result = new(character, from, to);

            Assert.AreEqual(character, result.Character);
        }

        [TestMethod]
        public void GivenFrom_WhenConstructed_ThenFromIsSet()
        {
            NonPlayableCharacter character = new("NPC", string.Empty);
            Room from = new("FROM", string.Empty);
            Room to = new("TO", string.Empty);

            NonPlayableCharacterFromTo result = new(character, from, to);

            Assert.AreEqual(from, result.From);
        }

        [TestMethod]
        public void GivenTo_WhenConstructed_ThenToIsSet()
        {
            NonPlayableCharacter character = new("NPC", string.Empty);
            Room from = new("FROM", string.Empty);
            Room to = new("TO", string.Empty);

            NonPlayableCharacterFromTo result = new(character, from, to);

            Assert.AreEqual(to, result.To);
        }
    }
}
