using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Conversations;
using NetAF.Serialization.Assets;

namespace NetAF.Tests.Serialization.Assets
{
    [TestClass]
    public class ConversationSerialization_Tests
    {
        [TestMethod]
        public void GivenNoCurrentParagraph_ThenCurrentParagraphIsNoCurrentParagraph()
        {
            Conversation conversation = new(new Paragraph(string.Empty), new Paragraph(string.Empty));

            ConversationSerialization result = new(conversation);

            Assert.AreEqual(ConversationSerialization.NoCurrentParagraph, result.CurrentParagraph);
        }

        [TestMethod]
        public void GivenCurrentParagraphIsElement0_ThenCurrentParagraphIs0()
        {
            Conversation conversation = new(new Paragraph(string.Empty), new Paragraph(string.Empty));
            conversation.Next(null);

            ConversationSerialization result = new(conversation);

            Assert.AreEqual(0, result.CurrentParagraph);
        }

        [TestMethod]
        public void GivenCurrentParagraphIsElement1_ThenCurrentParagraphIs1()
        {
            Conversation conversation = new(new Paragraph(string.Empty), new Paragraph(string.Empty));
            conversation.Next(null);
            conversation.Next(null);

            var result = new ConversationSerialization(conversation);

            Assert.AreEqual(1, result.CurrentParagraph);
        }

        [TestMethod]
        public void GivenAConversation_WhenRestoreFromWithNoCurrentParagraph_ThenCurrentParagraphIsNull()
        {
            Conversation conversation = new(new Paragraph(string.Empty), new Paragraph(string.Empty));
            conversation.Next(null);
            Conversation conversation2 = new();
            ConversationSerialization serialization = new(conversation2);

            serialization.Restore(conversation);

            Assert.IsNull(conversation.CurrentParagraph);
        }

        [TestMethod]
        public void GivenAConversation_WhenRestoreFromWithCurrentParagraph1_ThenCurrentParagraphIsSecondParagraph()
        {
            Conversation conversation = new(new Paragraph(string.Empty), new Paragraph(string.Empty), new Paragraph(string.Empty));
            Conversation conversation2 = new(new Paragraph(string.Empty), new Paragraph(string.Empty), new Paragraph(string.Empty));
            conversation2.Next(null);
            conversation2.Next(null);
            ConversationSerialization serialization = new(conversation2);

            serialization.Restore(conversation);

            Assert.AreEqual(conversation.Paragraphs[1], conversation.CurrentParagraph);
        }
    }
}
