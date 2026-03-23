using NetAF.Interpretation;
using NetAF.Logic.Callbacks;
using NetAF.Narratives;
using NetAF.Rendering.FrameBuilders;

namespace NetAF.Logic.Modes
{
    /// <summary>
    /// Provides a display mode for narrative.
    /// </summary>
    /// <param name="narrative">The narrative.</param>
    /// <param name="endCallback">An optional callback to invoke when the narrative ends.</param>
    public sealed class NarrativeMode(Narrative narrative, NarrativeEndCallback endCallback = null) : IGameMode
    {
        #region Fields

        private bool isComplete;

        #endregion

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
            if (isComplete)
            {
                game.NextMode();
            }
            else
            {
                narrative.Next();
                var frame = game.Configuration.FrameBuilders.GetFrameBuilder<INarrativeFrameBuilder>().Build(narrative, game.Configuration.DisplaySize);
                game.Configuration.Adapter.RenderFrame(frame);
                isComplete = narrative.IsComplete;
                
                if (isComplete)
                    endCallback?.Invoke(game);
            }
        }

        #endregion
    }
}
