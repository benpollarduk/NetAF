using NetAF.Logic;
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
        public static CommandHelp CommandHelp { get; } = new("Reset", "Reset pan", CommandCategory.RegionMap, "Z", displayAs: "Reset/Z");

        #endregion

        #region Implementation of ICommand

        /// <summary>
        /// Get the help for this command.
        /// </summary>
        public CommandHelp Help => CommandHelp;

        /// <summary>
        /// Invoke the command.
        /// </summary>
        /// <param name="game">The game to invoke the command on.</param>
        /// <returns>The reaction.</returns>
        public Reaction Invoke(Game game)
        {
            if (game == null)
                return new(ReactionResult.Error, "No game specified.");

            if (game.Mode is RegionMapMode regionMapMode)
            {
                var currentPosition = game.Overworld.CurrentRegion.GetPositionOfRoom(game.Overworld.CurrentRegion.CurrentRoom);

                if (!regionMapMode.FocusPosition.Equals(currentPosition.Position))
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

        /// <summary>
        /// Get all prompts for this command.
        /// </summary>
        /// <param name="game">The game to get the prompts for.</param>
        /// <returns>And array of prompts.</returns>
        public Prompt[] GetPrompts(Game game)
        {
            return [];
        }

        #endregion
    }
}