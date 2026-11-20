using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets;
using NetAF.Rendering;
using System.Threading;

namespace NetAF.Tests.Rendering
{
    [TestClass]
    public class FixedRateUpdatableFrame_Tests
    {
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
            var presenter = new TestPresenter();

            frame.Render(presenter);

            Assert.AreEqual(1, presenter.RenderCount);
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
            var presenter = new TestPresenter();

            frame.Updated += (s, e) => frame.Render(presenter);

            frame.Start();

            Thread.Sleep(50);

            frame.Stop();

            Assert.AreEqual(2, presenter.RenderCount);
            Assert.Contains(frame2, presenter.Renders);
            Assert.Contains(frame3, presenter.Renders);
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
            var presenter = new TestPresenter();

            frame.Updated += (s, e) => frame.Render(presenter);

            frame.Start();

            Thread.Sleep(250);

            frame.Stop();

            Assert.AreEqual(5, presenter.RenderCount);
        }
    }
}
