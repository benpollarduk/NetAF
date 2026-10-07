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
        /// Determines whether the specified command is excluded by the interpreter.
        /// </summary>
        /// <param name="value">The IInterpreter instance.</param>
        /// <param name="command">The command to check.</param>
        /// <returns>True if the command is excluded; otherwise, false.</returns>
        public static bool IsCommandExcluded(this IInterpreter value, CommandHelp command)
        {
            return !value.FilterExcludedCommands([command]).Any();
        }

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

        /// <summary>
        /// Determines whether the specified command can be parsed by the interpreter, taking into account excluded commands.
        /// </summary>
        /// <param name="value">The IInterpreter instance.</param>
        /// <param name="command">The command to check.</param>
        /// <param name="query">The query string to parse.</param>
        /// <param name="failIfExcluded">Indicates whether to fail if the command is excluded. As default this is set to true</param>
        /// <returns>True if the command can be parsed; otherwise, false.</returns>
        public static bool IsCommand(this IInterpreter value, CommandHelp command, string query, bool failIfExcluded = true)
        {
            if (command == null)
                return false;

            var parses = command.Equals(query);

            if (!parses)
                return false;

            if (!failIfExcluded)
                return true;

            return FilterExcludedCommands(value, [command]).Any();
        }
    }
}
