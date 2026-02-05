using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Targets.Markup;
using NetAF.Targets.Markup.Ast;
using NetAF.Targets.Markup.Ast.Nodes;

namespace NetAF.Tests.Targets.Markup.Ast
{
    [TestClass]
    public class AstParser_Tests
    {
        [TestMethod]
        public void GivenEmptyString_WhenTryParse_ThenReturnTrueAndDocumentWithNoBlocks()
        {
            var input = string.Empty;

            var result = AstParser.TryParse(input, out var doc);

            Assert.IsTrue(result);
            Assert.HasCount(0, doc.Blocks);
        }

        [TestMethod]
        public void GivenHeadingLevel1_WhenTryParse_ThenReturnTrueAndDocumentWithHeadingH1()
        {
            var input = $"{MarkupSyntax.Heading} Test";

            var result = AstParser.TryParse(input, out var doc);
            var heading = doc.Blocks[0] as HeadingNode;

            Assert.IsTrue(result);
            Assert.HasCount(1, doc.Blocks);
            Assert.IsNotNull(heading);
            Assert.AreEqual(HeadingLevel.H1, heading.Level);
            Assert.AreEqual("Test", heading.Text);
        }

        [TestMethod]
        public void GivenHeadingLevel2_WhenTryParse_ThenReturnTrueAndDocumentWithHeadingH2()
        {
            var input = $"{MarkupSyntax.Heading}{MarkupSyntax.Heading} Test";

            var result = AstParser.TryParse(input, out var doc);
            var heading = doc.Blocks[0] as HeadingNode;

            Assert.IsTrue(result);
            Assert.HasCount(1, doc.Blocks);
            Assert.IsNotNull(heading);
            Assert.AreEqual(HeadingLevel.H2, heading.Level);
            Assert.AreEqual("Test", heading.Text);
        }

        [TestMethod]
        public void GivenHeadingLevel3_WhenTryParse_ThenReturnTrueAndDocumentWithHeadingH3()
        {
            var input = $"{MarkupSyntax.Heading}{MarkupSyntax.Heading}{MarkupSyntax.Heading} Test";

            var result = AstParser.TryParse(input, out var doc);
            var heading = doc.Blocks[0] as HeadingNode;

            Assert.IsTrue(result);
            Assert.HasCount(1, doc.Blocks);
            Assert.IsNotNull(heading);
            Assert.AreEqual(HeadingLevel.H3, heading.Level);
            Assert.AreEqual("Test", heading.Text);
        }

        [TestMethod]
        public void GivenHeadingLevel4_WhenTryParse_ThenReturnTrueAndDocumentWithHeadingH4()
        {
            var input = $"{MarkupSyntax.Heading}{MarkupSyntax.Heading}{MarkupSyntax.Heading}{MarkupSyntax.Heading} Test";

            var result = AstParser.TryParse(input, out var doc);
            var heading = doc.Blocks[0] as HeadingNode;

            Assert.IsTrue(result);
            Assert.HasCount(1, doc.Blocks);
            Assert.IsNotNull(heading);
            Assert.AreEqual(HeadingLevel.H4, heading.Level);
            Assert.AreEqual("Test", heading.Text);
        }

        [TestMethod]
        public void GivenHeadingLevel5_WhenTryParse_ThenReturnTrueAndDocumentWithHeadingH4()
        {
            var input = $"{MarkupSyntax.Heading}{MarkupSyntax.Heading}{MarkupSyntax.Heading}{MarkupSyntax.Heading}{MarkupSyntax.Heading} Test";

            var result = AstParser.TryParse(input, out var doc);
            var heading = doc.Blocks[0] as HeadingNode;

            Assert.IsTrue(result);
            Assert.HasCount(1, doc.Blocks);
            Assert.IsNotNull(heading);
            Assert.AreEqual(HeadingLevel.H4, heading.Level);
            Assert.AreEqual("Test", heading.Text);
        }

