namespace NetAF.Assets
{
    /// <summary>
    /// Represents an examination.
    /// </summary>
    /// <param name="description">A description of the examination.</param>
    public sealed class Examination(string description)
    {
        #region Properties

        /// <summary>
        /// Get the description of the examination.
        /// </summary>
        public string Description { get; } = description;

        #endregion
    }
}