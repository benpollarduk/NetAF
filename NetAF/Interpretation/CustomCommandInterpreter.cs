using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using NetAF.Commands;
using NetAF.Logic;
using NetAF.Utilities;

namespace NetAF.Interpretation
{
    /// <summary>
    /// Provides an object that can be used for interpreting custom commands.
    /// </summary>
    public sealed class CustomCommandInterpreter : IInterpreter
    {
        #region StaticMethods

        /// <summary>
        /// Try and find a command.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="commands">The commands to search.</param>
        /// <param name="command">The matching command.</param>
        /// <param name="matchingInput">The input that matched.</param>
        /// <returns>True if the command could be found, else false.</returns>
        private static bool TryFindCommand(string input, CustomCommand[] commands, out CustomCommand command, out string matchingInput)
        {
            foreach (var c in commands)
            {
                if (IsMatch(input, c.Help.Command))
                {
                    command = c;
                    matchingInput = c.Help.Command;
                    return true;
                }
            }

            foreach (var c in commands)
            {
                if (IsMatch(input, c.Help.Shortcut))
                {
                    command = c;
                    matchingInput = c.Help.Shortcut;
                    return true;
                }
            }

            command = null;
            matchingInput = string.Empty;
            return false;
        }

        /// <summary>
        /// Determine if a command is a match.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="command">The command.</param>
        /// <returns>True if the input matched the command, else false.</returns>
        private static bool IsMatch(string input, string command)
        {
            // check that a match is found
            if (!input.StartsWith(command, StringComparison.InvariantCultureIgnoreCase))
                return false;

            // also check that it is an entire match or the next character is a space
            return input.Equals(command, StringComparison.InvariantCultureIgnoreCase) || (input.Length > command.Length && input[command.Length] == ' ');
        }

        #endregion

        #region Implementation of IInterpreter

        /// <summary>
        /// Get an array of all supported commands.
        /// </summary>
        public CommandHelp[] SupportedCommands { get; } = null;

        /// <summary>
        /// Interpret a string.
        /// </summary>
        /// <param name="input">The string to interpret.</param>
        /// <param name="game">The game.</param>
        /// <returns>The result of the interpretation.</returns>
        public InterpretationResult Interpret(string input, Game game)
        {
            if (string.IsNullOrEmpty(input))
                return InterpretationResult.Fail;

            input = StringUtilities.PreenInput(input);
            
            List<CustomCommand> commands = [];
            
            // compile commands
            foreach (var examinable in game.GetAllPlayerVisibleExaminables().Where(x => x.Commands != null))
                commands.AddRange(examinable.Commands.Where(x => x.IsPlayerVisible || x.InterpretIfNotPlayerVisible));

            if (commands.Count == 0)
                return InterpretationResult.Fail;

            // find command
            if (!TryFindCommand(input, [..commands], out var command, out var matchingInput))
                return InterpretationResult.Fail;

            // remove the matching part
            input = input.Remove(0, matchingInput.Length);

            // clone the command so that it is a new instance which allows args to be assigned to it
            var clonedCommand = command.Clone() as CustomCommand;
            clonedCommand.Arguments = StringUtilities.PreenInput(input).Split(' ', StringSplitOptions.RemoveEmptyEntries);
            return new InterpretationResult(true, clonedCommand);
        }

        /// <summary>
        /// Get contextual command help for a game, based on its current state.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <returns>The contextual help.</returns>
        public CommandHelp[] GetContextualCommandHelp(Game game)
        {
            List<CommandHelp> help = [];

            foreach (var examinable in game.GetAllPlayerVisibleExaminables().Where(x => x.Commands != null)) 
                help.AddRange(examinable.Commands.Where(x => x.IsPlayerVisible).Select(command => command.Help));

            return [.. help];
        }

        #endregion
    }
}
