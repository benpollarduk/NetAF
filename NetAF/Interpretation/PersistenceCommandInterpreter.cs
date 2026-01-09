using NetAF.Commands;
using NetAF.Commands.Persistence;
using NetAF.Logic;
using NetAF.Logic.Modes;
using NetAF.Utilities;
using System.Collections.Generic;

namespace NetAF.Interpretation
{
    /// <summary>
    /// Provides an object that can be used for interpreting persistence commands.
    /// </summary>
    public sealed class PersistenceCommandInterpreter : IInterpreter
    {
        #region StaticProperties

        /// <summary>
        /// Get an array of all supported commands.
        /// </summary>
        public static CommandHelp[] DefaultSupportedCommands { get; } =
        [
            Load.CommandHelp,
            Save.CommandHelp,
        ];

        #endregion

        #region Implementation of IInterpreter

        /// <summary>
        /// Get an array of all supported commands.
        /// </summary>
        public CommandHelp[] SupportedCommands { get; } = DefaultSupportedCommands;

        /// <summary>
        /// Interpret a string.
        /// </summary>
        /// <param name="input">The string to interpret.</param>
        /// <param name="game">The game.</param>
        /// <returns>The result of the interpretation.</returns>
        public InterpretationResult Interpret(string input, Game game)
        {
            StringUtilities.SplitInputToCommandAndArguments(input, out var commandString, out var args);

            if (Load.CommandHelp.Equals(commandString))
                return new(true, new Load(args));

            if (Save.CommandHelp.Equals(commandString))
                return new(true, new Save(args));

            return InterpretationResult.Fail;
        }

        /// <summary>
        /// Get contextual command help for a game, based on its current state.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <returns>The contextual help.</returns>
        public CommandHelp[] GetContextualCommandHelp(Game game)
        {
            List<CommandHelp> commands = [];

            if (game.Mode is SceneMode)
            {
                commands.Add(Load.CommandHelp);
                commands.Add(Save.CommandHelp);
            }

            return [.. commands];
        }

        #endregion
    }
}
