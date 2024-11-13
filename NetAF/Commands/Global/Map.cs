using NetAF.Assets.Interaction;

namespace NetAF.Commands.Global
{
    /// <summary>
    /// Represents the Map command.
    /// </summary>
    public class Map : ICommand
    {
        #region StaticProperties

        /// <summary>
        /// Get the command help.
        /// </summary>
        public static CommandHelp CommandHelp { get; } = new("Map", "View map of the current region");

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

            game.DisplayMap();
            return new(ReactionResult.Internal, string.Empty);
        }

        #endregion
    }
}