        [TestMethod]
        public void GivenText_WhenTryParse_ThenReturnTrueAndDocumentWithParagraphContainingText()
        {
            var input = "Test";

            var result = AstParser.TryParse(input, out var doc);
            var paragraph = doc.Blocks[0] as ParagraphNode;
            var text = paragraph?.Inlines[0] as TextNode;

            Assert.IsTrue(result);
            Assert.HasCount(1, doc.Blocks);
            Assert.IsNotNull(paragraph);
            Assert.IsNotNull(text);
            Assert.AreEqual("Test", text.Text);
        }

        [TestMethod]
        public void GivenBoldText_WhenTryParse_ThenReturnTrueAndDocumentWithParagraphContainingBoldTextInStyleSpanNode()
        {
            var input = $"{MarkupSyntax.OpenTag}{MarkupSyntax.Bold}{MarkupSyntax.CloseTag}Test{MarkupSyntax.OpenTag}{MarkupSyntax.EndTag}{MarkupSyntax.Bold}{MarkupSyntax.CloseTag}";

            var result = AstParser.TryParse(input, out var doc);
            var paragraph = doc.Blocks[0] as ParagraphNode;
            var styleSpan = paragraph?.Inlines[0] as StyleSpanNode;
            var text = styleSpan?.Inlines[0] as TextNode;

            Assert.IsTrue(result);
            Assert.HasCount(1, doc.Blocks);
            Assert.IsNotNull(paragraph);
            Assert.IsNotNull(styleSpan);
            Assert.IsTrue(styleSpan.Style.Bold);
            Assert.IsNotNull(text);
            Assert.AreEqual("Test", text.Text);
        }

        [TestMethod]
        public void GivenItalicText_WhenTryParse_ThenReturnTrueAndDocumentWithParagraphContainingItalicTextInStyleSpanNode()
        {
            var input = $"{MarkupSyntax.OpenTag}{MarkupSyntax.Italic}{MarkupSyntax.CloseTag}Test{MarkupSyntax.OpenTag}{MarkupSyntax.EndTag}{MarkupSyntax.Italic}{MarkupSyntax.CloseTag}";

            var result = AstParser.TryParse(input, out var doc);
            var paragraph = doc.Blocks[0] as ParagraphNode;
            var styleSpan = paragraph?.Inlines[0] as StyleSpanNode;
            var text = styleSpan?.Inlines[0] as TextNode;

            Assert.IsTrue(result);
            Assert.HasCount(1, doc.Blocks);
            Assert.IsNotNull(paragraph);
            Assert.IsNotNull(styleSpan);
            Assert.IsTrue(styleSpan.Style.Italic);
            Assert.IsNotNull(text);
            Assert.AreEqual("Test", text.Text);
        }

        [TestMethod]
        public void GivenBoldItalicText_WhenTryParse_ThenReturnTrueAndDocumentWithParagraphContainingBoldItalicTextInStyleSpanNode()
        {
            var input = $"{MarkupSyntax.OpenTag}{MarkupSyntax.Bold}{MarkupSyntax.CloseTag}{MarkupSyntax.OpenTag}{MarkupSyntax.Italic}{MarkupSyntax.CloseTag}Test{MarkupSyntax.OpenTag}{MarkupSyntax.EndTag}{MarkupSyntax.Italic}{MarkupSyntax.CloseTag}{MarkupSyntax.OpenTag}{MarkupSyntax.EndTag}{MarkupSyntax.Bold}{MarkupSyntax.CloseTag}";

            var result = AstParser.TryParse(input, out var doc);
            var paragraph = doc.Blocks[0] as ParagraphNode;
            var boldStyleSpan = paragraph?.Inlines[0] as StyleSpanNode;
            var italicStyleSpan = boldStyleSpan?.Inlines[0] as StyleSpanNode;
            var text = italicStyleSpan?.Inlines[0] as TextNode;

            Assert.IsTrue(result);
            Assert.HasCount(1, doc.Blocks);
            Assert.IsNotNull(paragraph);
            Assert.IsNotNull(boldStyleSpan);
            Assert.IsTrue(boldStyleSpan.Style.Bold);
            Assert.IsNotNull(italicStyleSpan);
            Assert.IsTrue(italicStyleSpan.Style.Italic);
            Assert.IsNotNull(text);
            Assert.AreEqual("Test", text.Text);
        }

