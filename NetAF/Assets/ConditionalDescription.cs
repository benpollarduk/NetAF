namespace NetAF.Assets
{
    /// <summary>
    /// Represents a conditional description of an object.
    /// </summary>
    /// <param name="trueDescription">The description of this object when the condition returns true.</param>
    /// <param name="falseDescription">The description of this object when the condition returns false.</param>
    /// <param name="condition">The condition.</param>
    public sealed class ConditionalDescription(string trueDescription, string falseDescription, Condition condition) : IDescription
    {
        #region Implementation of IDescription

        /// <summary>
        /// Get the description.
        /// </summary>
        /// <returns>The description.</returns>
        public string GetDescription()
        {
            if (condition != null)
                return condition.Invoke() ? trueDescription : falseDescription;

            return trueDescription;
        }

        #endregion
    }
}