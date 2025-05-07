namespace NetAF.Assets
{
    /// <summary>
    /// A described condition.
    /// </summary>
    /// <param name="Condition">The condition.</param>
    /// <param name="Description">The description of the condition.</param>
    public sealed record DescribedCondition(Condition Condition, string Description);
}
