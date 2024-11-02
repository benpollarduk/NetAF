using NetAF.Assets;

namespace NetAF.Serialization.Assets
{
    /// <summary>
    /// Represents a serialization of an Examinable.
    /// </summary>
    /// <param name="examinable">The examinable.</param>
    public class ExaminableSerialization(IExaminable examinable) : IObjectSerialization<IExaminable>
    {
        #region Properties

        /// <summary>
        /// Get or set the identifier.
        /// </summary>
        public string Identifier { get; set; } = examinable?.Identifier?.IdentifiableName;

        /// <summary>
        /// Get or set if it is player visible.
        /// </summary>
        public bool IsPlayerVisible { get; set; } = examinable?.IsPlayerVisible ?? false;

        /// <summary>
        /// Get or set the attribute manager serializations.
        /// </summary>
        public AttributeManagerSerialization AttributeManager { get; set; } = new(examinable?.Attributes);

        #endregion

        #region Implementation of IObjectSerialization<IExaminable>

        /// <summary>
        /// Restore an instance from this serialization.
        /// </summary>
        /// <param name="examinable">The examinable to restore.</param>
        public virtual void Restore(IExaminable examinable)
        {
            examinable.RestoreFrom(this);
        }

        #endregion
    }
}
