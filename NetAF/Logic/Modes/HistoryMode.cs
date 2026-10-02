using NetAF.Interpretation;
using NetAF.Logging.History;
using NetAF.Rendering.FrameBuilders;

namespace NetAF.Logic.Modes
{
    /// <summary>
    /// Provides a display mode for the in-game history.
    /// </summary>
    /// <param name="historyManager">The history manager.</param>
    public sealed class HistoryMode(HistoryManager historyManager) : IGameMode
    {
        #region Implementation of IGameMode

        /// <inheritdoc/>
        public IInterpreter Interpreter { get; }

        /// <inheritdoc/>
        public GameModeType Type { get; } = GameModeType.SingleFrameInformation;

        /// <inheritdoc/>
        public void Render(Game game)
        {
            var frame = game.Configuration.FrameBuilders.GetFrameBuilder<IHistoryFrameBuilder>().Build("History", string.Empty, historyManager?.GetAll(), game.Configuration.DisplaySize);
            game.Configuration.Adapter.RenderFrame(frame);
        }

        #endregion
    }
}
