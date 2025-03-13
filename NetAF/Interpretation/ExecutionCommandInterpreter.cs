using NetAF.Commands;
using NetAF.Commands.Global;
using NetAF.Logic;
using NetAF.Utilities;

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
            StringUtilities.SplitTextToVerbAndNoun(input, out var verb, out var _);

            if (Exit.CommandHelp.Equals(verb))
                return new(true, new Exit());

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
            return DefaultSupportedCommands;
        }

        #endregion
    }
}
