using System.Collections.Generic;
using System.Linq;
using NetAF.Commands.Game;
using NetAF.Logic;

namespace NetAF.Interpretation
{
    /// <summary>
    /// Provides an object that can be used for interpreting game input.
    /// </summary>
    /// <param name="interpreters">The interpreters.</param>
    public sealed class InputInterpreter(params IInterpreter[] interpreters) : IInterpreter
    {
        #region Implementation of IInterpreter

        /// <summary>
        /// Get an array of all supported commands.
        /// </summary>
        public CommandHelp[] SupportedCommands
        {
            get
            {
                var l = new List<CommandHelp>();

                foreach (var commands in interpreters.Select(i => i.SupportedCommands))
                {
                    if (commands != null)
                        l.AddRange(commands);
                }

                return [.. l];
            }
        }

        /// <summary>
        /// Interpret a string.
        /// </summary>
        /// <param name="input">The string to interpret.</param>
        /// <param name="game">The game.</param>
        /// <returns>The result of the interpretation.</returns>
        public InterpretationResult Interpret(string input, Game game)
        {
            foreach (var interpreter in interpreters)
            {
                var result = interpreter.Interpret(input, game);

                if (result.WasInterpretedSuccessfully)
                    return result;
            }

            return new(false, new Unactionable($"Could not interpret {input}"));
        }

        /// <summary>
        /// Get contextual command help for a game, based on its current state.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <returns>The contextual help.</returns>
        public CommandHelp[] GetContextualCommandHelp(Game game)
        {
            List<CommandHelp> l = [];

            foreach (var interpreter in interpreters)
            {
                var contextualCommands = interpreter.GetContextualCommandHelp(game);

                if (contextualCommands != null)
                    l.AddRange(interpreter.GetContextualCommandHelp(game));
            }

            return [.. l];
        }

        #endregion
    }
}
