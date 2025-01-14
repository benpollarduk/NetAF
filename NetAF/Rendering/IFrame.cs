namespace NetAF.Rendering
{
    /// <summary>
    /// Represents any object that is a frame for displaying an interface.
    /// </summary>
    public interface IFrame
    {
        /// <summary>
        /// Render this frame on a presenter.
        /// </summary>
        /// <param name="presenter">The presenter.</param>
        void Render(IFramePresenter presenter);
    }
}
