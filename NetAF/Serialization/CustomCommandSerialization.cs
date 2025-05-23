﻿using NetAF.Commands;
using System.Linq;

namespace NetAF.Serialization
{
    /// <summary>
    /// Represents a serialization of a CustomCommand.
    /// </summary>
    public class CustomCommandSerialization : IObjectSerialization<CustomCommand>
    {
        #region Properties

        /// <summary>
        /// Get or set the command name.
        /// </summary>
        public string CommandName { get; set; }

        /// <summary>
        /// Get or set if it is player visible.
        /// </summary>
        public bool IsPlayerVisible { get; set; }

        /// <summary>
        /// Get or set the prompts.
        /// </summary>
        public string[] Prompts { get; set; } = [];

        #endregion

        #region StaticMethods

        /// <summary>
        /// Create a new serialization from a CustomCommand.
        /// </summary>
        /// <param name="customCommand">The CustomCommand to create the serialization from.</param>
        /// <returns>The serialization.</returns>
        public static CustomCommandSerialization FromCustomCommand(CustomCommand customCommand)
        {
            return new()
            {
                CommandName = customCommand?.Help?.Command,
                IsPlayerVisible = customCommand?.IsPlayerVisible ?? false,
                Prompts = customCommand?.GetPrompts(null).Select(x => x.Entry).ToArray() ?? []
            };
        }

        #endregion

        #region Implementation of IObjectSerialization<CustomCommand>

        /// <summary>
        /// Restore an instance from this serialization.
        /// </summary>
        /// <param name="command">The command to restore.</param>
        void IObjectSerialization<CustomCommand>.Restore(CustomCommand command)
        {
            ((IRestoreFromObjectSerialization<CustomCommandSerialization>)command).RestoreFrom(this);
        }

        #endregion
    }
}
