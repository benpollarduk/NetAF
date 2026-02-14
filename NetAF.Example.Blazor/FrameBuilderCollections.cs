using NetAF.Rendering.FrameBuilders;

namespace NetAF.Example.Blazor
{
    /// <summary>
    /// Contains the frame builder collections for the game.
    /// </summary>
    internal static class FrameBuilderCollections
    {
        /// <summary>
        /// Get the frame builders for console emulation.
        /// </summary>
        internal static FrameBuilderCollection ConsoleEmulation => Rendering.FrameBuilders.FrameBuilderCollections.Console;

        /// <summary>
        /// Get the frame builders using HTML.
        /// </summary>
        internal static FrameBuilderCollection Html => Rendering.FrameBuilders.FrameBuilderCollections.Html;
    }
}
