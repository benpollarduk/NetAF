using NetAF.Interpretation;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;

namespace NetAF.Logic.Modes
{
    /// <summary>
    /// Provides a mode for displaying an animation.
    /// </summary>
    /// <param name="animation">The animation.</param>
    public sealed class AnimationMode(Animation animation) : IGameMode
    {
        #region Implementation of IGameMode

        /// <inheritdoc/>
        public IInterpreter Interpreter { get; }

        /// <inheritdoc/>
        public GameModeType Type { get; } = GameModeType.SingleFrameInformation;

        /// <inheritdoc/>
        public void Render(Game game)
        {
            var frame = game.Configuration.FrameBuilders.GetFrameBuilder<IAnimationFrameBuilder>().Build(animation, game.Configuration.DisplaySize);
            game.Configuration.Adapter.RenderFrame(frame);
        }

        #endregion
    }
}
