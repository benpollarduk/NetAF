using NetAF.Adapters;
using NetAF.Assets;
using NetAF.Console.Adapters;
using NetAF.Console.Rendering.FrameBuilders;
using NetAF.Logic;

namespace NetAF.Console.Logic
{
    /// <summary>
    /// Represents a configuration for a console game.
    /// </summary>
    /// <param name="displaySize">The display size.</param>
    /// <param name="exitMode">The exit mode.</param>
    /// <param name="adapter">The I/O adapter.</param>
    public sealed class ConsoleGameConfiguration(Size displaySize, ExitMode exitMode, IIOAdapter adapter) : GameConfiguration(displaySize, exitMode, adapter)
    {
        #region StaticProperties

        /// <summary>
        /// Get the default game configuration.
        /// </summary>
        public static GameConfiguration Default => new(new Size(80, 50), ExitMode.ReturnToTitleScreen, new SystemConsoleAdapter()) { FrameBuilders = FrameBuilderCollections.Default };

        #endregion
    }
}
