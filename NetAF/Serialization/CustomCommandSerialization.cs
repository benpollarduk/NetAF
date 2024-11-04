using NetAF.Commands;

namespace NetAF.Serialization.Assets
{
    /// <summary>
    /// Represents a serialization of a CustomCommand.
    /// </summary>
    /// <param name="command">The command.</param>
    public class CustomCommandSerialization(CustomCommand command) : IObjectSerialization<CustomCommand>
    {
        #region Properties

        /// <summary>
        /// Get or set the command name.
        /// </summary>
        public string CommandName { get; set; } = command?.Help?.Command;

        /// <summary>
        /// Get or set if it is player visible.
        /// </summary>
        public bool IsPlayerVisible { get; set; } = command?.IsPlayerVisible ?? false;

        #endregion

        #region Implementation of IObjectSerialization<CustomCommand>

        /// <summary>
        /// Restore an instance from this serialization.
        /// </summary>
        /// <param name="command">The command to restore.</param>
        public virtual void Restore(CustomCommand command)
        {
            command.RestoreFrom(this);
        }

        #endregion
    }
}
