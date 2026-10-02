using System.Collections.Generic;
using System.Linq;
using NetAF.Commands;
using NetAF.Commands.Scene;
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

        /// <inheritdoc/>
        public CommandHelp[] SupportedCommands
        {
            get
            {
                var l = new List<CommandHelp>();

                foreach (var commands in interpreters.Select(i => i.SupportedCommands).Where(x => x != null))
                    l.AddRange(commands);

                return [.. l];
            }
        }

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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
