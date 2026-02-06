using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Targets.Markup;
using NetAF.Targets.Markup.Rendering;

namespace NetAF.Tests.Targets.Markup.Rendering
{
    [TestClass]
    public class MarkupBuilder_Tests
    {
        [TestMethod]
        public void GivenBlank_WhenH1_ThenMarkupIsCorrectlyFormed()
        {
            var builder = new MarkupBuilder();

            builder.Heading("Test", HeadingLevel.H1);
            var result = builder.ToString();

            Assert.AreEqual($"{MarkupSyntax.Heading} Test", result);
        }

        [TestMethod]
        public void GivenBlank_WhenH2_ThenMarkupIsCorrectlyFormed()
        {
            var builder = new MarkupBuilder();

            builder.Heading("Test", HeadingLevel.H2);
            var result = builder.ToString();

            Assert.AreEqual($"{MarkupSyntax.Heading}{MarkupSyntax.Heading} Test", result);
        }

        [TestMethod]
        public void GivenBlank_WhenH3_ThenMarkupIsCorrectlyFormed()
        {
            var builder = new MarkupBuilder();

            builder.Heading("Test", HeadingLevel.H3);
            var result = builder.ToString();

            Assert.AreEqual($"{MarkupSyntax.Heading}{MarkupSyntax.Heading}{MarkupSyntax.Heading} Test", result);
        }

        [TestMethod]
        public void GivenBlank_WhenH4_ThenMarkupIsCorrectlyFormed()
        {
            var builder = new MarkupBuilder();

            builder.Heading("Test", HeadingLevel.H4);
            var result = builder.ToString();

            Assert.AreEqual($"{MarkupSyntax.Heading}{MarkupSyntax.Heading}{MarkupSyntax.Heading}{MarkupSyntax.Heading} Test", result);
        }

        [TestMethod]
        public void GivenTest_WhenText_ThenMarkupIsCorrectlyFormed()
        {
            var builder = new MarkupBuilder();

            builder.Text("Test");
            var result = builder.ToString();

            Assert.AreEqual("Test", result);
        }

        [TestMethod]
        public void GivenTest_WhenTextWithBold_ThenMarkupIsCorrectlyFormed()
        {
            var builder = new MarkupBuilder();

            builder.Text("Test", new TextStyle(Bold: true));
            var result = builder.ToString();

            Assert.AreEqual($"{MarkupSyntax.OpenTag}{MarkupSyntax.Bold}{MarkupSyntax.CloseTag}Test{MarkupSyntax.OpenTag}{MarkupSyntax.EndTag}{MarkupSyntax.Bold}{MarkupSyntax.CloseTag}", result);
        }

        [TestMethod]
        public void GivenTest_WhenTextWithItalic_ThenMarkupIsCorrectlyFormed()
        {
            var builder = new MarkupBuilder();

            builder.Text("Test", new TextStyle(Italic: true));
            var result = builder.ToString();

            Assert.AreEqual($"{MarkupSyntax.OpenTag}{MarkupSyntax.Italic}{MarkupSyntax.CloseTag}Test{MarkupSyntax.OpenTag}{MarkupSyntax.EndTag}{MarkupSyntax.Italic}{MarkupSyntax.CloseTag}", result);
        }

        [TestMethod]
        public void GivenTest_WhenTextWithStrikethrough_ThenMarkupIsCorrectlyFormed()
        {
            var builder = new MarkupBuilder();

            builder.Text("Test", new TextStyle(Strikethrough: true));
            var result = builder.ToString();

            Assert.AreEqual($"{MarkupSyntax.OpenTag}{MarkupSyntax.Strikethrough}{MarkupSyntax.CloseTag}Test{MarkupSyntax.OpenTag}{MarkupSyntax.EndTag}{MarkupSyntax.Strikethrough}{MarkupSyntax.CloseTag}", result);
        }

        [TestMethod]
        public void GivenTest_WhenTextWithUnderline_ThenMarkupIsCorrectlyFormed()
        {
            var builder = new MarkupBuilder();

            builder.Text("Test", new TextStyle(Underline: true));
            var result = builder.ToString();

            Assert.AreEqual($"{MarkupSyntax.OpenTag}{MarkupSyntax.Underline}{MarkupSyntax.CloseTag}Test{MarkupSyntax.OpenTag}{MarkupSyntax.EndTag}{MarkupSyntax.Underline}{MarkupSyntax.CloseTag}", result);
        }

        [TestMethod]
        public void GivenTest_WhenTextWithMonospace_ThenMarkupIsCorrectlyFormed()
        {
            var builder = new MarkupBuilder();

            builder.Text("Test", new TextStyle(Monospace: true));
            var result = builder.ToString();

            Assert.AreEqual($"{MarkupSyntax.OpenTag}{MarkupSyntax.Monospace}{MarkupSyntax.CloseTag}Test{MarkupSyntax.OpenTag}{MarkupSyntax.EndTag}{MarkupSyntax.Monospace}{MarkupSyntax.CloseTag}", result);
        }

        [TestMethod]
        public void GivenTest_WhenTextWithForegroundSet_ThenMarkupIsCorrectlyFormed()
        {
            var builder = new MarkupBuilder();

            builder.Text("Test", new TextStyle(Foreground: new Color(1, 2, 3)));
            var result = builder.ToString();

            Assert.AreEqual($"{MarkupSyntax.OpenTag}{MarkupSyntax.Foregound}{MarkupSyntax.Delimiter}#010203{MarkupSyntax.CloseTag}Test{MarkupSyntax.OpenTag}{MarkupSyntax.EndTag}{MarkupSyntax.Foregound}{MarkupSyntax.CloseTag}", result);
        }

        [TestMethod]
        public void GivenTest_WhenTextWithBackgroundSet_ThenMarkupIsCorrectlyFormed()
        {
            var builder = new MarkupBuilder();

            builder.Text("Test", new TextStyle(Background: new Color(1, 2, 3)));
            var result = builder.ToString();

            Assert.AreEqual($"{MarkupSyntax.OpenTag}{MarkupSyntax.Background}{MarkupSyntax.Delimiter}#010203{MarkupSyntax.CloseTag}Test{MarkupSyntax.OpenTag}{MarkupSyntax.EndTag}{MarkupSyntax.Background}{MarkupSyntax.CloseTag}", result);
        }

        [TestMethod]
        public void GivenBlank_WhenNewline_ThenMarkupIsCorrectlyFormed()
        {
            var builder = new MarkupBuilder();

            builder.Newline();
            var result = builder.ToString();

            Assert.AreEqual(MarkupSyntax.NewLine.ToString(), result);
        }

        [TestMethod]
        public void GivenBlank_WhenRaw_ThenNoModification()
        {
            var builder = new MarkupBuilder();

            builder.Raw("Test");
            var result = builder.ToString();

            Assert.AreEqual("Test", result);
        }

        [TestMethod]
        public void GivenSomeContent_WhenClear_ThenContentCleared()
        {
            var builder = new MarkupBuilder();

            builder.Raw("Test");
            builder.Clear();
            var result = builder.ToString();

            Assert.AreEqual(string.Empty, result);
        }
    }
}
