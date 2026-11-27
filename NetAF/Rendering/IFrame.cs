namespace NetAF.Rendering
{
    /// <summary>
    /// Represents any object that represents an in game frame which can be presented.
    /// </summary>
    public interface IFrame
    {
        /// <summary>
        /// Compile the frame into a presentable string.
        /// </summary>
        /// <returns>The presentable string.</returns>
        string ToPresentableString();
    }
}
