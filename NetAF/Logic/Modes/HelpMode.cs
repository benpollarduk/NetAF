using NetAF.Commands;
using NetAF.Interpretation;
using NetAF.Rendering.FrameBuilders;

namespace NetAF.Logic.Modes
{
    /// <summary>
    /// Provides a display mode for help.
    /// </summary>
    /// <param name="command">The command to display.</param>
    /// <param name="prompts">The prompts to display for the command.</param>
    public sealed class HelpMode(CommandHelp command, Prompt[] prompts) : IGameMode
    {
        #region Implementation of IGameMode

        /// <summary>
        /// Get the interpreter.
        /// </summary>
        public IInterpreter Interpreter { get; }

        /// <summary>
        /// Get the type of mode this provides.
        /// </summary>
        public GameModeType Type { get; } = GameModeType.SingleFrameInformation;

        /// <summary>
        /// Render the current state of a game.
        /// </summary>
        /// <param name="game">The game.</param>
        public void Render(Game game)
        {
            var frame = game.Configuration.FrameBuilders.GetFrameBuilder<IHelpFrameBuilder>().Build("Help", command, prompts, game.Configuration.DisplaySize);
            game.Configuration.Adapter.RenderFrame(frame);
        }

        #endregion
    }
}
