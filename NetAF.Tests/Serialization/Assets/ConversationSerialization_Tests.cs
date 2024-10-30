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
            var conversation = new Conversation(new Paragraph(string.Empty), new Paragraph(string.Empty));

            var result = new ConversationSerialization(conversation);

            Assert.AreEqual(ConversationSerialization.NoCurrentParagraph, result.CurrentParagraph);
        }

        [TestMethod]
        public void GivenCurrentParagraphIsElement0_ThenCurrentParagraphIs0()
        {
            var conversation = new Conversation(new Paragraph(string.Empty), new Paragraph(string.Empty));
            conversation.Next(null);

            var result = new ConversationSerialization(conversation);

            Assert.AreEqual(0, result.CurrentParagraph);
        }

        [TestMethod]
        public void GivenCurrentParagraphIsElement1_ThenCurrentParagraphIs1()
        {
            var conversation = new Conversation(new Paragraph(string.Empty), new Paragraph(string.Empty));
            conversation.Next(null);
            conversation.Next(null);

            var result = new ConversationSerialization(conversation);

            Assert.AreEqual(1, result.CurrentParagraph);
        }
    }
}
