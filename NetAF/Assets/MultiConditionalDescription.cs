using System.Linq;

namespace NetAF.Assets
{
    /// <summary>
    /// Represents a multi conditional description of an object.
    /// </summary>
    /// <param name="fallbackDescription">The description of this object when no condition is true.</param>
    /// <param name="describedConditions">The conditional descriptions.</param>
    public sealed class MultiConditionalDescription(string fallbackDescription, params DescribedCondition[] describedConditions) : IDescription
    {
        #region Implementation of IDescription

        /// <summary>
        /// Get the description.
        /// </summary>
        /// <returns>The description.</returns>
        public string GetDescription()
        {
            var firstHit = describedConditions?.FirstOrDefault(x => x.Condition.Invoke());

            if (firstHit != null)
                return firstHit.Description;

            return fallbackDescription;
        }

        #endregion
    }
}