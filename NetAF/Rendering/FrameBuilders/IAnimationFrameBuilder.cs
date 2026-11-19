using NetAF.Assets;

namespace NetAF.Rendering.FrameBuilders
{
    /// <summary>
    /// Represents any object that can build animation frames.
    /// </summary>
    public interface IAnimationFrameBuilder : IFrameBuilder
    {
        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="animation">The animation.</param>
        /// <param name="size">The size of the frame.</param>
        /// <returns>The frame.</returns>
        IFrame Build(Animation animation, Size size);
    }
}
