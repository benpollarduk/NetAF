using NetAF.Assets;
using NetAF.Information;

namespace NetAF.Rendering.FrameBuilders
{
    /// <summary>
    /// Represents any object that can build information frames.
    /// </summary>
    public interface IInformationFrameBuilder : IFrameBuilder
    {
        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="description">The description.</param>
        /// <param name="entries">The information entries.</param>
        /// <param name="size">The size of the frame.</param>
        /// <returns>The frame.</returns>
        IFrame Build(string title, string description, InformationEntry[] entries, Size size);
    }
}
