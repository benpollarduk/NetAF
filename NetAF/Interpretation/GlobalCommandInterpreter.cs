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

            if (About.CommandHelp.Equals(commandString))
                return new(true, new About());

            if (Notes.CommandHelp.Equals(commandString))
                return new(true, new Notes());

            if (History.CommandHelp.Equals(commandString))
                return new(true, new History());

            if (GeneralHelp.CommandHelp.Equals(commandString))
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

            if (CommandList.CommandHelp.Equals(commandString))
                return new(true, new CommandList());

            if (Map.CommandHelp.Equals(commandString))
                return new(true, new Map());

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
                commands.Add(About.CommandHelp);
                commands.Add(Notes.CommandHelp);
                commands.Add(History.CommandHelp);
                commands.Add(Map.CommandHelp);
                commands.Add(GeneralHelp.CommandHelp);
                commands.Add(CommandList.CommandHelp);
            }

            return [.. commands];
        }

        #endregion
    }
}