        [TestMethod]
        public void GivenForeground010203_WhenTryParse_ThenReturnTrueAndDocumentWithParagraphContainingTextInStyleSpanNodeWithForeground010203()
        {
            var input = $"{MarkupSyntax.OpenTag}{MarkupSyntax.Foregound}{MarkupSyntax.Delimiter}010203{MarkupSyntax.CloseTag}Test{MarkupSyntax.OpenTag}{MarkupSyntax.EndTag}{MarkupSyntax.Foregound}{MarkupSyntax.CloseTag}";

            var result = AstParser.TryParse(input, out var doc);
            var paragraph = doc.Blocks[0] as ParagraphNode;
            var foregroundStyleSpan = paragraph?.Inlines[0] as StyleSpanNode;

            Assert.IsTrue(result);
            Assert.HasCount(1, doc.Blocks);
            Assert.IsNotNull(paragraph);
            Assert.IsNotNull(foregroundStyleSpan);
            Assert.IsNotNull(foregroundStyleSpan.Style.Foreground);
            Assert.AreEqual(1, foregroundStyleSpan.Style.Foreground.Value.R);
            Assert.AreEqual(2, foregroundStyleSpan.Style.Foreground.Value.G);
            Assert.AreEqual(3, foregroundStyleSpan.Style.Foreground.Value.B);
        }

        [TestMethod]
        public void GivenBackground010203_WhenTryParse_ThenReturnTrueAndDocumentWithParagraphContainingTextInStyleSpanNodeWithBackground010203()
        {
            var input = $"{MarkupSyntax.OpenTag}{MarkupSyntax.Background}{MarkupSyntax.Delimiter}010203{MarkupSyntax.CloseTag}Test{MarkupSyntax.OpenTag}{MarkupSyntax.EndTag}{MarkupSyntax.Background}{MarkupSyntax.CloseTag}";

            var result = AstParser.TryParse(input, out var doc);
            var paragraph = doc.Blocks[0] as ParagraphNode;
            var backgroundStyleSpan = paragraph?.Inlines[0] as StyleSpanNode;

            Assert.IsTrue(result);
            Assert.HasCount(1, doc.Blocks);
            Assert.IsNotNull(paragraph);
            Assert.IsNotNull(backgroundStyleSpan);
            Assert.IsNotNull(backgroundStyleSpan.Style.Background);
            Assert.AreEqual(1, backgroundStyleSpan.Style.Background.Value.R);
            Assert.AreEqual(2, backgroundStyleSpan.Style.Background.Value.G);
            Assert.AreEqual(3, backgroundStyleSpan.Style.Background.Value.B);
        }

        [TestMethod]
        public void GivenTextNewlineText_WhenTryParse_ThenReturnTrueAndDocumentWithParagraphContainingTwoNodes()
        {
            var input = $"Test1{MarkupSyntax.NewLine}Test2";

            var result = AstParser.TryParse(input, out var doc);
            var paragraph1 = doc.Blocks[0] as ParagraphNode;
            var paragraph2 = doc.Blocks[1] as ParagraphNode;
            var text1 = paragraph1?.Inlines[0] as TextNode;
            var text2 = paragraph2?.Inlines[0] as TextNode;

            Assert.IsTrue(result);
            Assert.HasCount(2, doc.Blocks);
            Assert.IsNotNull(paragraph1);
            Assert.IsNotNull(paragraph2);
            Assert.IsNotNull(text1);
            Assert.IsNotNull(text2);
            Assert.AreEqual("Test1", text1.Text);
            Assert.AreEqual("Test2", text2.Text);
        }
    }
}
