namespace NetAF.Logic
{
    /// <summary>
    /// Represents the result of an update action.
    /// </summary>
    /// <param name="Completed">True if the update completed successfully, else false.</param>
    /// <param name="Description">A description of the the result.</param>
    public record UpdateResult(bool Completed, string Description = "");
}
