using NetAF.Commands;
using NetAF.Commands.Global;
using NetAF.Extensions;
using NetAF.Logic;
using NetAF.Utilities;
using System;

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
            Map.CommandHelp,
            Exit.CommandHelp,
            New.CommandHelp,
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

            if (Exit.CommandHelp.Equals(verb))
                return new(true, new Exit());

            if (Help.CommandHelp.Equals(verb))
            {
                if (string.IsNullOrEmpty(noun))
                    return InterpretationResult.Fail;

                var commands = game.GetContextualCommands();
                var command = Array.Find(commands, x => x.Command.IsPlayerVisible && (x.Command.InsensitiveEquals(noun) || x.Shortcut.InsensitiveEquals(noun)));
                
                if (command != null)
                    return new(true, new Help(command));
            }

            if (CommandList.CommandHelp.Equals(verb))
                return new(true, new CommandList());

            if (Map.CommandHelp.Equals(verb))
                return new(true, new Map());

            if (New.CommandHelp.Equals(verb))
                return new(true, new New());

            return InterpretationResult.Fail;
        }

        /// <summary>
        /// Get contextual command help for a game, based on its current state.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <returns>The contextual help.</returns>
        public CommandHelp[] GetContextualCommandHelp(Game game)
        {
            return [];
        }

        #endregion
    }
}
