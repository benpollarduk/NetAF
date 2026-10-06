using NetAF.Commands;
using NetAF.Interpretation;
using System.Collections.Generic;
using System.Linq;

namespace NetAF.Extensions
{
    /// <summary>
    /// Provides extension functions for IInterpreter.
    /// </summary>
    internal static class IInterpreterExtensions
    {
        /// <summary>
        /// Filters out commands that are present in the ExcludedCommands collection from the commands collection.
        /// </summary>
        /// <param name="value">The IInterpreter instance.</param>
        /// <param name="commands">The collection of commands to filter.</param>
        /// <returns>A collection of commands that are not present in the ExcludedCommands collection.</returns>
        public static IEnumerable<CommandHelp> FilterExcludedCommands(this IInterpreter value, IEnumerable<CommandHelp> commands)
        {
            foreach (var command in commands ?? [])
            {
                if (!value.ExcludedCommands?.Any(x => x.Command.InsensitiveEquals(command.Command)) ?? true)
                    yield return command;
            }
        }
    }
}
