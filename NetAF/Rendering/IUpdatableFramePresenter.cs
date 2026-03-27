namespace NetAF.Rendering
{
    /// <summary>
    /// Represents an object that can present a frame.
    /// </summary>
    public interface IUpdatableFramePresenter : IFramePresenter
    {
        /// <summary>
        /// Present an updated frame.
        /// </summary>
        /// <param name="frame">The udated frame to write, as a string.</param>
        void PresentUpdate(string frame);
    }
}
