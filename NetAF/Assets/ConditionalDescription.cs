namespace NetAF.Assets
{
    /// <summary>
    /// Represents a conditional description of an object.
    /// </summary>
    /// <param name="falseDescription">The description of this object when the condition returns false.</param>
    /// <param name="condition">The condition.</param>
    public sealed class ConditionalDescription(string falseDescription, DescribedCondition condition) : IDescription
    {
        #region Fields

        private readonly DescribedCondition condition = condition;
        private readonly string falseDescription = falseDescription;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ConditionalDescription class.
        /// </summary>
        /// <param name="trueDescription">The description of this object when the condition returns true.</param>
        /// <param name="falseDescription">The description of this object when the condition returns false.</param>
        /// <param name="condition">The condition.</param>
        public ConditionalDescription(string trueDescription, string falseDescription, Condition condition) : this(falseDescription, new(condition, trueDescription))
        {
        }

        #endregion

        #region Implementation of IDescription

        /// <summary>
        /// Get the description.
        /// </summary>
        /// <returns>The description.</returns>
        public string GetDescription()
        {
            return condition?.Condition?.Invoke() ?? false ? condition.Description : falseDescription;
        }

        #endregion
    }
}
