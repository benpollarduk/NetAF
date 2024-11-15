using NetAF.Commands;

namespace NetAF.Extensions
{
    /// <summary>
    /// Provides extension methods for CommandHelp.
    /// </summary>
    public static class CommandHelpExtensions
    {
        #region Extensions

        /// <summary>
        /// Returns this CommandHelp formatted to display command in the format Command/Shortcut.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The formatted CommandHelp.</returns>
        public static CommandHelp FormattedToDisplayShortcut(this CommandHelp value)
        {
            return new($"{value.Command}/{value.Shortcut}", value.Description);
        }

        /// <summary>
        /// Returns this CommandHelp formatted to display command in the format Command/Shortcut __.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The formatted CommandHelp.</returns>
        public static CommandHelp FormattedToDisplayShortcutAndVariable(this CommandHelp value)
        {
            return new($"{value.Command}/{value.Shortcut} __", value.Description);
        }

        #endregion
    }
}