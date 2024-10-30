namespace NetAF.Serialization.Assets
{
    /// <summary>
    /// Represents a serialization of an Attribute.
    /// </summary>
    /// <param name="attribute">The asset to serialize.</param>
    public sealed class AttributeSerialization(NetAF.Assets.Attributes.Attribute attribute) : IObjectSerialization<NetAF.Assets.Attributes.Attribute>
    {
        #region Properties

        /// <summary>
        /// Get the name.
        /// </summary>
        public readonly string Name = attribute.Name;

        /// <summary>
        /// Get the description.
        /// </summary>
        public readonly string Description = attribute.Description;

        /// <summary>
        /// Get the minimum.
        /// </summary>
        public readonly int Minimum = attribute.Minimum;

        /// <summary>
        /// Get the maximum.
        /// </summary>
        public readonly int Maximum = attribute.Maximum;

        #endregion

        #region Implementation of IObjectSerialization<Attribute>

        /// <summary>
        /// Restore an instance from this serialization.
        /// </summary>
        /// <param name="attribute">The attribute to restore.</param>
        public void Restore(NetAF.Assets.Attributes.Attribute attribute)
        {
            //attribute.RestoreFrom(this);
        }

        #endregion
    }
}
