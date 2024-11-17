using NetAF.Assets.Characters;
using NetAF.Interpretation;

namespace NetAF.Logic.Modes
{
    /// <summary>
    /// Provides a display mode for conversation.
    /// </summary>
    public sealed class ConversationMode : IGameMode
    {
        #region Properties

        /// <summary>
        /// Get the converser.
        /// </summary>
        public IConverser Converser { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ConversationMode class.
        /// </summary>
        /// <param name="converser">The IConverser the conversation is being held with.</param>
        public ConversationMode(IConverser converser)
        {
            Converser = converser;
        }

        #endregion

        #region Implementation of IGameMode

        /// <summary>
        /// Get the interpreter.
        /// </summary>
        public IInterpreter Interpreter { get; } = Interpreters.ConversationInterpreter;

        /// <summary>
        /// Get the type of mode this provides.
        /// </summary>
        public GameModeType Type { get; } = GameModeType.Interactive;

        /// <summary>
        /// Render the current state of a game.
        /// </summary>
        /// <param name="game">The game.</param>
        public void Render(Game game)
        {
            var frame = game.Configuration.FrameBuilders.ConversationFrameBuilder.Build($"Conversation with {Converser.Identifier.Name}", Converser, Interpreter.GetContextualCommandHelp(game), game.Configuration.DisplaySize);
            game.Configuration.Adapter.RenderFrame(frame);
        }

        #endregion
    }
}
