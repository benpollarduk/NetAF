using NetAF.Assets;
using NetAF.Logic;

namespace NetAF.Rendering.FrameBuilders
{
    /// <summary>
    /// Represents any object that can build about frames.
    /// </summary>
    public interface IAboutFrameBuilder
    {
        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="game">The game.</param>
        /// <param name="size">The size of the frame.</param>
        /// <returns>The frame.</returns>
        IFrame Build(string title, Game game, Size size);
    }
}
