using NetAF.Assets.Interaction;
using NetAF.Logic.Modes;

namespace NetAF.Commands.RegionMap
{
    /// <summary>
    /// Represents the Down command.
    /// </summary>
    public class Down : ICommand
    {
        #region StaticProperties

        /// <summary>
        /// Get the command help.
        /// </summary>
        public static CommandHelp CommandHelp { get; } = new("Down", "Shift to the previous level", "D");

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
                regionMapMode.Level--;
                return new(ReactionResult.Silent, "Shifted down a level.");
            }

            return new(ReactionResult.Error, "Not in region map mode.");
        }

        #endregion
    }
}