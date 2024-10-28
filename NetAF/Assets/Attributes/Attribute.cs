namespace NetAF.Assets.Attributes
{
    /// <summary>
    /// Provides a description of an attribute.
    /// </summary>
    /// <param name="name">Specify the name of the attribute.</param>
    /// <param name="description">Specify the description of the attribute.</param>
    /// <param name="minimum">Specify the minimum limit of the attribute.</param>
    /// <param name="maximum">Specify the maximum limit of the attribute.</param>
    public class Attribute(string name, string description, int minimum, int maximum)
    {
        #region Properties

        /// <summary>
        /// Get the name of the attribute.
        /// </summary>
        public string Name { get; } = name;

        /// <summary>
        /// Get the description of the attribute.
        /// </summary>
        public string Description { get; } = description;

        /// <summary>
        /// Get the minimum limit of the attribute.
        /// </summary>
        public int Minimum { get; } = minimum;

        /// <summary>
        /// Get the maximum limit of the attribute.
        /// </summary>
        public int Maximum { get; } = maximum;

        #endregion
    }
}
