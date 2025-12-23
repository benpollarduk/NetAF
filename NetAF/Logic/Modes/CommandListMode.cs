using NetAF.Commands;
using NetAF.Interpretation;
using NetAF.Rendering.FrameBuilders;
using System.Linq;

namespace NetAF.Logic.Modes
{
    /// <summary>
    /// Provides a display mode for command list.
    /// </summary>
    /// <param name="commands">The commands to display.</param>
    public sealed class CommandListMode(CommandHelp[] commands) : IGameMode
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
            var orderedCommands = commands.OrderBy(x => x.Command).ToArray();
            var frame = game.Configuration.FrameBuilders.GetFrameBuilder<ICommandListFrameBuilder>().Build("Commands", string.Empty, orderedCommands, game.Configuration.DisplaySize);
            game.Configuration.Adapter.RenderFrame(frame);
        }

        #endregion
    }
}
