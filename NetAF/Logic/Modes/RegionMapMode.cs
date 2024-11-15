using NetAF.Interpretation;

namespace NetAF.Logic.Modes
{
    /// <summary>
    /// Provides a display mode for region map.
    /// </summary>
    /// <param name="level">The level to display. To use the player level use RegionMapMode.PlayerLevel.</param>
    public sealed class RegionMapMode(int level) : IGameMode
    {
        #region Constants

        /// <summary>
        /// Get the value to use to display the player level.
        /// </summary>
        public const int PlayerLevel = -1;

        #endregion

        #region Properties

        /// <summary>
        /// Get or set the level to display. To use the player level use RegionMapMode.PlayerLevel.
        /// </summary>
        public int Level { get; set; } = level;

        #endregion

        #region Implementation of IGameMode

        /// <summary>
        /// Get the interpreter.
        /// </summary>
        public IInterpreter Interpreter { get; } = Interpreters.RegionMapCommandInterpreter;

        /// <summary>
        /// Get the type of mode this provides.
        /// </summary>
        public GameModeType Type { get; } = GameModeType.Interactive;

        /// <summary>
        /// Render the current state of a game.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <returns>The render state.</returns>
        public RenderState Render(Game game)
        {
            var frame = game.Configuration.FrameBuilders.RegionMapFrameBuilder.Build(game.Overworld.CurrentRegion, game.Configuration.DisplaySize);
            game.Configuration.Adapter.RenderFrame(frame);
            return RenderState.Completed;
        }

        #endregion
    }
}
