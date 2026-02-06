using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Targets.Console.Rendering;
using NetAF.Targets.Markup.Rendering;
using System.IO;

namespace NetAF.Tests.Targets.Markup.Rendering
{
    [TestClass]
    public class MarkupFrame_Tests
    {
        [TestMethod]
        public void GivenEmptyString_WhenRender_ThenEmpty()
        {
            MarkupBuilder builder = new();
            MarkupFrame frame = new(builder);
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

            Assert.AreEqual(string.Empty, result);
        }
    }
}
