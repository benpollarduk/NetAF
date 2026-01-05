using NetAF.Assets;
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
        /// Setup for a game.
        /// </summary>
        /// <param name="game">The game to set up for.</param>
        void Setup(Game game);
        /// <summary>
        /// Get the display size for this adapter.
        /// </summary>
        /// <returns>The size.</returns>
        Size GetDisplaySize();
    }
}
