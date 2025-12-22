using NetAF.Logic;

namespace NetAF.Commands.Scene
{
    /// <summary>
    /// Represents the Unactionable command.
    /// </summary>
    public sealed class Unactionable : ICommand
    {
        #region StaticProperties

        /// <summary>
        /// Get the general command help.
        /// </summary>
        private static CommandHelp GeneralCommandHelp { get; } = new(string.Empty, "Unactionable", CommandCategory.Uncategorized);

        #endregion

        #region Properties

        /// <summary>
        /// Get the description.
        /// </summary>
        public string Description { get; } = "Could not react.";

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Unactionable class.
        /// </summary>
        public Unactionable()
        {
        }

        /// <summary>
        /// Initializes a new instance of the Unactionable class.
        /// </summary>
        /// <param name="description">The description.</param>
        public Unactionable(string description)
        {
            Description = description;
        }

        #endregion

        #region Implementation of ICommand

        /// <summary>
        /// Get the help for this command.
        /// </summary>
        public CommandHelp Help => GeneralCommandHelp;

        /// <summary>
        /// Invoke the command.
        /// </summary>
        /// <param name="game">The game to invoke the command on.</param>
        /// <returns>The reaction.</returns>
        public Reaction Invoke(Game game)
        {
            return new(ReactionResult.Error, Description);
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