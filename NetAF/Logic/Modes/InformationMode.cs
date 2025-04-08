using NetAF.Information;
using NetAF.Interpretation;
using NetAF.Rendering.FrameBuilders;

namespace NetAF.Logic.Modes
{
    /// <summary>
    /// Provides a display mode for acquired information.
    /// </summary>
    /// <param name="informationManager">The information manager.</param>
    public sealed class InformationMode(InformationManager informationManager) : IGameMode
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
            var frame = game.Configuration.FrameBuilders.GetFrameBuilder<IInformationFrameBuilder>().Build("Information", string.Empty, informationManager?.GetAll(), game.Configuration.DisplaySize);
            game.Configuration.Adapter.RenderFrame(frame);
        }

        #endregion
    }
}
