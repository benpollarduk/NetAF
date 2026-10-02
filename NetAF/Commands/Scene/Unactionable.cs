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

        /// <inheritdoc/>
        public CommandHelp Help => GeneralCommandHelp;

        /// <inheritdoc/>
        public Reaction Invoke(Game game)
        {
            return new(ReactionResult.Error, Description);
        }

        /// <inheritdoc/>
        public Prompt[] GetPrompts(Game game)
        {
            return [];
        }

        #endregion
    }
}