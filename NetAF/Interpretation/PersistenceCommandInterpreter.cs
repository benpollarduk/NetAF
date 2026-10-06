using NetAF.Commands;
using NetAF.Commands.Persistence;
using NetAF.Extensions;
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

        /// <inheritdoc/>
        public List<CommandHelp> ExcludedCommands { get; } = [];

        /// <inheritdoc/>
        public CommandHelp[] SupportedCommands => [.. this.FilterExcludedCommands(DefaultSupportedCommands)];

        /// <inheritdoc/>
        public InterpretationResult Interpret(string input, Game game)
        {
            StringUtilities.SplitInputToCommandAndArgument(input, out var commandString, out var args);

            if (this.IsCommand(Load.CommandHelp, commandString))
                return new(true, new Load(args));

            if (this.IsCommand(Save.CommandHelp, commandString))
                return new(true, new Save(args));

            return InterpretationResult.Fail;
        }

        /// <inheritdoc/>
        public CommandHelp[] GetContextualCommandHelp(Game game)
        {
            List<CommandHelp> commands = [];

            if (game.Mode is SceneMode)
            {
                commands.Add(Load.CommandHelp);
                commands.Add(Save.CommandHelp);
            }

            return [.. this.FilterExcludedCommands(commands)];
        }

        #endregion
    }
}
