using NetAF.Assets.Interaction;
using NetAF.Logic.Modes;

namespace NetAF.Commands.RegionMap
{
    /// <summary>
    /// Represents the PanReset command.
    /// </summary>
    public sealed class PanReset : ICommand
    {
        #region StaticProperties

        /// <summary>
        /// Get the command help.
        /// </summary>
        public static CommandHelp CommandHelp { get; } = new("Reset", "Reset pan", "Z");

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
                var currentPosition = game.Overworld.CurrentRegion.GetPositionOfRoom(game.Overworld.CurrentRegion.CurrentRoom);

                if (!regionMapMode.FocusPosition.Equals(currentPosition))
                {
                    regionMapMode.FocusPosition = RegionMapMode.Player;
                    return new(ReactionResult.Silent, "Reset pan.");
                }
                else
                {
                    return new(ReactionResult.Silent, "Can't reset pan as reset position is the same as the focus position.");
                }
            }

            return new(ReactionResult.Error, "Not in region map mode.");
        }

        #endregion
    }
}