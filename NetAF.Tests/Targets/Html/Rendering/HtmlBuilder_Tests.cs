using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Targets.Html.Rendering;

namespace NetAF.Tests.Targets.Html.Rendering
{
    [TestClass]
    public class HtmlBuilder_Tests
    {
        [TestMethod]
        public void GivenBlank_WhenH1_ThenHtmlIsCorrectlyFormed()
        {
            var builder = new HtmlBuilder();

            builder.H1("Test");
            var result = builder.ToString();

            Assert.AreEqual(@"<h1>Test</h1>", result);
        }

        [TestMethod]
        public void GivenBlank_WhenH2_ThenHtmlIsCorrectlyFormed()
        {
            var builder = new HtmlBuilder();

            builder.H2("Test");
            var result = builder.ToString();

            Assert.AreEqual(@"<h2>Test</h2>", result);
        }

        [TestMethod]
        public void GivenBlank_WhenH3_ThenHtmlIsCorrectlyFormed()
        {
            var builder = new HtmlBuilder();

            builder.H3("Test");
            var result = builder.ToString();

            Assert.AreEqual(@"<h3>Test</h3>", result);
        }

        [TestMethod]
        public void GivenBlank_WhenH4_ThenHtmlIsCorrectlyFormed()
        {
            var builder = new HtmlBuilder();

            builder.H4("Test");
            var result = builder.ToString();

            Assert.AreEqual(@"<h4>Test</h4>", result);
        }

        [TestMethod]
        public void GivenBlank_WhenP_ThenHtmlIsCorrectlyFormed()
        {
            var builder = new HtmlBuilder();

            builder.P("Test");
            var result = builder.ToString();

            Assert.AreEqual(@"<p>Test</p>", result);
        }

        [TestMethod]
        public void GivenBlank_WhenBr_ThenHtmlIsCorrectlyFormed()
        {
            var builder = new HtmlBuilder();

            builder.Br();
            var result = builder.ToString();

            Assert.AreEqual(@"<br>", result);
        }

        [TestMethod]
        public void GivenBlank_WhenRaw_ThenNoModification()
        {
            var builder = new HtmlBuilder();

            builder.Raw("Test");
            var result = builder.ToString();

            Assert.AreEqual(@"Test", result);
        }

        [TestMethod]
        public void GivenSomeContent_WhenClear_ThenContentCleared()
        {
            var builder = new HtmlBuilder();

            builder.Raw("Test");
            builder.Clear();
            var result = builder.ToString();

            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void Given3ListItems_WhenOl_Then3ItemsCreated()
        {
            var builder = new HtmlBuilder();

            builder.Ol("A", "B", "C");
            var result = builder.ToString();

            Assert.AreEqual("<ol><li>A</li><li>B</li><li>C</li></ol>", result);
        }

        [TestMethod]
        public void Given3ListItems_WhenUl_Then3ItemsCreated()
        {
            var builder = new HtmlBuilder();

            builder.Ul("A", "B", "C");
            var result = builder.ToString();

            Assert.AreEqual("<ul><li>A</li><li>B</li><li>C</li></ul>", result);
        }
    }
}
