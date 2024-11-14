using NetAF.Commands.Global;
using NetAF.Commands.Scene;
using NetAF.Extensions;
using NetAF.Interpretation;

namespace NetAF.Logic.Modes
{
    /// <summary>
    /// Provides a display mode for reaction.
    /// </summary>
    /// <param name="title">The title.</param>
    /// <param name="message">The message.</param>
    public sealed class ReactionMode(string title, string message) : IGameMode
    {
        #region Properties

        /// <summary>
        /// Get or set if successful movement reactions are suppressed.
        /// </summary>
        public bool SuppressSuccessfulMovementReactions { get; set; } = true;

        /// <summary>
        /// Get or set if ended reactions are suppressed.
        /// </summary>
        public bool SuppressEndedReactions { get; set; } = true;

        /// <summary>
        /// Get if a reaction is a successful movement reaction.
        /// </summary>
        private bool IsSuccessfulMovementReaction => message.InsensitiveEquals(Move.SuccessfulMove);

        /// <summary>
        /// Get if a reaction is a ended reaction.
        /// </summary>
        private bool IsEndedReaction => message.InsensitiveEquals(End.SuccessfulEnd);

        #endregion

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
            if (SuppressSuccessfulMovementReactions && IsSuccessfulMovementReaction)
                return RenderState.Aborted;

            if (SuppressEndedReactions && IsEndedReaction)
                return RenderState.Aborted;

            if (string.IsNullOrEmpty(message))
                return RenderState.Aborted;

            var frame = game.Configuration.FrameBuilders.ReactionModeFrameBuilder.Build(title, message, game.Configuration.DisplaySize.Width, game.Configuration.DisplaySize.Height);
            game.Configuration.Adapter.RenderFrame(frame);
            return RenderState.Completed;
        }

        #endregion
    }
}
