namespace NetAF.Assets
{
    /// <summary>
    /// Represents a description of an object.
    /// </summary>
    /// <param name="description">The description</param>
    public class Description(string description)
    {
        #region StaticProperties

        /// <summary>
        /// Get an empty description.
        /// </summary>
        public static Description Empty { get; } = new(string.Empty);

        #endregion

        #region Properties

        /// <summary>
        /// Get or set the description.
        /// </summary>
        protected string DefaultDescription { get; set; } = description;

        #endregion

        #region Methods

        /// <summary>
        /// Get the description.
        /// </summary>
        /// <returns>The description.</returns>
        public virtual string GetDescription()
        {
            return DefaultDescription;
        }

        #endregion
    }
}