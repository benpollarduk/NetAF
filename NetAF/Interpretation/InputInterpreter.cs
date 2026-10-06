using System.Collections.Generic;
using System.Linq;
using NetAF.Commands;
using NetAF.Commands.Scene;
using NetAF.Extensions;
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
        public List<CommandHelp> ExcludedCommands { get; } = [];

        /// <inheritdoc/>
        public CommandHelp[] SupportedCommands
        {
            get
            {
                var commands = new List<CommandHelp>();

                foreach (var supportedCommands in interpreters.Select(i => i.SupportedCommands).Where(x => x != null))
                    commands.AddRange(supportedCommands);

                return [.. this.FilterExcludedCommands(commands)];
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
            List<CommandHelp> commands = [];

            foreach (var interpreter in interpreters)
            {
                var contextualCommands = interpreter.GetContextualCommandHelp(game);

                if (contextualCommands != null)
                    commands.AddRange(interpreter.GetContextualCommandHelp(game));
            }

            return [.. this.FilterExcludedCommands(commands)];
        }

        #endregion
    }
}
