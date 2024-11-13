using NetAF.Extensions;
using System;

namespace NetAF.Commands
{
    /// <summary>
    /// Provides help for a command.
    /// </summary>
    /// <param name="command">The command.</param>
    /// <param name="description">The help.</param>
    /// <param name="shortcut">A shortcut for the command.</param>
    public sealed class CommandHelp(string command, string description, string shortcut = "") : IEquatable<CommandHelp>, IEquatable<string>
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
            return Command == other?.Command && Description == other?.Description;
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
            return Command.InsensitiveEquals(other) || Shortcut.InsensitiveEquals(other);
        }

        #endregion
    }
}
