using System;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Targets.Console.Rendering;

namespace NetAF.Tests.Targets.Console.Rendering
{
    [TestClass]
    public class GridTextFrame_Tests
    {
        [TestMethod]
        public void Given10x10GridWithBorder_WhenRender_ThenStreamContainsData()
        {
            var gridStringBuilder = new GridStringBuilder();
            gridStringBuilder.Resize(new(10, 10));
            gridStringBuilder.DrawBoundary(AnsiColor.Black);
            var frame = new GridTextFrame(gridStringBuilder, 0, 0, AnsiColor.Black);
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

        [TestMethod]
        public void Given10x10GridWithBorder_WhenToString_ThenStringWithDataReturned()
        {
            var gridStringBuilder = new GridStringBuilder();
            gridStringBuilder.Resize(new(10, 10));
            gridStringBuilder.DrawBoundary(AnsiColor.Black);
            var frame = new GridTextFrame(gridStringBuilder, 0, 0, AnsiColor.Black);

            var result = frame.ToString();

            Assert.IsTrue(result.Any(x => x != 0));
        }

        [TestMethod]
        public void Given1x1Grid_WhenGetCell_ThenReturnExpected()
        {
            var gridStringBuilder = new GridStringBuilder();
            gridStringBuilder.Resize(new(1, 1));
            gridStringBuilder.SetCell(0, 0, 'c', AnsiColor.Red);
            var frame = new GridTextFrame(gridStringBuilder, 0, 0, AnsiColor.Blue);

            var result = frame.GetCell(0, 0);

            Assert.AreEqual('c', result.Character);
            Assert.AreEqual(AnsiColor.Red, result.Foreground);
            Assert.AreEqual(AnsiColor.Blue, result.Background);
        }
    }
}
