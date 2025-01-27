using NetAF.Assets;
using NetAF.Interpretation;
using NetAF.Rendering.FrameBuilders;

namespace NetAF.Logic.Configuration
{
    /// <summary>
    /// Represents a configuration for a game.
    /// </summary>
    public interface IGameConfiguration
    {
        #region Properties

        /// <summary>
        /// Get the display size.
        /// </summary>
        public Size DisplaySize { get; }

        /// <summary>
        /// Get the exit mode.
        /// </summary>
        public ExitMode ExitMode { get; }

        /// <summary>
        /// Get or set the interpreter used for interpreting input.
        /// </summary>
        public IInterpreter Interpreter { get; set; }

        /// <summary>
        /// Get or set the collection of frame builders to use to render the game.
        /// </summary>
        public FrameBuilderCollection FrameBuilders { get; set; }

        /// <summary>
        /// Get the I/O adapter.
        /// </summary>
        public IIOAdapter Adapter { get; }

        #endregion
    }
}
