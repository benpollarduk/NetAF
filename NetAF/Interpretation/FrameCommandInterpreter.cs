using NetAF.Commands;
using NetAF.Commands.Frame;
using NetAF.Logic;

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
            CommandsOn.CommandHelp,
            CommandsOff.CommandHelp,
            KeyOn.CommandHelp,
            KeyOff.CommandHelp
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
            if (CommandsOff.CommandHelp.Equals(input))
                return new(true, new CommandsOff());

            if (CommandsOn.CommandHelp.Equals(input))
                return new(true, new CommandsOn());

            if (KeyOff.CommandHelp.Equals(input))
                return new(true, new KeyOff());

            if (KeyOn.CommandHelp.Equals(input))
                return new(true, new KeyOn());

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
