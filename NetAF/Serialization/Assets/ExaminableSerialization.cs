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
        /// Get the identifier.
        /// </summary>
        public readonly string Identifier = examinable.Identifier.IdentifiableName;

        /// <summary>
        /// Get if it is player visible.
        /// </summary>
        public readonly bool IsPlayerVisible = examinable.IsPlayerVisible;

        /// <summary>
        /// Get the attribute manager serializations.
        /// </summary>
        public readonly AttributeManagerSerialization AttributeManager = new(examinable.Attributes);

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
