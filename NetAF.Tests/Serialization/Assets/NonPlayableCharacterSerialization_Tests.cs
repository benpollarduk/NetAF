using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            NonPlayableCharacter character = new(string.Empty, string.Empty);

            NonPlayableCharacterSerialization result = new(character);

            Assert.IsNotNull(result.Conversation);
        }

        [TestMethod]
        public void GivenNotNullConversation_ThenConversationNotNull()
        {
            NonPlayableCharacter character = new(string.Empty, string.Empty)
            {
                Conversation = new()
            };

            NonPlayableCharacterSerialization result = new(character);

            Assert.IsNotNull(result.Conversation);
        }

        [TestMethod]
        public void GivenCharacter_WhenRestoreFromCharacterConversationElement0_ThenCurrentParagraphIsNotNull()
        {
            NonPlayableCharacter character = new(string.Empty, string.Empty, new Conversation(new Paragraph(string.Empty), new Paragraph(string.Empty)));
            NonPlayableCharacter character2 = new(string.Empty, string.Empty, new Conversation(new Paragraph(string.Empty), new Paragraph(string.Empty)));
            character2.Conversation.Next(null);
            NonPlayableCharacterSerialization serialization = new(character2);

            serialization.Restore(character);

            Assert.IsNotNull(character.Conversation.CurrentParagraph);
        }
    }
}
