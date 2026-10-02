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

        /// <inheritdoc/>
        public CommandHelp[] SupportedCommands { get; } = DefaultSupportedCommands;

        /// <inheritdoc/>
        public InterpretationResult Interpret(string input, Game game)
        {
            StringUtilities.SplitInputToCommandAndArgument(input, out var commandString, out var args);

            if (Load.CommandHelp.Equals(commandString))
                return new(true, new Load(args));

            if (Save.CommandHelp.Equals(commandString))
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

            return [.. commands];
        }

        #endregion
    }
}
