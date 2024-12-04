using NetAF.Assets.Attributes;
using System.Collections.Generic;

namespace NetAF.Serialization.Assets
{
    /// <summary>
    /// Represents a serialization of a KeyValuePair where key is an Attribute and value is a int.
    /// </summary>
    public sealed class AttributeAndValueSerialization : IObjectSerialization<KeyValuePair<Attribute, int>>
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

        /// <summary>
        /// Get or set the value.
        /// </summary>
        public int Value { get; set; }

        #endregion

        #region StaticMethods

        /// <summary>
        /// Create a new serialization from a KeyValuePair where key is an Attribute and value is a int.
        /// </summary>
        /// <param name="attributeAndValue">The KeyValuePair to create the serialization from.</param>
        /// <returns>The serialization.</returns>
        public static AttributeAndValueSerialization FromAttributeAndValue(KeyValuePair<Attribute, int> attributeAndValue)
        {
            return new()
            {
                Name = attributeAndValue.Key.Name,
                Description = attributeAndValue.Key.Description,
                Minimum = attributeAndValue.Key.Minimum,
                Maximum = attributeAndValue.Key.Maximum,
                Value = attributeAndValue.Value
            };
        }

        #endregion

        #region Implementation of IObjectSerialization<KeyValuePair<Attribute, int>>

        /// <summary>
        /// Restore an instance from this serialization.
        /// </summary>
        /// <param name="attributeAndValue">The KeyValuePair to restore.</param>
        void IObjectSerialization<KeyValuePair<Attribute, int>>.Restore(KeyValuePair<Attribute, int> attributeAndValue)
        {
            // cannot restore as readonly
        }

        #endregion
    }
}
