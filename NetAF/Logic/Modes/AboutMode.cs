using NetAF.Interpretation;

namespace NetAF.Logic.Modes
{
    /// <summary>
    /// Provides a display mode for about.
    /// </summary>
    public sealed class AboutMode : IGameMode
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
        /// <returns>The render state.</returns>
        public RenderState Render(Game game)
        {
            var frame = game.Configuration.FrameBuilders.AboutFrameBuilder.Build("About", game, game.Configuration.DisplaySize);
            game.Configuration.Adapter.RenderFrame(frame);
            return RenderState.Completed;
        }

        #endregion
    }
}
