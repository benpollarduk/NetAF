using NetAF.Assets;
using NetAF.Assets.Locations;
using NetAF.Interpretation;

namespace NetAF.Logic.Modes
{
    /// <summary>
    /// Provides a display mode for region map.
    /// </summary>
    /// <param name="focusPosition">The position to focus on. To use the player position use RegionMapMode.Player.</param>
    public sealed class RegionMapMode(Point3D focusPosition) : IGameMode
    {
        #region StaticProperties

        /// <summary>
        /// Get the value to use to display the player level.
        /// </summary>
        public static Point3D Player => new Point3D(int.MinValue, int.MinValue, int.MinValue);

        #endregion

        #region Properties

        /// <summary>
        /// Get or set the position to focus on. To use the player position use RegionMapMode.Player.
        /// </summary>
        public Point3D FocusPosition { get; set; } = focusPosition;

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
            // if focusing on the player, find their location
            if (FocusPosition.Equals(Player))
                FocusPosition = game.Overworld.CurrentRegion.GetPositionOfRoom(game.Overworld.CurrentRegion.CurrentRoom).Position;

            var frame = game.Configuration.FrameBuilders.RegionMapFrameBuilder.Build(game.Overworld.CurrentRegion, FocusPosition, Interpreter?.GetContextualCommandHelp(game) ?? [], game.Configuration.DisplaySize);
            game.Configuration.Adapter.RenderFrame(frame);
            return RenderState.Completed;
        }

        #endregion

        #region StaticMethods

        /// <summary>
        /// Determine if a pan position is valid.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <param name="position">The position.</param>
        /// <returns>True if the pan position is valid, else false.</returns>
        public static bool CanPanToPosition(Region region, Point3D position)
        {
            var matrix = region.ToMatrix();
            var room = matrix[position.X, position.Y, position.Z];
            return room != null && room.HasBeenVisited;
        }

        #endregion
    }
}
