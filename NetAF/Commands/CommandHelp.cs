using NetAF.Extensions;
using System;
using System.Linq;

namespace NetAF.Commands
{
    /// <summary>
    /// Provides help for a command.
    /// </summary>
    /// <param name="command">The canonical command.</param>
    /// <param name="description">A description of the command.</param>
    /// <param name="category">A category for the command.</param>
    /// <param name="shortcut">A shortcut for the command.</param>
    /// <param name="instructions">A instructions on how to use the command.</param>
    /// <param name="displayAs">A string overriding how the command should be displayed.</param>
    /// <param name="synonyms">An array of synonms for this command.</param>
    public sealed class CommandHelp(string command, string description = "", CommandCategory category = CommandCategory.Uncategorized, string shortcut = "", string instructions = "", string displayAs = "", string[] synonyms = null) : IEquatable<CommandHelp>, IEquatable<string>
    {
        #region Properties

        /// <summary>
        /// Get the canonical command.
        /// </summary>
        public string Command { get; } = command;

        /// <summary>
        /// Get the description of the command.
        /// </summary>
        public string Description { get; } = description;

        /// <summary>
        /// Get the shortcut for the command.
        /// </summary>
        public string Shortcut { get; } = shortcut;

        /// <summary>
        /// Get the instructions of the command.
        /// </summary>
        public string Instructions { get; } = instructions;

        /// <summary>
        /// Get how this command should be displayed.
        /// </summary>
        public string DisplayAs { get; } = displayAs;

        /// <summary>
        /// Get a string representing the command as it should be displayed to the user.
        /// </summary>
        public string DisplayCommand => !string.IsNullOrEmpty(DisplayAs) ? DisplayAs : Command;

        /// <summary>
        /// Get the category for this command.
        /// </summary>
        public CommandCategory Category { get; } = category;

        /// <summary>
        /// Get the synonyms for this command.
        /// </summary>
        public string[] Synonyms { get; } = synonyms ?? [];

        #endregion

        #region Overrides of Object

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return Equals(obj as CommandHelp);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return StringComparer.OrdinalIgnoreCase.GetHashCode(Command ?? string.Empty);
        }

        #endregion

        #region Implementation of IEquatable<CommandHelp>

        /// <inheritdoc/>
        public bool Equals(CommandHelp other)
        {
            return Command.InsensitiveEquals(other?.Command);
        }

        #endregion

        #region Implementation of IEquatable<String>

        /// <inheritdoc/>
        public bool Equals(string other)
        {
            if (Command.InsensitiveEquals(other))
                return true;

            if (!string.IsNullOrEmpty(Shortcut) && Shortcut.InsensitiveEquals(other))
                return true;

            if (Synonyms.Any(x => x.InsensitiveEquals(other)))
                return true;

            return false;
        }

        #endregion
    }
}
