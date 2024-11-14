using NetAF.Interpretation;

namespace NetAF.Logic.Modes
{
    /// <summary>
    /// Provides a display mode for completion.
    /// </summary>
    /// <param name="title">The title.</param>
    /// <param name="message">The message.</param>
    public sealed class CompletionMode(string title, string message) : IGameMode
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
            var frame = game.Configuration.FrameBuilders.CompletionFrameBuilder.Build(title, message, game.Configuration.DisplaySize.Width, game.Configuration.DisplaySize.Height);
            game.Configuration.Adapter.RenderFrame(frame);
            return RenderState.Completed;
        }

        #endregion
    }
}
