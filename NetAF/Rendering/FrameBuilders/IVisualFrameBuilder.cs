using NetAF.Assets;
using NetAF.Targets.Console.Rendering;

namespace NetAF.Rendering.FrameBuilders
{
    /// <summary>
    /// Represents any object that can build visual frames.
    /// </summary>
    public interface IVisualFrameBuilder : IFrameBuilder
    {
        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="description">The description.</param>
        /// <param name="gridVisualBuilder">The grid visual builder containing the visual.</param>
        /// <param name="size">The size of the frame.</param>
        /// <returns>The frame.</returns>
        IFrame Build(string title, string description, GridVisualBuilder gridVisualBuilder, Size size);
    }
}
