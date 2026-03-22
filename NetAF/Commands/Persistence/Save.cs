using NetAF.Extensions;
using NetAF.Logic;
using NetAF.Persistence;
using System.Collections.Generic;
using System.Linq;

namespace NetAF.Commands.Persistence
{
    /// <summary>
    /// Represents the Save command.
    /// </summary>
    /// <param name="name">The name of the restore point to save.</param>
    public sealed class Save(string name) : ICommand
    {
        #region StaticProperties

        /// <summary>
        /// Get the command help.
        /// </summary>
        public static CommandHelp CommandHelp { get; } = new("Save", "Saves the state of a game.", CommandCategory.Persistence, instructions: "Provide the name of the restore point.");

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
            if (string.IsNullOrEmpty(name))
                return new(ReactionResult.Error, "No name provided.");

            if (!RestorePointManager.Save(game, name, out _, out string message))
                return new(ReactionResult.Error, $"Failed to save '{name}'. {message}");

            return new(ReactionResult.Inform, "Saved.");
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
                return [new Prompt(RestorePointManager.AutoFileName)];

            List<Prompt> prompts = [];

            foreach (var n in availableNames)
                prompts.Add(new Prompt(n));

            if (!prompts.Any(x => x.Entry.InsensitiveEquals(RestorePointManager.AutoFileName)))
                prompts.Add(new Prompt(RestorePointManager.AutoFileName));

            return [.. prompts];
        }

        #endregion
    }
}
