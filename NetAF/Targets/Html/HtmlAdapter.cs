using NetAF.Logic;
using NetAF.Rendering;
using System.Threading;

namespace NetAF.Targets.Html
{
    /// <summary>
    /// Provides an adapter for HTML.
    /// </summary>
    /// <param name="presenter">The presenter to use for presenting frames.</param>
    public sealed class HtmlAdapter(IFramePresenter presenter) : IIOAdapter
    {
        #region Fields

        private readonly ManualResetEvent gate = new(false);
        private string lastReceivedInput = string.Empty;

        #endregion

        #region Properties

        /// <summary>
        /// Get the game.
        /// </summary>
        public Game Game { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// Register that and acknowledge was received.
        /// </summary>
        public void AcknowledgeReceived()
        {
            gate.Set();
        }

        /// <summary>
        /// Register that input was received.
        /// </summary>
        /// <param name="input">The received input.</param>
        public void InputReceived(string input)
        {
            lastReceivedInput = input;
            gate.Set();
        }

        /// <summary>
        /// Clear the input.
        /// </summary>
        private void ClearInput()
        {
            lastReceivedInput = string.Empty;
        }

        /// <summary>
        /// Read and clear the input.
        /// </summary>
        /// <returns>The input.</returns>
        private string ReadAndClearInput()
        {
            var input = lastReceivedInput;
            ClearInput();
            return input;
        }

        /// <summary>
        /// Wait for acknowledgment.
        /// </summary>
        /// <param name="timeout">The timeout, in milliseconds.</param>
        /// <returns>True if the acknowledgment was received correctly, else false.</returns>
        public bool WaitForAcknowledge(int timeout)
        {
            gate.Reset();
            return gate.WaitOne(timeout);
        }

        #endregion

        #region Implementation of IIOAdapter

        /// <summary>
        /// Setup for a game.
        /// </summary>
        /// <param name="game">The game to set up for.</param>
        public void Setup(Game game)
        {
            Game = game;
            ClearInput();
        }

        /// <summary>
        /// Render a frame.
        /// </summary>
        /// <param name="frame">The frame to render.</param>
        public void RenderFrame(IFrame frame)
        {
            frame.Render(presenter);
        }

        /// <summary>
        /// Wait for acknowledgment.
        /// </summary>
        /// <returns>True if the acknowledgment was received correctly, else false.</returns>
        public bool WaitForAcknowledge()
        {
            return WaitForAcknowledge(Timeout.Infinite);
        }

        /// <summary>
        /// Wait for input.
        /// </summary>
        /// <returns>The input.</returns>
        public string WaitForInput()
        {
            gate.Reset();
            ClearInput();
            gate.WaitOne(Timeout.Infinite);
            return ReadAndClearInput();
        }

        #endregion
    }
}
