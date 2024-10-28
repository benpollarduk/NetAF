namespace NetAF.Rendering.Presenter
{
    /// <summary>
    /// Represents an object that can render a frame.
    /// </summary>
    public interface IFramePresenter
    {
        /// <summary>
        /// Write a character.
        /// </summary>
        /// <param name="value">The character to write.</param>
        void Write(char value);
        /// <summary>
        /// Write a string.
        /// </summary>
        /// <param name="value">The string to write.</param>
        void Write(string value);
    }
}
