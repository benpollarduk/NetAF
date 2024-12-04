using System;
using System.Collections.Generic;
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
        /// Score commands against input to find closest match.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="commands">The commands to score.</param>
        /// <returns>A dictionary containing the commands and their scores. The higher the score, the closer the match.</returns>
        private static Dictionary<CustomCommand, int> ScoreCommandsAgainstInput(string input, CustomCommand[] commands)
        {
            Dictionary<CustomCommand, int> scores = [];
            var upperCaseInput = input.ToUpper();

            foreach (var command in commands) 
            {
                var upperCaseCommand = $"{command.Help.Command.ToUpper()} ";
                var upperCaseShortcut = $"{command.Help.Shortcut.ToUpper()} ";
                int score = 0;

                for (var i = 0; i < upperCaseInput.Length; i++)
                {
                    score = i;

                    if (upperCaseCommand.Length < i + 1 && upperCaseShortcut.Length < i + 1)
                        break;

                    if (upperCaseCommand[i] != upperCaseInput[i] && upperCaseShortcut[i] != upperCaseInput[i])
                        break;
                }

                scores.Add(command, score);
            }

            return scores;
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

            // score all and find highest scoring command
            var scores = ScoreCommandsAgainstInput(input, [.. commands]);
            var match = scores.OrderByDescending(x => x.Value).First();

            // no matches
            if (match.Value == 0)
                return InterpretationResult.Fail;

            // remove either input or shortcut
            if (input.StartsWith(match.Key.Help.Command, StringComparison.InvariantCultureIgnoreCase))
                input = input.Remove(0, match.Key.Help.Command.Length);
            else if (input.StartsWith(match.Key.Help.Shortcut, StringComparison.InvariantCultureIgnoreCase))
                input = input.Remove(0, match.Key.Help.Shortcut.Length);

            var command = match.Key.Clone() as CustomCommand;
            command.Arguments = StringUtilities.PreenInput(input).Split(' ', StringSplitOptions.RemoveEmptyEntries);
            return new InterpretationResult(true, command);
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
