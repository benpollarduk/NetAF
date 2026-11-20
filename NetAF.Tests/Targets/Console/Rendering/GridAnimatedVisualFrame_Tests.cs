using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets;
using NetAF.Rendering;
using NetAF.Targets.Console.Rendering;
using System;
using System.IO;
using System.Threading;

namespace NetAF.Tests.Targets.Console.Rendering
{
    [TestClass]
    public class GridAnimatedVisualFrame_Tests
    {
        [TestMethod]
        public void Given10x10GridWithBorder_WhenRender_ThenStreamContainsData()
        {
            var gridVisualBuilder = new GridVisualBuilder(AnsiColor.Black, AnsiColor.White);
            gridVisualBuilder.Resize(new(10, 10));
            gridVisualBuilder.SetCell(0, 0, 'C', AnsiColor.Black, AnsiColor.Green);
            gridVisualBuilder.SetCell(1, 0, 'D', AnsiColor.Green, AnsiColor.Black);
            var frame = new GridVisualFrame(gridVisualBuilder);
            var animatedVisualFrame = new GridAnimatedVisualFrame([frame], AnimationOptions.Single);
            byte[] data;

            using (var stream = new MemoryStream())
            {
                using var writer = new StreamWriter(stream);
                var presenter = new TextWriterPresenter(writer, new(80, 50));
                animatedVisualFrame.Render(presenter);
                writer.Flush();
                data = stream.ToArray();
            }

            Assert.IsTrue(Array.Exists(data, x => x != 0));
        }

        [TestMethod]
        public void Given1x1Grid_WhenGetCell_ThenReturnExpected()
        {
            var gridVisualBuilder = new GridVisualBuilder(AnsiColor.Black, AnsiColor.White);
            gridVisualBuilder.Resize(new(1, 1));
            gridVisualBuilder.SetCell(0, 0, 'c', AnsiColor.Black, AnsiColor.Green);
            var frame = new GridVisualFrame(gridVisualBuilder);
            var animatedVisualFrame = new GridAnimatedVisualFrame([frame], AnimationOptions.Single);

            var result = animatedVisualFrame.GetCell(0, 0);

            Assert.AreEqual('c', result.Character);
            Assert.AreEqual(AnsiColor.Black, result.Foreground);
            Assert.AreEqual(AnsiColor.Green, result.Background);
        }

        [TestMethod]
        public void Given3Frames_WhenCurrentFrame_ThenFirstFrame()
        {
            var size = new Size(10, 10);
            var gridVisualBuilder1 = new GridVisualBuilder(AnsiColor.Black, AnsiColor.White);
            gridVisualBuilder1.Resize(size);
            gridVisualBuilder1.SetCell(0, 0, 'C', AnsiColor.Black, AnsiColor.Green);
            gridVisualBuilder1.SetCell(1, 0, 'D', AnsiColor.Green, AnsiColor.Black);

            var gridVisualBuilder2 = new GridVisualBuilder(AnsiColor.Black, AnsiColor.White);
            gridVisualBuilder2.Resize(size);
            gridVisualBuilder2.SetCell(0, 0, 'E', AnsiColor.Black, AnsiColor.Green);
            gridVisualBuilder2.SetCell(1, 0, 'F', AnsiColor.Green, AnsiColor.Black);

            var gridVisualBuilder3 = new GridVisualBuilder(AnsiColor.Black, AnsiColor.White);
            gridVisualBuilder3.Resize(size);
            gridVisualBuilder3.SetCell(0, 0, 'G', AnsiColor.Black, AnsiColor.Green);
            gridVisualBuilder3.SetCell(1, 0, 'H', AnsiColor.Green, AnsiColor.Black);

            var frame1 = new GridVisualFrame(gridVisualBuilder1);
            var frame2 = new GridVisualFrame(gridVisualBuilder2);
            var frame3 = new GridVisualFrame(gridVisualBuilder3);

            var frames = new[] { frame1, frame2, frame3 };
            var frame = new GridAnimatedVisualFrame(frames, new AnimationOptions(1, 1));

            var result = frame.CurrentFrame;

            Assert.AreEqual(frame1, result);
        }

