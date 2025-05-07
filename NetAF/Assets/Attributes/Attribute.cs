namespace NetAF.Assets.Attributes
{
    /// <summary>
    /// Provides a description of an attribute.
    /// </summary>
    /// <param name="name">Specify the name of the attribute.</param>
    /// <param name="description">Specify the description of the attribute.</param>
    /// <param name="minimum">Specify the minimum limit of the attribute.</param>
    /// <param name="maximum">Specify the maximum limit of the attribute.</param>
    /// <param name="isPlayerVisible">Set if this is visible to the player.</param>
    public sealed class Attribute(string name, string description, int minimum, int maximum, bool isPlayerVisible) : IPlayerVisible
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

        #region Implementation of IPlayerVisible

        /// <summary>
        /// Get or set if this is visible to the player.
        /// </summary>
        public bool IsPlayerVisible { get; set; } = isPlayerVisible;

        #endregion
    }
}
