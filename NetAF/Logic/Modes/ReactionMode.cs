using NetAF.Interpretation;

namespace NetAF.Logic.Modes
{
    /// <summary>
    /// Provides a display mode for reaction.
    /// </summary>
    /// <param name="title">The title.</param>
    /// <param name="message">The message.</param>
    /// <param name="isError">If the message is an error..</param> 
    public sealed class ReactionMode(string title, string message, bool isError) : IGameMode
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
            var frame = game.Configuration.FrameBuilders.ReactionModeFrameBuilder.Build(title, message, isError, game.Configuration.DisplaySize);
            game.Configuration.Adapter.RenderFrame(frame);
        }

        #endregion
    }
}
