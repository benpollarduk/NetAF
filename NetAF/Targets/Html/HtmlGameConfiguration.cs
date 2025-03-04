using NetAF.Interpretation;
using NetAF.Logic.Configuration;
using NetAF.Logic;
using NetAF.Rendering.FrameBuilders;
using NetAF.Assets;

namespace NetAF.Targets.Html
{
    /// <summary>
    /// Represents a configuration for a Html game.
    /// </summary>
    /// <param name="adapter">The I/O adapter.</param>
    /// <param name="exitMode">The exit mode.</param>
    public sealed class HtmlGameConfiguration(IIOAdapter adapter, ExitMode exitMode) : IGameConfiguration
    {
        #region Implementation of IGameConfiguration

        /// <summary>
        /// Get the display size.
        /// </summary>
        public Size DisplaySize { get; private set; } = new(80, 50);

        /// <summary>
        /// Get the exit mode.
        /// </summary>
        public ExitMode ExitMode { get; private set; } = exitMode;

        /// <summary>
        /// Get or set the interpreter used for interpreting input.
        /// </summary>
        public IInterpreter Interpreter { get; set; } = Interpreters.Default;

        /// <summary>
        /// Get or set the collection of frame builders to use to render the game.
        /// </summary>
        public FrameBuilderCollection FrameBuilders { get; set; } = FrameBuilderCollections.Html;

        /// <summary>
        /// Get the I/O adapter.
        /// </summary>
        public IIOAdapter Adapter { get; private set; } = adapter;

        #endregion
    }
}
