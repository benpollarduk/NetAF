using NetAF.Assets.Attributes;
using System.Collections.Generic;
using System.Linq;

namespace NetAF.Serialization.Assets
{
    /// <summary>
    /// Represents a serialization of an AttributeManager.
    /// </summary>
    /// <param name="attributeManager">The attribute manager to serialize.</param>
    public class AttributeManagerSerialization(AttributeManager attributeManager) : IObjectSerialization<AttributeManager>
    {
        #region Properties

        /// <summary>
        /// Get the values.
        /// </summary>
        public readonly Dictionary<AttributeSerialization, int> Values = attributeManager?.GetAsDictionary()?.ToDictionary<AttributeSerialization, int>( x => new AttributeSerialization(x.Key), x => x.Value) ?? [];


        #endregion

        #region Implementation of IObjectSerialization<AttributeManager>

        /// <summary>
        /// Restore an instance from this serialization.
        /// </summary>
        /// <param name="attributeManager">The attribute manager to restore.</param>
        public void Restore(AttributeManager attributeManager)
        {
            var r = attributeManager?.GetAsDictionary();
            var s = r.ToDictionary<AttributeSerialization, int>(x => new AttributeSerialization(x.Key), x => x.Value) ?? [];
            attributeManager.RestoreFrom(this);
        }

        #endregion
    }
}
