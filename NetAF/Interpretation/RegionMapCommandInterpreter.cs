using NetAF.Commands;
using NetAF.Commands.Global;
using NetAF.Commands.RegionMap;
using NetAF.Logic;

namespace NetAF.Interpretation
{
    /// <summary>
    /// Provides an object that can be used for interpreting region map commands.
    /// </summary>
    public sealed class RegionMapCommandInterpreter : IInterpreter
    {
        #region StaticProperties

        /// <summary>
        /// Get an array of all supported commands.
        /// </summary>
        public static CommandHelp[] DefaultSupportedCommands { get; } =
        [
            Up.CommandHelp,
            Down.CommandHelp,
            End.CommandHelp
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
            if (Up.CommandHelp.Equals(input))
                return new(true, new Up());

            if (Down.CommandHelp.Equals(input))
                return new(true, new Down());

            return InterpretationResult.Fail;
        }

        /// <summary>
        /// Get contextual command help for a game, based on its current state.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <returns>The contextual help.</returns>
        public CommandHelp[] GetContextualCommandHelp(Game game)
        {
            return [new CommandHelp(End.CommandHelp.Command, "Finish looking at the map")];
        }

        #endregion
    }
}
