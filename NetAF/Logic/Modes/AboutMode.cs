using NetAF.Interpretation;
using NetAF.Rendering.FrameBuilders;

namespace NetAF.Logic.Modes
{
    /// <summary>
    /// Provides a display mode for about.
    /// </summary>
    public sealed class AboutMode : IGameMode
    {
        #region Implementation of IGameMode

        /// <inheritdoc/>
        public IInterpreter Interpreter { get; }

        /// <inheritdoc/>
        public GameModeType Type { get; } = GameModeType.SingleFrameInformation;

        /// <inheritdoc/>
        public void Render(Game game)
        {
            var frame = game.Configuration.FrameBuilders.GetFrameBuilder<IAboutFrameBuilder>().Build("About", game, game.Configuration.DisplaySize);
            game.Configuration.Adapter.RenderFrame(frame);
        }

        #endregion
    }
}
