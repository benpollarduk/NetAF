namespace NetAF.Rendering
{
    /// <summary>
    /// Represents an object that can present a frame.
    /// </summary>
    public interface IFramePresenter
    {
        /// <summary>
        /// Write a string.
        /// </summary>
        /// <param name="value">The string to write.</param>
        void Write(string value);
    }
}
