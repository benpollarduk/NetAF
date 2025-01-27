using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Rendering.Console;
using NetAF.Rendering.Html;
using System.IO;

namespace NetAF.Tests.Rendering.Html
{
    [TestClass]
    public class HtmlFrame_Tests
    {
        [TestMethod]
        public void GivenEmptyString_WhenRender_ThenStartsWithHTMLTag()
        {
            HtmlBuilder builder = new();
            HtmlFrame frame = new(builder);
            byte[] data;

            using (var stream = new MemoryStream())
            {
                using var writer = new StreamWriter(stream);
                var presenter = new TextWriterPresenter(writer);
                frame.Render(presenter);
                writer.Flush();
                data = stream.ToArray();
            }

            var result = System.Text.Encoding.Default.GetString(data);

            Assert.IsTrue(result.StartsWith("<!DOCTYPE html>"));
        }
    }
}
