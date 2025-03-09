using NetAF.Assets;
using NetAF.Interpretation;
using NetAF.Rendering.FrameBuilders;

namespace NetAF.Logic
{
    /// <summary>
    /// Represents a configuration for a console game.
    /// </summary>
    /// <param name="adapter">The I/O adapter.</param>
    /// <param name="frameBuilders">The collection of frame builders to use to render the game.</param>
    /// <param name="displaySize">The display size.</param>
    /// <param name="finishMode">The finish mode.</param>
    public sealed class GameConfiguration(IIOAdapter adapter, FrameBuilderCollection frameBuilders, Size displaySize, FinishModes finishMode = FinishModes.ReturnToTitleScreen)
    {
        #region Properties

        /// <summary>
        /// Get the display size.
        /// </summary>
        public Size DisplaySize { get; private set; } = displaySize;

        /// <summary>
        /// Get the finish mode.
        /// </summary>
        public FinishModes FinishMode { get; private set; } = finishMode;

        /// <summary>
        /// Get or set the interpreter used for interpreting input.
        /// </summary>
        public IInterpreter Interpreter { get; set; } = Interpreters.Default;

        /// <summary>
        /// Get or set the collection of frame builders to use to render the game.
        /// </summary>
        public FrameBuilderCollection FrameBuilders { get; set; } = frameBuilders;

        /// <summary>
        /// Get the I/O adapter.
        /// </summary>
        public IIOAdapter Adapter { get; private set; } = adapter;

        #endregion
    }
}
