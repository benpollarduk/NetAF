using NetAF.Logic;
using NetAF.Persistence;
using System.Collections.Generic;

namespace NetAF.Commands.Persistence
{
    /// <summary>
    /// Represents the Load command.
    /// </summary>
    /// <param name="name">The name of the restore point to load.</param>
    public sealed class Load(string name) : ICommand
    {
        #region StaticProperties

        /// <summary>
        /// Get the command help.
        /// </summary>
        public static CommandHelp CommandHelp { get; } = new("Load", "Restore the state of a game.", CommandCategory.Persistence, instructions: "Provide the name of the restore point.");

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
            if (!RestorePointManager.Exists(game, name))
                return new(ReactionResult.Error, $"'{name}' does not exist.");

            if (!RestorePointManager.Apply(game, name, out string message))
                return new(ReactionResult.Error, $"Failed to load '{name}'. {message}");

            return new(ReactionResult.Inform, "Loaded.");
        }

        /// <summary>
        /// Get all prompts for this command.
        /// </summary>
        /// <param name="game">The game to get the prompts for.</param>
        /// <returns>And array of prompts.</returns>
        public Prompt[] GetPrompts(Game game)
        {
            var availableNames = RestorePointManager.GetAvailableRestorePointNames(game);

            if (availableNames.Length == 0)
                return [];

            List<Prompt> prompts = [];

            foreach (var name in availableNames)
                prompts.Add(new Prompt(name));

            return [.. prompts];
        }

        #endregion
    }
}
