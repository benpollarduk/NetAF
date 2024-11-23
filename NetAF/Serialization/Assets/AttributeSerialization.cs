using NetAF.Assets.Attributes;

namespace NetAF.Serialization.Assets
{
    /// <summary>
    /// Represents a serialization of an Attribute.
    /// </summary>
    public sealed class AttributeSerialization : IObjectSerialization<Attribute>
    {
        #region Properties

        /// <summary>
        /// Get or set the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Get or set the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Get or set the minimum.
        /// </summary>
        public int Minimum { get; set; }

        /// <summary>
        /// Get or set the maximum.
        /// </summary>
        public int Maximum { get; set; }

        #endregion

        #region StaticMethods

        /// <summary>
        /// Create a new serialization from an Attribute.
        /// </summary>
        /// <param name="attribute">The Attribute to create the serialization from.</param>
        /// <returns>The serialization.</returns>
        public static AttributeSerialization FromAttribute(Attribute attribute)
        {
            return new()
            {
                Name = attribute.Name,
                Description = attribute.Description,
                Minimum = attribute.Minimum,
                Maximum = attribute.Maximum
            };
        }

        #endregion

        #region Implementation of IObjectSerialization<Attribute>

        /// <summary>
        /// Restore an instance from this serialization.
        /// </summary>
        /// <param name="attribute">The attribute to restore.</param>
        public void Restore(Attribute attribute)
        {
            attribute.RestoreFrom(this);
        }

        #endregion
    }
}
