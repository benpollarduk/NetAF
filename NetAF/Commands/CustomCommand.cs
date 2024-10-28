using NetAF.Assets;
using NetAF.Assets.Interaction;
using NetAF.Interpretation;

namespace NetAF.Commands
{
    /// <summary>
    /// Provides a custom command.
    /// </summary>
    /// <param name="help">The help for this command.</param>
    /// <param name="isPlayerVisible">If this is visible to the player.</param>
    /// <param name="callback">The callback to invoke when this command is invoked.</param>
    public class CustomCommand(CommandHelp help, bool isPlayerVisible, CustomCommandCallback callback) : ICommand, IPlayerVisible
    {
        #region Properties

        /// <summary>
        /// Get the callback.
        /// </summary>
        private CustomCommandCallback Callback { get; } = callback;

        /// <summary>
        /// Get or set the arguments.
        /// </summary>
        public string[] Arguments { get; set; }

        /// <summary>
        /// Get the help for this command.
        /// </summary>
        public CommandHelp Help { get; } = help;

        #endregion

        #region Implementation of ICommand

        /// <summary>
        /// Invoke the command.
        /// </summary>
        /// <param name="game">The game to invoke the command on.</param>
        /// <returns>The reaction.</returns>
        public Reaction Invoke(Logic.Game game)
        {
            return Callback.Invoke(game, Arguments);
        }

        #endregion

        #region Implementation of IPlayerVisible

        /// <summary>
        /// Get or set if this is visible to the player.
        /// </summary>
        public bool IsPlayerVisible { get; set; } = isPlayerVisible;

        #endregion
    }
}
