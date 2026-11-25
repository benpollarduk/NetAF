using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Rendering;
using System;

namespace NetAF.Tests.Rendering
{
    [TestClass]
    public class UpdatableFrameManager_Tests
    {
        private class UpdateFrame : IUpdatableFrame
        {
            public bool WasStartCalled { get; private set; }
            public bool WasStopCalled { get; private set; }
            public bool WasRenderCalled { get; private set; }
            public bool WasReRenderCalled { get; private set; }
            public event EventHandler<IFrame> Updated;

            public override string ToString()
            {
                WasRenderCalled = true;
                Updated?.Invoke(this, this);
                return base.ToString();
            }

            public void ReRender(IFrame frame)
            {
                WasReRenderCalled = true;
            }

            public void Start()
            {
                WasStartCalled = true;
            }

            public void Stop()
            {
                WasStopCalled = true;
            }
        }

        [TestMethod]
        public void GivenUpdateableFrame_WhenManageFrameTransition_ThenStartCalled()
        {
            var frame = new UpdateFrame();

            UpdatableFrameManager.ManageFrameTransition(frame, frame.ReRender);

            Assert.IsTrue(frame.WasStartCalled);
        }

        [TestMethod]
        public void GivenUpdateableFrameThenNull_WhenManageFrameTransition_ThenStopCalled()
        {
            var frame = new UpdateFrame();

            UpdatableFrameManager.ManageFrameTransition(frame, frame.ReRender);
            UpdatableFrameManager.ManageFrameTransition(null, null);

            Assert.IsTrue(frame.WasStopCalled);
        }
    }
}
