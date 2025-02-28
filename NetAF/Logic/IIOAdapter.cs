using NetAF.Rendering;

namespace NetAF.Logic
{
    /// <summary>
    /// Represents any object that provides an adapter for input.
    /// </summary>
    public interface IIOAdapter
    {
        /// <summary>
        /// Render a frame.
        /// </summary>
        /// <param name="frame">The frame to render.</param>
        void RenderFrame(IFrame frame);
        /// <summary>
        /// Wait for acknowledgment.
        /// </summary>
        /// <returns>True if the acknowledgment was received correctly, else false.</returns>
        bool WaitForAcknowledge();
        /// <summary>
        /// Wait for input.
        /// </summary>
        /// <returns>The input.</returns>
        string WaitForInput();
        /// <summary>
        /// Setup for a game.
        /// </summary>
        /// <param name="game">The game to set up for.</param>
        void Setup(Game game);
    }
}
