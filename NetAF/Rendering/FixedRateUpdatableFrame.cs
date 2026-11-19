using NetAF.Assets;
using System;
using System.Threading;

namespace NetAF.Rendering
{
    /// <summary>
    /// Represents a frame that updates at a fixed rate.
    /// </summary>
    /// <param name="frames">The pre-built frames.</param>
    /// <param name="size">The size of the frames.</param>
    /// <param name="displayTimeInMs">The display time of each frame, in milliseconds.</param>
    /// <param name="cycles">The number of times to cycle the frames. For forever, use AnimationOptions.Forever.</param>
    public class FixedRateUpdatableFrame(string[] frames, Size size, int displayTimeInMs, int cycles = AnimationOptions.Forever) : IFrame, IUpdatableFrame
    {
        #region Fields

        private int currentFrameIndex = 0;
        private Timer timer;
        private int cyclesCompleted = 0;
        private SynchronizationContext synchronizationContext;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the frames.
        /// </summary>
        public string[] Frames { get; } = frames;

        /// <summary>
        /// Gets the current frame.
        /// </summary>
        public string CurrentFrame => Frames[currentFrameIndex];

        /// <summary>
        /// Gets the width.
        /// </summary>
        public int Width => size.Width;

        /// <summary>
        /// Gets the height.
        /// </summary>
        public int Height => size.Height;

        #endregion

        #region Methods

        private void ThreadSafeUpdateFrame(object state)
        {
            if (synchronizationContext != null && SynchronizationContext.Current != synchronizationContext)
                synchronizationContext.Post(_ => UpdateFrame(), null);
            else
                UpdateFrame();
        }

        private void UpdateFrame()
        {
            var isAtEnd = currentFrameIndex == Frames.Length - 1;

            if (isAtEnd)
            {
                cyclesCompleted++;
                currentFrameIndex = 0;
            }
            else
            {
                currentFrameIndex++;
            }

            Updated?.Invoke(this, this);

            if (isAtEnd && cycles != AnimationOptions.Forever && cyclesCompleted >= cycles)
                Stop();
        }

        #endregion

        #region  Implementation of IFrame

        /// <inheritdoc/>
        public void Render(IFramePresenter presenter)
        {
            presenter.Present(CurrentFrame);
        }

        #endregion

        #region Implementation of IUpdatableFrame

        /// <inheritdoc/>
        public event EventHandler<IFrame> Updated;

        /// <inheritdoc/>
        public void Start()
        {
            currentFrameIndex = 0;
            cyclesCompleted = 0;

            synchronizationContext = SynchronizationContext.Current;

            if (Frames.Length < 2)
                return;

            timer = new Timer(ThreadSafeUpdateFrame, null, displayTimeInMs, displayTimeInMs);
        }

        /// <inheritdoc/>
        public void Stop()
        {
            timer?.Change(Timeout.Infinite, Timeout.Infinite);
            timer?.Dispose();
            timer = null;

            synchronizationContext = null;
        }

        #endregion
    }
}
