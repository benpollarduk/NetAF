using NetAF.Commands;
using NetAF.Commands.Frame;
using NetAF.Extensions;
using NetAF.Logic;
using NetAF.Utilities;
using System.Collections.Generic;

namespace NetAF.Interpretation
{
    /// <summary>
    /// Provides an object that can be used for interpreting frame commands.
    /// </summary>
    public sealed class FrameCommandInterpreter : IInterpreter
    {
        #region StaticProperties

        /// <summary>
        /// Get an array of all supported commands.
        /// </summary>
        public static CommandHelp[] DefaultSupportedCommands { get; } =
        [
            Option.CommandHelp
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
            StringUtilities.SplitInputToCommandAndArgument(input, out var commandString, out var args);

            if (this.IsCommand(Option.CommandHelp, commandString))
                return new(true, new Option(args));

            return InterpretationResult.Fail;
        }

        /// <inheritdoc/>
        public CommandHelp[] GetContextualCommandHelp(Game game)
        {
            List<CommandHelp> commands = [];
            commands.Add(Option.CommandHelp);

            return [.. this.FilterExcludedCommands(commands)];
        }

        #endregion
    }
}
