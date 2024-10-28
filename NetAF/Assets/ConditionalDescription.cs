using NetAF.Assets.Interaction;

namespace NetAF.Assets
{
    /// <summary>
    /// Represents a conditional description of an object.
    /// </summary>
    /// <param name="trueDescription">The true description.</param>
    /// <param name="falseDescription">The false description.</param>
    /// <param name="condition">The condition.</param>
    public sealed class ConditionalDescription(string trueDescription, string falseDescription, Condition condition) : Description(trueDescription)
    {
        #region Properties

        /// <summary>
        /// Get or set the description for when this condition is false
        /// </summary>
        private readonly string falseDescription = falseDescription;

        /// <summary>
        /// Get or set the condition
        /// </summary>
        public Condition Condition { get; set; } = condition;

        #endregion

        #region Overrides of Description

        /// <summary>
        /// Get the description.
        /// </summary>
        /// <returns>The description.</returns>
        public override string GetDescription()
        {
            if (Condition != null)
                return Condition.Invoke() ? DefaultDescription : falseDescription;

            return DefaultDescription;
        }

        #endregion
    }
}