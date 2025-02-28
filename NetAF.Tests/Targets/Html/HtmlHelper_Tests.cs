using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Targets.Console.Rendering;
using NetAF.Targets.Html.Rendering;

namespace NetAF.Tests.Targets.Html
{
    [TestClass]
    public class HtmlHelper_Tests
    {
        [TestMethod]
        public void GivenEmptyCharacters_WhenConvertGridStringBuilderToHtmlStringWithPadding_ThenReturnCorrectlyFormattedOutput()
        {
            var builder = new GridStringBuilder();
            builder.Resize(new(5, 1));

            var result = HtmlHelper.ConvertGridStringBuilderToHtmlString(builder);

            Assert.AreEqual("     <br>", result);
        }

        [TestMethod]
        public void GivenEmptyCharacters_WhenConvertGridStringBuilderToHtmlStringWithoutPadding_ThenReturnCorrectlyFormattedOutput()
        {
            var builder = new GridStringBuilder();
            builder.Resize(new(5, 1));

            var result = HtmlHelper.ConvertGridStringBuilderToHtmlString(builder, false);

            Assert.AreEqual("\0\0\0\0\0<br>", result);
        }
    }
}
