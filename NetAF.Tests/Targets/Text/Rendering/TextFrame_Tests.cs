using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Targets.Console.Rendering;
using NetAF.Targets.Text.Rendering;
using System.IO;
using System.Text;

namespace NetAF.Tests.Targets.Text.Rendering
{
    [TestClass]
    public class TextFrame_Tests
    {
        [TestMethod]
        public void GivenTest_WhenRender_ThenStartsWithTest()
        {
            StringBuilder builder = new();
            builder.Append("Test");
            TextFrame frame = new(builder);
            byte[] data;

            using (var stream = new MemoryStream())
            {
                using var writer = new StreamWriter(stream);
                var presenter = new TextWriterPresenter(writer, new(80, 50));
                frame.Render(presenter);
                writer.Flush();
                data = stream.ToArray();
            }

            var result = Encoding.Default.GetString(data);

            Assert.IsTrue(result.StartsWith("Test"));
        }
    }
}
