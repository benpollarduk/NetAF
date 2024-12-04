using NetAF.Assets;
using System.Linq;

namespace NetAF.Serialization.Assets
{
    /// <summary>
    /// Represents a serialization of an Examinable.
    /// </summary>
    public class ExaminableSerialization : IObjectSerialization<IExaminable>
    {
        #region Properties

        /// <summary>
        /// Get or set the identifier.
        /// </summary>
        public string Identifier { get; set; }

        /// <summary>
        /// Get or set if it is player visible.
        /// </summary>
        public bool IsPlayerVisible { get; set; }

        /// <summary>
        /// Get or set the attribute manager serializations.
        /// </summary>
        public AttributeManagerSerialization AttributeManager { get; set; }

        /// <summary>
        /// Get or set the command serializations.
        /// </summary>
        public CustomCommandSerialization[] Commands { get; set; }

        #endregion

        #region StaticMethods

        /// <summary>
        /// Create a new serialization from an IExaminable.
        /// </summary>
        /// <param name="examinable">The IExaminable to create the serialization from.</param>
        /// <returns>The serialization.</returns>
        public static ExaminableSerialization FromIExaminable(IExaminable examinable)
        {
            return new()
            {
                Identifier = examinable?.Identifier?.IdentifiableName,
                IsPlayerVisible = examinable?.IsPlayerVisible ?? false,
                AttributeManager = AttributeManagerSerialization.FromAttributeManager(examinable?.Attributes),
                Commands = examinable?.Commands?.Select(CustomCommandSerialization.FromCustomCommand).ToArray() ?? []
            };
        }

        #endregion

        #region Implementation of IObjectSerialization<IExaminable>

        /// <summary>
        /// Restore an instance from this serialization.
        /// </summary>
        /// <param name="examinable">The examinable to restore.</param>
        void IObjectSerialization<IExaminable>.Restore(IExaminable examinable)
        {
            ((IRestoreFromObjectSerialization<ExaminableSerialization>)examinable).RestoreFrom(this);
        }

        #endregion
    }
}
