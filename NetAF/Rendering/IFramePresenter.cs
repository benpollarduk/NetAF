namespace NetAF.Rendering
{
    /// <summary>
    /// Represents an object that can present a frame.
    /// </summary>
    public interface IFramePresenter
    {
        /// <summary>
        /// Present a frame.
        /// </summary>
        /// <param name="frame">The frame to write, as a string.</param>
        void Present(string frame);
    }
}
