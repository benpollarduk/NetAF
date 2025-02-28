using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Targets.Console.Rendering;

namespace NetAF.Tests.Targets.Console.Rendering
{
    [TestClass]
    public class GridVisualFrame_Tests
    {
        [TestMethod]
        public void Given10x10GridWithBorder_WhenRender_ThenStreamContainsData()
        {
            var gridPictureBuilder = new GridVisualBuilder(AnsiColor.Black, AnsiColor.White);
            gridPictureBuilder.Resize(new(10, 10));
            gridPictureBuilder.SetCell(0, 0, 'C', AnsiColor.Black, AnsiColor.Green);
            gridPictureBuilder.SetCell(1, 0, 'D', AnsiColor.Green, AnsiColor.Black);
            var frame = new GridVisualFrame(gridPictureBuilder);
            byte[] data;

            using (var stream = new MemoryStream())
            {
                using var writer = new StreamWriter(stream);
                var presenter = new TextWriterPresenter(writer);
                frame.Render(presenter);
                writer.Flush();
                data = stream.ToArray();
            }

            Assert.IsTrue(Array.Exists(data, x => x != 0));
        }
    }
}
