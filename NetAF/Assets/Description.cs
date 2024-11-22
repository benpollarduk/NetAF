namespace NetAF.Assets
{
    /// <summary>
    /// Represents a description of an object.
    /// </summary>
    /// <param name="description">The description of this object.</param>
    public sealed class Description(string description) : IDescription
    {
        #region StaticProperties

        /// <summary>
        /// Get an empty description.
        /// </summary>
        public static Description Empty { get; } = new(string.Empty);

        #endregion

        #region Implementation of IDescription

        /// <summary>
        /// Get the description.
        /// </summary>
        /// <returns>The description.</returns>
        public string GetDescription()
        {
            return description;
        }

        #endregion
    }
}