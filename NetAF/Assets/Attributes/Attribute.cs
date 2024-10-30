using NetAF.Serialization;
using NetAF.Serialization.Assets;

namespace NetAF.Assets.Attributes
{
    /// <summary>
    /// Provides a description of an attribute.
    /// </summary>
    /// <param name="name">Specify the name of the attribute.</param>
    /// <param name="description">Specify the description of the attribute.</param>
    /// <param name="minimum">Specify the minimum limit of the attribute.</param>
    /// <param name="maximum">Specify the maximum limit of the attribute.</param>
    public class Attribute(string name, string description, int minimum, int maximum) : IRestoreFromObjectSerialization<AttributeSerialization>
    {
        #region Properties

        /// <summary>
        /// Get the name of the attribute.
        /// </summary>
        public string Name { get; private set; } = name;

        /// <summary>
        /// Get the description of the attribute.
        /// </summary>
        public string Description { get; private set; } = description;

        /// <summary>
        /// Get the minimum limit of the attribute.
        /// </summary>
        public int Minimum { get; private set; } = minimum;

        /// <summary>
        /// Get the maximum limit of the attribute.
        /// </summary>
        public int Maximum { get; private set; } = maximum;

        #endregion

        #region StaticMethods

        /// <summary>
        /// Create a new Attribute from a serialization.
        /// </summary>
        /// <param name="serialization">The serialization to create the Attribute from.</param>
        public static Attribute FromSerialization(AttributeSerialization serialization)
        {
            var attribute = new Attribute(string.Empty, string.Empty, 0, 0);
            attribute.RestoreFrom(serialization);
            return attribute;
        }

        #endregion

        #region Implementation of IRestoreFromObjectSerialization<AttributeSerialization>

        /// <summary>
        /// Restore this object from a serialization.
        /// </summary>
        /// <param name="serialization">The serialization to restore from.</param>
        public void RestoreFrom(AttributeSerialization serialization)
        {
            Name = serialization.Name;
            Description = serialization.Description;
            Minimum = serialization.Minimum;
            Maximum = serialization.Maximum;
        }

        #endregion
    }
}
