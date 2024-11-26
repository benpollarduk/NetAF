using NetAF.Interpretation;
using NetAF.Rendering;

namespace NetAF.Logic.Modes
{
    /// <summary>
    /// Provides a display mode for rendering of visual frame.
    /// </summary>
    /// <param name="frame">The frame to render.</param>
    public sealed class VisualMode(IFrame frame) : IGameMode
    {
        #region Implementation of IGameMode

        /// <summary>
        /// Get the interpreter.
        /// </summary>
        public IInterpreter Interpreter { get; }

        /// <summary>
        /// Get the type of mode this provides.
        /// </summary>
        public GameModeType Type { get; } = GameModeType.Information;

        /// <summary>
        /// Render the current state of a game.
        /// </summary>
        /// <param name="game">The game.</param>
        public void Render(Game game)
        {
            game.Configuration.Adapter.RenderFrame(frame);
        }

        #endregion
    }
}
