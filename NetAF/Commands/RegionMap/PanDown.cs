using NetAF.Assets;
using NetAF.Assets.Interaction;
using NetAF.Logic.Modes;

namespace NetAF.Commands.RegionMap
{
    /// <summary>
    /// Represents the PanDown command.
    /// </summary>
    public class PanDown : ICommand
    {
        #region StaticProperties

        /// <summary>
        /// Get the command help.
        /// </summary>
        public static CommandHelp CommandHelp { get; } = new("Down", "Pan down", "D");

        #endregion

        #region StaticMethods

        /// <summary>
        /// Get the pan position.
        /// </summary>
        /// <param name="current">The current pan position.</param>
        /// <returns>The modified pan position.</returns>
        public static Point3D GetPanPosition(Point3D current)
        {
            return new Point3D(current.X, current.Y, current.Z - 1);
        }

        #endregion

        #region Implementation of ICommand

        /// <summary>
        /// Invoke the command.
        /// </summary>
        /// <param name="game">The game to invoke the command on.</param>
        /// <returns>The reaction.</returns>
        public Reaction Invoke(Logic.Game game)
        {
            if (game == null)
                return new(ReactionResult.Error, "No game specified.");

            if (game.Mode is RegionMapMode regionMapMode)
            {
                var newPosition = GetPanPosition(regionMapMode.FocusPosition);

                if (RegionMapMode.CanPanToPosition(game.Overworld.CurrentRegion, newPosition))
                {
                    regionMapMode.FocusPosition = newPosition;
                    return new(ReactionResult.Silent, "Shifted down a level.");
                }
                else
                {
                    return new(ReactionResult.Silent, "Can't pan to that position.");
                }
            }

            return new(ReactionResult.Error, "Not in region map mode.");
        }

        #endregion
    }
}