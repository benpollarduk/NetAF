using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets;
using NetAF.Assets.Characters;
using NetAF.Conversations;
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

        [TestMethod]
        public void GivenCharacter_WhenRestoreFromCharacterConversationElement0_ThenCurrentParagraphIsNotNull()
        {
            var character = new NonPlayableCharacter(string.Empty, string.Empty, new Conversation(new Paragraph(string.Empty), new Paragraph(string.Empty)));
            var character2 = new NonPlayableCharacter(string.Empty, string.Empty, new Conversation(new Paragraph(string.Empty), new Paragraph(string.Empty)));
            character2.Conversation.Next(null);
            var serialization = new NonPlayableCharacterSerialization(character2);

            character.RestoreFrom(serialization);

            Assert.IsNotNull(character.Conversation.CurrentParagraph);
        }
    }
}
