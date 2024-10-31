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
        /// Get or set the name.
        /// </summary>
        public string Name { get; set; } = attribute.Name;

        /// <summary>
        /// Get or set the description.
        /// </summary>
        public string Description { get; set; } = attribute.Description;

        /// <summary>
        /// Get or set the minimum.
        /// </summary>
        public int Minimum { get; set; } = attribute.Minimum;

        /// <summary>
        /// Get or set the maximum.
        /// </summary>
        public int Maximum { get; set; } = attribute.Maximum;

        #endregion

        #region Implementation of IObjectSerialization<Attribute>

        /// <summary>
        /// Restore an instance from this serialization.
        /// </summary>
        /// <param name="attribute">The attribute to restore.</param>
        public void Restore(NetAF.Assets.Attributes.Attribute attribute)
        {
            attribute.RestoreFrom(this);
        }

        #endregion
    }
}
