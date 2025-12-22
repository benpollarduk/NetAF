using NetAF.Logic;
using NetAF.Persistence.Json;
using NetAF.Serialization;

namespace NetAF.Commands.Persistence
{
    /// <summary>
    /// Represents the Load command.
    /// </summary>
    /// <param name="path">The path to load.</param>
    public sealed class Load(string path) : ICommand
    {
        #region StaticProperties

        /// <summary>
        /// Get the command help.
        /// </summary>
        public static CommandHelp CommandHelp { get; } = new("Load", "Load the game state from a file.", CommandCategory.Persistence, instructions: "When loading the path should be specified as an absolute path.");

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
            var result = JsonSave.FromFile(path, out var restorePoint, out var message);

            if (!result)
                return new(ReactionResult.Error, $"Failed to load: {message}");

            ((IObjectSerialization<Game>)restorePoint.Game).Restore(game);

            return new(ReactionResult.Inform, $"Loaded.");
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
