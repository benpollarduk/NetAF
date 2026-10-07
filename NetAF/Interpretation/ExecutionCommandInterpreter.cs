using NetAF.Commands;
using NetAF.Commands.Execution;
using NetAF.Extensions;
using NetAF.Logic;
using NetAF.Logic.Modes;
using NetAF.Utilities;
using System.Collections.Generic;

namespace NetAF.Interpretation
{
    /// <summary>
    /// Provides an object that can be used for interpreting execution commands.
    /// </summary>
    public sealed class ExecutionCommandInterpreter : IInterpreter
    {
        #region StaticProperties

        /// <summary>
        /// Get an array of all supported commands.
        /// </summary>
        public static CommandHelp[] DefaultSupportedCommands { get; } =
        [
            Exit.CommandHelp,
            New.CommandHelp,
        ];

        #endregion

        #region Implementation of IInterpreter

        /// <inheritdoc/>
        public List<CommandHelp> ExcludedCommands { get; } = [];

        /// <inheritdoc/>
        public CommandHelp[] SupportedCommands => [.. this.FilterExcludedCommands(DefaultSupportedCommands)];

        /// <inheritdoc/>
        public InterpretationResult Interpret(string input, Game game)
        {
            StringUtilities.SplitInputToCommandAndArguments(input, out var commandString, out var _);

            if (this.IsCommand(Exit.CommandHelp, commandString))
                return new(true, new Exit());

            if (this.IsCommand(New.CommandHelp, commandString))
                return new(true, new New());

            return InterpretationResult.Fail;
        }

        /// <inheritdoc/>
        public CommandHelp[] GetContextualCommandHelp(Game game)
        {
            List<CommandHelp> commands = [];

            if (game.Mode is SceneMode)
            {
                commands.Add(Exit.CommandHelp);
                commands.Add(New.CommandHelp);
            }

            return [.. this.FilterExcludedCommands(commands)];
        }

        #endregion
    }
}
