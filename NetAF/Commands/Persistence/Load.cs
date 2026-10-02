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

        /// <inheritdoc/>
        public CommandHelp Help => CommandHelp;

        /// <inheritdoc/>
        public Reaction Invoke(Game game)
        {
            if (string.IsNullOrEmpty(name))
                return new(ReactionResult.Error, "No name provided.");

            if (!RestorePointManager.Exists(game, name))
                return new(ReactionResult.Error, $"'{name}' does not exist.");

            if (!RestorePointManager.Apply(game, name, out string message))
                return new(ReactionResult.Error, $"Failed to load '{name}'. {message}");

            return new(ReactionResult.Inform, "Loaded.");
        }

        /// <inheritdoc/>
        public Prompt[] GetPrompts(Game game)
        {
            var availableNames = RestorePointManager.GetAvailableRestorePointNames(game);

            if (availableNames.Length == 0)
                return [];

            List<Prompt> prompts = [];

            foreach (var n in availableNames)
                prompts.Add(new Prompt(n));

            return [.. prompts];
        }

        #endregion
    }
}
