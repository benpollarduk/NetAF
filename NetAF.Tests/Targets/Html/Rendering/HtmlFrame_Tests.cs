using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Targets.Console.Rendering;
using NetAF.Targets.Html.Rendering;
using System.IO;

namespace NetAF.Tests.Targets.Html.Rendering
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
                var presenter = new TextWriterPresenter(writer, new(80, 50));
                frame.Render(presenter);
                writer.Flush();
                data = stream.ToArray();
            }

            var result = System.Text.Encoding.Default.GetString(data);

            Assert.StartsWith("<!DOCTYPE html>", result);
        }
    }
}