        [TestMethod]
        public void Given3Frames_WhenRender_ThenRendered()
        {
            var size = new Size(10, 10);
            var gridVisualBuilder1 = new GridVisualBuilder(AnsiColor.Black, AnsiColor.White);
            gridVisualBuilder1.Resize(size);
            gridVisualBuilder1.SetCell(0, 0, 'C', AnsiColor.Black, AnsiColor.Green);
            gridVisualBuilder1.SetCell(1, 0, 'D', AnsiColor.Green, AnsiColor.Black);

            var gridVisualBuilder2 = new GridVisualBuilder(AnsiColor.Black, AnsiColor.White);
            gridVisualBuilder2.Resize(size);
            gridVisualBuilder2.SetCell(0, 0, 'E', AnsiColor.Black, AnsiColor.Green);
            gridVisualBuilder2.SetCell(1, 0, 'F', AnsiColor.Green, AnsiColor.Black);

            var gridVisualBuilder3 = new GridVisualBuilder(AnsiColor.Black, AnsiColor.White);
            gridVisualBuilder3.Resize(size);
            gridVisualBuilder3.SetCell(0, 0, 'G', AnsiColor.Black, AnsiColor.Green);
            gridVisualBuilder3.SetCell(1, 0, 'H', AnsiColor.Green, AnsiColor.Black);

            var frame1 = new GridVisualFrame(gridVisualBuilder1);
            var frame2 = new GridVisualFrame(gridVisualBuilder2);
            var frame3 = new GridVisualFrame(gridVisualBuilder3);

            var frames = new[] { frame1, frame2, frame3 };
            var frame = new GridAnimatedVisualFrame(frames, new AnimationOptions(1, 50));
            var presenter = new TestPresenter();

            frame.Render(presenter);

            Assert.AreEqual(1, presenter.RenderCount);
        }

        [TestMethod]
        public void Given3Frames1Cycle_WhenStart_ThenRenderedFrames2And3()
        {
            var size = new Size(10, 10);
            var gridVisualBuilder1 = new GridVisualBuilder(AnsiColor.Black, AnsiColor.White);
            gridVisualBuilder1.Resize(size);
            gridVisualBuilder1.SetCell(0, 0, 'C', AnsiColor.Black, AnsiColor.Green);
            gridVisualBuilder1.SetCell(1, 0, 'D', AnsiColor.Green, AnsiColor.Black);

            var gridVisualBuilder2 = new GridVisualBuilder(AnsiColor.Black, AnsiColor.White);
            gridVisualBuilder2.Resize(size);
            gridVisualBuilder2.SetCell(0, 0, 'E', AnsiColor.Black, AnsiColor.Green);
            gridVisualBuilder2.SetCell(1, 0, 'F', AnsiColor.Green, AnsiColor.Black);

            var gridVisualBuilder3 = new GridVisualBuilder(AnsiColor.Black, AnsiColor.White);
            gridVisualBuilder3.Resize(size);
            gridVisualBuilder3.SetCell(0, 0, 'G', AnsiColor.Black, AnsiColor.Green);
            gridVisualBuilder3.SetCell(1, 0, 'H', AnsiColor.Green, AnsiColor.Black);

            var frame1 = new GridVisualFrame(gridVisualBuilder1);
            var frame2 = new GridVisualFrame(gridVisualBuilder2);
            var frame3 = new GridVisualFrame(gridVisualBuilder3);

            var frames = new[] { frame1, frame2, frame3 };
            var frame = new GridAnimatedVisualFrame(frames, new AnimationOptions(1, 50));
            var presenter = new TestPresenter();

            frame.Updated += (s, e) => frame.Render(presenter);

            frame.Start();

            Thread.Sleep(250);

            frame.Stop();

            Assert.AreEqual(2, presenter.RenderCount);
        }

        [TestMethod]
        public void Given3Frames2Cycle_WhenStart_ThenRendered5Frames()
        {
            var size = new Size(10, 10);
            var gridVisualBuilder1 = new GridVisualBuilder(AnsiColor.Black, AnsiColor.White);
            gridVisualBuilder1.Resize(size);
            gridVisualBuilder1.SetCell(0, 0, 'C', AnsiColor.Black, AnsiColor.Green);
            gridVisualBuilder1.SetCell(1, 0, 'D', AnsiColor.Green, AnsiColor.Black);

            var gridVisualBuilder2 = new GridVisualBuilder(AnsiColor.Black, AnsiColor.White);
            gridVisualBuilder2.Resize(size);
            gridVisualBuilder2.SetCell(0, 0, 'E', AnsiColor.Black, AnsiColor.Green);
            gridVisualBuilder2.SetCell(1, 0, 'F', AnsiColor.Green, AnsiColor.Black);

            var gridVisualBuilder3 = new GridVisualBuilder(AnsiColor.Black, AnsiColor.White);
            gridVisualBuilder3.Resize(size);
            gridVisualBuilder3.SetCell(0, 0, 'G', AnsiColor.Black, AnsiColor.Green);
            gridVisualBuilder3.SetCell(1, 0, 'H', AnsiColor.Green, AnsiColor.Black);

            var frame1 = new GridVisualFrame(gridVisualBuilder1);
            var frame2 = new GridVisualFrame(gridVisualBuilder2);
            var frame3 = new GridVisualFrame(gridVisualBuilder3);

            var frames = new[] { frame1, frame2, frame3 };
            var frame = new GridAnimatedVisualFrame(frames, new AnimationOptions(2, 50));
            var presenter = new TestPresenter();

            frame.Updated += (s, e) => frame.Render(presenter);

            frame.Start();

            Thread.Sleep(500);

            frame.Stop();

            Assert.AreEqual(5, presenter.RenderCount);
        }
    }
}
