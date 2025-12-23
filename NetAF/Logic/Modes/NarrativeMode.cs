using NetAF.Interpretation;
using NetAF.Narratives;
using NetAF.Rendering.FrameBuilders;

namespace NetAF.Logic.Modes
{
    /// <summary>
    /// Provides a display mode for narrative.
    /// </summary>
    /// <param name="narrative">The narrative.</param>
    public sealed class NarrativeMode(Narrative narrative) : IGameMode
    {
        #region Implementation of IGameMode

        /// <summary>
        /// Get the interpreter.
        /// </summary>
        public IInterpreter Interpreter { get; }

        /// <summary>
        /// Get the type of mode this provides.
        /// </summary>
        public GameModeType Type { get; } = GameModeType.MultipleFrameInformation;

        /// <summary>
        /// Render the current state of a game.
        /// </summary>
        /// <param name="game">The game.</param>
        public void Render(Game game)
        {
            narrative.Next();
            var frame = game.Configuration.FrameBuilders.GetFrameBuilder<INarrativeFrameBuilder>().Build(narrative, game.Configuration.DisplaySize);
            game.Configuration.Adapter.RenderFrame(frame);

            if (!narrative.IsComplete)
                return;

            var interpreter = game.Configuration.InterpreterProvider.Find(typeof(SceneMode));
            game.ChangeMode(new SceneMode(interpreter));
        }

        #endregion
    }
}
