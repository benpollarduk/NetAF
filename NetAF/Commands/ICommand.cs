using NetAF.Logic;

namespace NetAF.Commands
{
    /// <summary>
    /// Represents a command.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Get the help for this command.
        /// </summary>
        CommandHelp Help { get; }
        /// <summary>
        /// Invoke the command.
        /// </summary>
        /// <param name="game">The game to invoke the command on.</param>
        /// <returns>The reaction.</returns>
        Reaction Invoke(Game game);
        /// <summary>
        /// Get all prompts for this command.
        /// </summary>
        /// <param name="game">The game to get the prompts for.</param>
        /// <returns>And array of prompts.</returns>
        Prompt[] GetPrompts(Game game);
    }
}
