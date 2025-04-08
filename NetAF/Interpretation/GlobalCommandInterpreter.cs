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
            Commands.Information.Log.CommandHelp,
            Map.CommandHelp,
            Help.CommandHelp,
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
            StringUtilities.SplitTextToVerbAndNoun(input, out var verb, out var noun);

            if (About.CommandHelp.Equals(verb))
                return new(true, new About());

            if (Commands.Information.Log.CommandHelp.Equals(verb))
                return new(true, new Commands.Information.Log());

            if (Help.CommandHelp.Equals(verb))
            {
                var prompts = game.GetPromptsForCommand(noun);

                if (string.IsNullOrEmpty(noun))
                    return new(true, new Help(Help.CommandHelp, prompts));

                var commands = game.GetContextualCommands();
                var command = Array.Find(commands, x => x.Command.InsensitiveEquals(noun) || x.Shortcut.InsensitiveEquals(noun));

                if (command != null)
                    return new(true, new Help(command, prompts));
                else
                    return new(true, new Unactionable($"'{noun}' is not a command."));
            }

            if (CommandList.CommandHelp.Equals(verb))
                return new(true, new CommandList());

            if (Map.CommandHelp.Equals(verb))
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
                commands.Add(Commands.Information.Log.CommandHelp);
                commands.Add(Map.CommandHelp);
                commands.Add(Help.CommandHelp);
                commands.Add(CommandList.CommandHelp);
            }

            return [.. commands];
        }

        #endregion
    }
}
