using NetAF.Logic;
using NetAF.Persistence;
using NetAF.Persistence.Json;

namespace NetAF.Commands.Persistence
{
    /// <summary>
    /// Represents the Save command.
    /// </summary>
    /// <param name="path">The path to save.</param>
    public sealed class Save(string path) : ICommand
    {
        #region StaticProperties

        /// <summary>
        /// Get the command help.
        /// </summary>
        public static CommandHelp CommandHelp { get; } = new("Save", "Save the game state to a file.", CommandCategory.Persistence, instructions: "When saving the path should be specified as an absolute path.");

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
            var fileName = System.IO.Path.GetFileName(path);
            var restorePoint = RestorePoint.Create(fileName, game);
            var result = JsonSave.ToFile(path, restorePoint, out var message);

            if (!result)
                return new(ReactionResult.Error, $"Failed to save: {message}");

            return new(ReactionResult.Inform, $"Saved.");
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
