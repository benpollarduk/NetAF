using NetAF.Assets.Attributes;
using System.Collections.Generic;
using System.Linq;

namespace NetAF.Serialization.Assets
{
    /// <summary>
    /// Represents a serialization of an AttributeManager.
    /// </summary>
    public sealed class AttributeManagerSerialization : IObjectSerialization<AttributeManager>
    {
        #region Properties

        /// <summary>
        /// Get or set the values.
        /// </summary>
        public List<AttributeAndValueSerialization> Values { get; set; }

        #endregion

        #region StaticMethods

        /// <summary>
        /// Create a new serialization from an AttributeManager.
        /// </summary>
        /// <param name="attributeManager">The AttributeManager to create the serialization from.</param>
        /// <returns>The serialization.</returns>
        public static AttributeManagerSerialization FromAttributeManager(AttributeManager attributeManager)
        {
            List<KeyValuePair<Attribute, int>> values = [];

            foreach (var pair in attributeManager?.GetAsDictionary() ?? [])
                values.Add(pair);

            return new()
            {
                Values = values.Select(AttributeAndValueSerialization.FromAttributeAndValue).ToList()
            };
        }

        #endregion

        #region Implementation of IObjectSerialization<AttributeManager>

        /// <summary>
        /// Restore an instance from this serialization.
        /// </summary>
        /// <param name="attributeManager">The attribute manager to restore.</param>
        void IObjectSerialization<AttributeManager>.Restore(AttributeManager attributeManager)
        {
            ((IRestoreFromObjectSerialization<AttributeManagerSerialization>)attributeManager).RestoreFrom(this);
        }

        #endregion
    }
}
