using NetAF.Commands;
using NetAF.Commands.Global;
using NetAF.Commands.Information;
using NetAF.Commands.Scene;
using NetAF.Extensions;
using NetAF.Logic;
using NetAF.Logic.Modes;
using NetAF.Utilities;
using System;
using System.Collections.Generic;

namespace NetAF.Interpretation
{
    /// <summary>
    /// Provides an object that can be used for interpreting global commands.
    /// </summary>
    public sealed class GlobalCommandInterpreter : IInterpreter
    {
        #region StaticProperties

        /// <summary>
        /// Get an array of all supported commands.
        /// </summary>
        public static CommandHelp[] DefaultSupportedCommands { get; } =
        [
            About.CommandHelp,
            Notes.CommandHelp,
            History.CommandHelp,
            Map.CommandHelp,
            GeneralHelp.CommandHelp,
            CommandList.CommandHelp
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

            if (this.IsCommand(About.CommandHelp, commandString))
                return new(true, new About());

            if (this.IsCommand(Notes.CommandHelp, commandString))
                return new(true, new Notes(args));

            if (this.IsCommand(History.CommandHelp, commandString))
                return new(true, new History(args));

            if (this.IsCommand(GeneralHelp.CommandHelp, commandString))
            {
                var prompts = game.GetPromptsForCommand(args);

                if (string.IsNullOrEmpty(args))
                    return new(true, new GeneralHelp(GeneralHelp.CommandHelp, prompts));

                var commands = game.GetContextualCommands();
                var command = Array.Find(commands, x => x.Command.InsensitiveEquals(args) || x.Shortcut.InsensitiveEquals(args));

                if (command != null)
                    return new(true, new GeneralHelp(command, prompts));
                else
                    return new(true, new Unactionable($"'{args}' is not a command."));
            }

            if (this.IsCommand(CommandList.CommandHelp, commandString))
                return new(true, new CommandList());

            if (this.IsCommand(Map.CommandHelp, commandString))
                return new(true, new Map());

            return InterpretationResult.Fail;
        }

        /// <inheritdoc/>
        public CommandHelp[] GetContextualCommandHelp(Game game)
        {
            List<CommandHelp> commands = [];

            if (game.Mode is SceneMode)
            {
                commands.Add(About.CommandHelp);
                commands.Add(Notes.CommandHelp);
                commands.Add(History.CommandHelp);
                commands.Add(Map.CommandHelp);
                commands.Add(GeneralHelp.CommandHelp);
                commands.Add(CommandList.CommandHelp);
            }

            return [.. this.FilterExcludedCommands(commands)];
        }

        #endregion
    }
}
