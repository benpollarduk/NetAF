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
        public Dictionary<AttributeSerialization, int> Values { get; set; }

        #endregion

        #region StaticMethods

        /// <summary>
        /// Create a new serialization from an AttributeManager.
        /// </summary>
        /// <param name="attributeManager">The AttributeManager to create the serialization from.</param>
        /// <returns>The serialization.</returns>
        public static AttributeManagerSerialization FromAttributeManager(AttributeManager attributeManager)
        {
            return new()
            {
                Values = attributeManager?.GetAsDictionary()?.ToDictionary(x => AttributeSerialization.FromAttribute(x.Key), x => x.Value) ?? []
            };
        }

        #endregion

        #region Implementation of IObjectSerialization<AttributeManager>

        /// <summary>
        /// Restore an instance from this serialization.
        /// </summary>
        /// <param name="attributeManager">The attribute manager to restore.</param>
        public void Restore(AttributeManager attributeManager)
        {
            attributeManager.RestoreFrom(this);
        }

        #endregion
    }
}
