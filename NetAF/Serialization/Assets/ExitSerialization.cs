using NetAF.Assets.Locations;
using System.Linq;

namespace NetAF.Serialization.Assets
{
    /// <summary>
    /// Represents a serialization of an Exit.
    /// </summary>
    public sealed class ExitSerialization : ExaminableSerialization, IObjectSerialization<Exit>
    {
        #region Properties

        /// <summary>
        /// Get or set if the exit is locked.
        /// </summary>
        public bool IsLocked { get; set; }

        #endregion

        #region StaticMethods

        /// <summary>
        /// Create a new serialization from an Exit.
        /// </summary>
        /// <param name="exit">The Exit to create the serialization from.</param>
        /// <returns>The serialization.</returns>
        public static ExitSerialization FromExit(Exit exit)
        {
            return new()
            {
                Identifier = exit?.Identifier?.IdentifiableName,
                IsPlayerVisible = exit?.IsPlayerVisible ?? false,
                AttributeManager = AttributeManagerSerialization.FromAttributeManager(exit?.Attributes),
                Commands = exit?.Commands?.Select(CustomCommandSerialization.FromCustomCommand).ToArray() ?? [],
                IsLocked = exit?.IsLocked ?? false
            };
        }

        #endregion

        #region Implementation of IObjectSerialization<Exit>

        /// <summary>
        /// Restore an instance from this serialization.
        /// </summary>
        /// <param name="exit">The exit to restore.</param>
        void IObjectSerialization<Exit>.Restore(Exit exit)
        {
            ((IRestoreFromObjectSerialization<ExitSerialization>)exit).RestoreFrom(this);
        }

        #endregion
    }
}
