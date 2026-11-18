using NetAF.Rendering;
using NetAF.Rendering.Animation;
using System;
using System.Text;
using System.Threading;

namespace NetAF.Targets.Console.Rendering
{
    /// <summary>
    /// Provides a grid based frame for displaying an animated visual.
    /// </summary>
    /// <param name="frames">The frames.</param>
    /// <param name="options">The animation options.</param>
    public sealed class GridAnimatedVisualFrame(GridVisualFrame[] frames, AnimationOptions options) : IConsoleFrame, IUpdatableFrame
    {
        #region Fields

        private int currentFrameIndex = 0;
        private Timer timer;
        private TimerCallback timerCallback;
        private int cyclesCompleted = 0;
        private AnsiCell[,] backBuffer;

        #endregion

        #region Methods

        private void UpdateFrame(object state)
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

            if (isAtEnd && Options.Cycles != AnimationOptions.Forever && cyclesCompleted >= Options.Cycles)
                Stop();
        }

        #endregion


        #region Fields

        private GridVisualFrame currentFrame;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the visual frames for the grid.
        /// </summary>
        public GridVisualFrame[] Frames { get; } = frames;

        /// <summary>
        /// Gets the current grid visual frame.
        /// </summary>
        public GridVisualFrame CurrentFrame => currentFrame ?? Frames[0];

        /// <summary>
        /// Get the options.
        /// </summary>
        public AnimationOptions Options { get; } = options;

        #endregion

        #region StaticMethods

        private static AnsiCell[,] CreateBuffer(GridVisualFrame frame)
        {
            var buffer = new AnsiCell[frame.Width, frame.Height];

            for (var y = 0; y < frame.Height; y++)
                for (var x = 0; x < frame.Width; x++)
                    buffer[x, y] = frame.GetCell(x, y);

            return buffer;
        }

        #endregion

        #region Implementation of IFrame

        /// <inheritdoc/>
        public int CursorLeft => CurrentFrame.CursorLeft;

        /// <inheritdoc/>
        public int CursorTop => CurrentFrame.CursorTop;

        /// <inheritdoc/>
        public bool ShowCursor 
        {
            get { return CurrentFrame.ShowCursor; }
            set
            {
                foreach (var frame in Frames)
                    frame.ShowCursor = value;
            }
        }

        ///  <inheritdoc/>
        public AnsiColor InputColor => CurrentFrame.InputColor;

        /// <inheritdoc/>
        public void Render(IFramePresenter presenter)
        {
            var renderAll = backBuffer == null || backBuffer.Length != CurrentFrame.Width * CurrentFrame.Height;
            var currentBuffer = CreateBuffer(CurrentFrame);

            if (renderAll)
            {
                // render eveyerything
                CurrentFrame.Render(presenter);
            }
            else
            {
                // render changes only

                var stringBuilder = new StringBuilder();
                var suppressColor = Ansi.IsColorSuppressed();

                presenter.Present(Ansi.ANSI_HIDE_CURSOR);

                static bool areCellsEqual(AnsiCell a, AnsiCell b) => a.Character == b.Character && a.Foreground == b.Foreground && a.Background == b.Background;

                for (var y = 0; y < CurrentFrame.Height; y++)
                {
                    for (var x = 0; x < CurrentFrame.Width; x++)
                    {
                        if (!areCellsEqual(backBuffer[x ,y], currentBuffer[x, y]))
                        {
                            var cell = currentBuffer[x, y];

                            // set position
                            stringBuilder.Append(Ansi.GetAnsiCursorPositionEscapeSequence(x, y));
        
                            // set background
                            stringBuilder.Append(Ansi.GetAnsiBackgroundEscapeSequence(suppressColor ? AnsiColor.Black : cell.Background));

                            // set foreground
                            stringBuilder.Append(Ansi.GetAnsiForegroundEscapeSequence(suppressColor ? AnsiColor.White : cell.Foreground));

                            // set character
                            stringBuilder.Append(cell.Character);
                        }
                    }
                }

                presenter.Present(stringBuilder.ToString());
            }

            backBuffer = currentBuffer;
        }

        #endregion

        #region Implementation of IAnsiGridFrame

        /// <inheritdoc/>
        public AnsiCell GetCell(int x, int y)
        {
            return CurrentFrame.GetCell(x, y);
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

            var frequency = (int)TimeSpan.FromSeconds(1d / Options.FramesPerSecond).TotalMilliseconds;
            timerCallback = new TimerCallback(UpdateFrame);
            timer = new Timer(timerCallback, null, frequency, frequency);
        }

        /// <inheritdoc/>
        public void Stop()
        {
            timer?.Change(Timeout.Infinite, Timeout.Infinite);
            timer?.Dispose();
            timer = null;
        }

        #endregion
    }
}