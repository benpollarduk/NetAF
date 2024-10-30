using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets.Characters;
using NetAF.Serialization.Assets;

namespace NetAF.Tests.Serialization.Assets
{
    [TestClass]
    public class NonPlayableCharacterSerialization_Tests
    {
        [TestMethod]
        public void GivenNullConversation_ThenConversationNotNull()
        {
            var character = new NonPlayableCharacter(string.Empty, string.Empty);

            var result = new NonPlayableCharacterSerialization(character);

            Assert.IsNotNull(result.Conversation);
        }

        [TestMethod]
        public void GivenNotNullConversation_ThenConversationNotNull()
        {
            var character = new NonPlayableCharacter(string.Empty, string.Empty)
            {
                Conversation = new NetAF.Conversations.Conversation()
            };

            var result = new NonPlayableCharacterSerialization(character);

            Assert.IsNotNull(result.Conversation);
        }
    }
}
