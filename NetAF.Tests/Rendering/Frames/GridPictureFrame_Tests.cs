using System;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Rendering.FrameBuilders;
using NetAF.Rendering.FrameBuilders.Console;
using NetAF.Rendering.Frames;
using NetAF.Rendering.Presenters;

namespace NetAF.Tests.Rendering.Frames
{
    [TestClass]
    public class GridPictureFrame_Tests
    {
        [TestMethod]
        public void Given10x10GridWithBorder_WhenRender_ThenStreamContainsData()
        {
            var gridPictureBuilder = new GridPictureBuilder();
            gridPictureBuilder.Resize(new(10, 10));
            gridPictureBuilder.SetCell(0, 0, 'C', AnsiColor.Black, AnsiColor.Green);
            gridPictureBuilder.SetCell(1, 0, 'D', AnsiColor.Green, AnsiColor.Black);
            var frame = new GridPictureFrame(gridPictureBuilder);
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
