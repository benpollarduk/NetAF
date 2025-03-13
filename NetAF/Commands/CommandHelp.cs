using NetAF.Extensions;
using System;

namespace NetAF.Commands
{
    /// <summary>
    /// Provides help for a command.
    /// </summary>
    /// <param name="command">The command.</param>
    /// <param name="description">A description of the command.</param>
    /// <param name="category">A category for the command.</param>
    /// <param name="shortcut">A shortcut for the command.</param>
    /// <param name="instructions">A instructions on how to use the command.</param>
    /// <param name="displayAs">A string overriding how the command should be displayed.</param>
    public sealed class CommandHelp(string command, string description, CommandCategory category = CommandCategory.Uncategorized, string shortcut = "", string instructions = "", string displayAs = "") : IEquatable<CommandHelp>, IEquatable<string>
    {
        #region Properties

        /// <summary>
        /// Get the command.
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

        #endregion

        #region Implementation of IEquatable<CommandHelp>

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.</returns>
        public bool Equals(CommandHelp other)
        {
            return Command.InsensitiveEquals(other?.Command);
        }

        #endregion

        #region Implementation of IEquatable<String>

        /// <summary>
        /// Indicates whether the current object is equal to another object of a different type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.</returns>
        public bool Equals(string other)
        {
            return Command.InsensitiveEquals(other) || (!string.IsNullOrEmpty(Shortcut) && Shortcut.InsensitiveEquals(other));
        }

        #endregion
    }
}
