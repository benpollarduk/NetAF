using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Targets.Markup;
using NetAF.Targets.Markup.Model.Tokens;
using System.Linq;

namespace NetAF.Tests.Targets.Markup.Model.Tokens
{
    [TestClass]
    public class Tokenizer_Tests
    {
        [TestMethod]
        public void GivenEmptyString_WhenTokenize_ThenReturnEmptyArray()
        {
            var input = "";

            var result = Tokenizer.Tokenize(input).ToArray();

            Assert.IsEmpty(result);
        }

        [TestMethod]
        public void GivenRawText_WhenTokenize_ThenOneTokenWhereTypeIsTextAndTagMatchesInput()
        {
            var input = "abc";

            var result = Tokenizer.Tokenize(input).ToArray();

            Assert.HasCount(1, result);
            Assert.AreEqual(TokenType.Text, result[0].Type);
            Assert.AreEqual(input, result[0].Tag);
        }

        [TestMethod]
        public void GivenOpenTag_WhenTokenize_ThenOneTokenWhereTypeIsTextAndTagMatchesInput()
        {
            var input = MarkupSyntax.OpenTag.ToString();

            var result = Tokenizer.Tokenize(input).ToArray();

            Assert.HasCount(1, result);
            Assert.AreEqual(TokenType.Text, result[0].Type);
            Assert.AreEqual(input, result[0].Tag);
        }

        [TestMethod]
        public void GivenMonospacedH_WhenTokenize_ThenThreeTokensOpenHClose()
        {
            var input = @"[monospace]H[/monospace]";

            var result = Tokenizer.Tokenize(input).ToArray();

            Assert.HasCount(3, result);
            Assert.AreEqual(TokenType.OpenTag, result[0].Type);
            Assert.AreEqual("monospace", result[0].Tag);
            Assert.AreEqual(TokenType.Text, result[1].Type);
            Assert.AreEqual("H", result[1].Tag);
            Assert.AreEqual(TokenType.CloseTag, result[2].Type);
            Assert.AreEqual("monospace", result[2].Tag);
        }

        [TestMethod]
        public void GivenMonospacedOpenTag_WhenTokenize_ThenThreeTokensOpenOpenTagClose()
        {
            var input = @"[monospace][[/monospace]";

            var result = Tokenizer.Tokenize(input).ToArray();

            Assert.HasCount(3, result);
            Assert.AreEqual(TokenType.OpenTag, result[0].Type);
            Assert.AreEqual("monospace", result[0].Tag);
            Assert.AreEqual(TokenType.Text, result[1].Type);
            Assert.AreEqual("[", result[1].Tag);
            Assert.AreEqual(TokenType.CloseTag, result[2].Type);
            Assert.AreEqual("monospace", result[2].Tag);
        }

        [TestMethod]
        public void GivenOpenTagOnly_WhenTokenize_ThenOneTokenWhereTypeIsText()
        {
            var input = MarkupSyntax.OpenTag;

            var result = Tokenizer.Tokenize(input.ToString()).ToArray();

            Assert.HasCount(1, result);
            Assert.AreEqual(TokenType.Text, result[0].Type);
        }

        [TestMethod]
        public void GivenCloseTagOnly_WhenTokenize_ThenOneTokenWhereTypeIsTextTag()
        {
            var input = MarkupSyntax.CloseTag;

            var result = Tokenizer.Tokenize(input.ToString()).ToArray();

            Assert.HasCount(1, result);
            Assert.AreEqual(TokenType.Text, result[0].Type);
        }

        [TestMethod]
        public void GivenOpenTagContentAndCloseTag_WhenTokenize_ThenOneTokenWhereTypeIsOpenTag()
        {
            var input = $"{MarkupSyntax.OpenTag}content{MarkupSyntax.CloseTag}";

            var result = Tokenizer.Tokenize(input).ToArray();

            Assert.HasCount(1, result);
            Assert.AreEqual(TokenType.OpenTag, result[0].Type);
        }

        [TestMethod]
        public void GivenHeading_WhenTokenize_ThenOneTokenWhereTypeIsHeading()
        {
            var input = MarkupSyntax.Heading.ToString();

            var result = Tokenizer.Tokenize(input).ToArray();

            Assert.HasCount(1, result);
            Assert.AreEqual(TokenType.Heading, result[0].Type);
            Assert.AreEqual(input, result[0].Tag);
        }

        [TestMethod]
        public void GivenHeadingx2_WhenTokenize_ThenOneTokenWhereTypeIsHeading()
        {
            var input = $"{MarkupSyntax.Heading}{MarkupSyntax.Heading}";

            var result = Tokenizer.Tokenize(input).ToArray();

            Assert.HasCount(1, result);
            Assert.AreEqual(TokenType.Heading, result[0].Type);
            Assert.AreEqual(input, result[0].Tag);
        }

        [TestMethod]
        public void GivenHeadingx3_WhenTokenize_ThenOneTokenWhereTypeIsHeading()
        {
            var input = $"{MarkupSyntax.Heading}{MarkupSyntax.Heading}{MarkupSyntax.Heading}";

            var result = Tokenizer.Tokenize(input).ToArray();

            Assert.HasCount(1, result);
            Assert.AreEqual(TokenType.Heading, result[0].Type);
            Assert.AreEqual(input, result[0].Tag);
        }

        [TestMethod]
        public void GivenHeadingx4_WhenTokenize_ThenOneTokenWhereTypeIsHeading()
        {
            var input = $"{MarkupSyntax.Heading}{MarkupSyntax.Heading}{MarkupSyntax.Heading}{MarkupSyntax.Heading}";

            var result = Tokenizer.Tokenize(input).ToArray();

            Assert.HasCount(1, result);
            Assert.AreEqual(TokenType.Heading, result[0].Type);
            Assert.AreEqual(input, result[0].Tag);
        }

        [TestMethod]
        public void GivenNewline_WhenTokenize_ThenOneTokenWhereTypeIsNewline()
        {
            var input = MarkupSyntax.NewLine;

            var result = Tokenizer.Tokenize(input.ToString()).ToArray();

            Assert.HasCount(1, result);
            Assert.AreEqual(TokenType.NewLine, result[0].Type);
            Assert.AreEqual(input.ToString(), result[0].Tag);
        }
    }
}
