using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets;
using NetAF.Rendering;
using System.Collections.Generic;
using System.Threading;

namespace NetAF.Tests.Rendering
{
    [TestClass]
    public class FixedRateUpdatableFrame_Tests
    {
        private class DummyPresenter : IFramePresenter
        {
            public int RenderCount { get; private set; }
            public Queue<string> Renders { get; } = [];

            public Size GetPresentableSize()
            {
                return new Size(10, 10);
            }

            public void Present(string frame)
            {
                RenderCount++;
                Renders.Enqueue(frame);
            }
        }

        [TestMethod]
        public void Given3Frames_WhenCurrentFrame_ThenFirstFrame()
        {
            var frames = new[] { "Frame 1", "Frame 2", "Frame 3" };
            var size = new Size(10, 10);
            var frame = new FixedRateUpdatableFrame(frames, size, 1000, 2);

            var result = frame.CurrentFrame;

            Assert.AreEqual("Frame 1", result);
        }

        [TestMethod]
        public void Given3Frames_WhenRender_ThenRendered()
        {
            var frames = new[] { "Frame 1", "Frame 2", "Frame 3" };
            var size = new Size(10, 10);
            var frame = new FixedRateUpdatableFrame(frames, size, 1000, 2);
            var dummyPresenter = new DummyPresenter();

            frame.Render(dummyPresenter);

            Assert.AreEqual(1, dummyPresenter.RenderCount);
        }

        [TestMethod]
        public void Given3Frames1Cycle_WhenStart_ThenRenderedFrames2And3()
        {
            var frame1 = "Frame 1";
            var frame2 = "Frame 2";
            var frame3 = "Frame 3";
            var frames = new[] { frame1, frame2, frame3 };
            var size = new Size(10, 10);
            var frame = new FixedRateUpdatableFrame(frames, size, 1, 1);
            var dummyPresenter = new DummyPresenter();

            frame.Updated += (s, e) => frame.Render(dummyPresenter);

            frame.Start();

            Thread.Sleep(10);

            frame.Stop();

            Assert.AreEqual(2, dummyPresenter.RenderCount);
            Assert.Contains(frame2, dummyPresenter.Renders);
            Assert.Contains(frame3, dummyPresenter.Renders);
        }

        [TestMethod]
        public void Given3Frames2Cycle_WhenStart_ThenRendered5Frames()
        {
            var frame1 = "Frame 1";
            var frame2 = "Frame 2";
            var frame3 = "Frame 3";
            var frames = new[] { frame1, frame2, frame3 };
            var size = new Size(10, 10);
            var frame = new FixedRateUpdatableFrame(frames, size, 1, 2);
            var dummyPresenter = new DummyPresenter();

            frame.Updated += (s, e) => frame.Render(dummyPresenter);

            frame.Start();

            Thread.Sleep(10);

            frame.Stop();

            Assert.AreEqual(5, dummyPresenter.RenderCount);
        }
    }
}
