using NetAF.Conversations;
using NetAF.Conversations.Instructions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NetAF.Tests.Conversations.Instructions
{
    [TestClass]
    public class ByCallback_Tests
    {
        [TestMethod]
        public void GivenCallback_WhenNext_ThenReturn1()
        {
            var paragraphs = new[]
            {
                new Paragraph(""),
                new Paragraph("")
            };
            var instruction = new ByCallback(() => new Last());

            var result = instruction.GetIndexOfNext(paragraphs[0], paragraphs);

            Assert.AreEqual(1, result);
        }
    }
}
