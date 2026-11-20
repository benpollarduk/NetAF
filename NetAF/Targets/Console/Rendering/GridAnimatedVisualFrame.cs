using NetAF.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace NetAF.Targets.Console.Rendering
{
    /// <summary>
    /// Provides a grid based frame for displaying an animated visual.
    /// </summary>
    public sealed class GridAnimatedVisualFrame : IConsoleFrame, IUpdatableFrame
    {
        #region Fields

        private int currentFrameIndex = 0;
        private Timer timer;
        private int cyclesCompleted = 0;
        private readonly List<AnsiCell[,]> backBuffers = [];
        private AnsiCell[,] backBuffer;
        private bool isUpdatingFrame = false;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the visual frames for the grid.
        /// </summary>
        public GridVisualFrame[] Frames { get; }

        /// <summary>
        /// Gets the current grid visual frame.
        /// </summary>
        public GridVisualFrame CurrentFrame => Frames[currentFrameIndex];

        /// <summary>
        /// Get the options.
        /// </summary>
        public AnimationOptions Options { get; }

        /// <summary>
        /// Gets the width.
        /// </summary>
        public int Width => Frames.Max(x => x.Width);

        /// <summary>
        /// Gets the height.
        /// </summary>
        public int Height => Frames.Max(x => x.Height);

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the GridAnimatedVisualFrame class.
        /// </summary>
        /// <param name="frames">The frames.</param>
        /// <param name="options">The animation options.</param>
        public GridAnimatedVisualFrame(GridVisualFrame[] frames, AnimationOptions options)
        {
            Frames = frames;
            Options = options;

            foreach (var frame in Frames)
                backBuffers.Add(CreateBuffer(frame));
        }

        #endregion

        #region Methods

        /// <summary>
        /// Update the frame.
        /// </summary>
        /// <param name="state">An object representing state.</param>
        private void UpdateFrame(object state)
        {
            if (isUpdatingFrame)
                return;

            isUpdatingFrame = true;

            try
            {
                var isAtEnd = currentFrameIndex == Frames.Length - 1;

                if (isAtEnd)
                {
                    cyclesCompleted++;

                    if (Options.Cycles != AnimationOptions.Forever && cyclesCompleted == Options.Cycles)
                    {
                        Stop();
                        return;
                    }

                    currentFrameIndex = 0;
                }
                else
                {
                    currentFrameIndex++;
                }

                Updated?.Invoke(this, this);
            }
            finally
            {
                isUpdatingFrame = false;
            }
        }

        #endregion

        #region Overrides of Object

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A frame string created from the current object state.</returns>
        public override string ToString()
        {
            var currentBuffer = backBuffers[currentFrameIndex];
            var renderAll = backBuffer == null || backBuffer.Length != CurrentFrame.Width * CurrentFrame.Height;
            var stringBuilder = new StringBuilder();

            if (renderAll)
            {
                // render everything
                stringBuilder.Append(CurrentFrame.ToString());
            }
            else
            {
                // render changes only

                var suppressColor = Ansi.IsColorSuppressed();

                stringBuilder.Append(Ansi.ANSI_HIDE_CURSOR);

                static bool areCellsEqual(AnsiCell a, AnsiCell b) => a.Character == b.Character && a.Foreground.Equals(b.Foreground) && a.Background.Equals(b.Background);

                for (var y = 0; y < CurrentFrame.Height; y++)
                    for (var x = 0; x < CurrentFrame.Width; x++)
                        if (!areCellsEqual(backBuffer[x, y], currentBuffer[x, y]))
                            AppendCell(stringBuilder, currentBuffer[x, y], x, y, suppressColor);
            }

            backBuffer = currentBuffer;

            return stringBuilder.ToString();
        }

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

        private static void AppendCell(StringBuilder stringBuilder, AnsiCell cell, int x, int y, bool suppressColor)
        {
            // set position
            stringBuilder.Append(Ansi.GetAnsiCursorPositionEscapeSequence(x, y));

            // set background
            stringBuilder.Append(Ansi.GetAnsiBackgroundEscapeSequence(suppressColor ? AnsiColor.Black : cell.Background));

            // set foreground
            stringBuilder.Append(Ansi.GetAnsiForegroundEscapeSequence(suppressColor ? AnsiColor.White : cell.Foreground));

            // set character
            stringBuilder.Append(cell.Character == 0 ? ' ' : cell.Character);
        }

        #endregion

        #region Implementation of IFrame

        /// <inheritdoc/>
        public void Render(IFramePresenter presenter)
        {
            presenter.Present(ToString());
        }

        #endregion

        #region Implementation of IConsoleFrame

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
            Stop();

            backBuffer = null;
            currentFrameIndex = 0;
            cyclesCompleted = 0;

            if (Frames.Length < 2)
                return;

            var frequency = (int)TimeSpan.FromSeconds(1d / Options.FramesPerSecond).TotalMilliseconds;
            timer = new Timer(UpdateFrame, null, frequency, frequency);
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