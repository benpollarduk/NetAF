using NetAF.Adapters;
using NetAF.Assets;
using NetAF.Console.Adapters;
using NetAF.Console.Rendering.FrameBuilders;
using NetAF.Interpretation;
using NetAF.Logic;
using NetAF.Rendering.FrameBuilders;
using NetAF.Rendering;

namespace NetAF.Console.Logic
{
    /// <summary>
    /// Represents a configuration for a console game.
    /// </summary>
    /// <param name="displaySize">The display size.</param>
    /// <param name="exitMode">The exit mode.</param>
    /// <param name="adapter">The I/O adapter.</param>
    public sealed class ConsoleGameConfiguration(Size displaySize, ExitMode exitMode, IIOAdapter adapter) : IGameConfiguration
    {
        #region StaticProperties

        /// <summary>
        /// Get the default game configuration.
        /// </summary>
        public static IGameConfiguration Default => new ConsoleGameConfiguration(new Size(80, 50), ExitMode.ReturnToTitleScreen, new SystemConsoleAdapter());

        #endregion

        #region Implementation of IGameConfiguration

        /// <summary>
        /// Get the display size.
        /// </summary>
        public Size DisplaySize { get; private set; } = displaySize;

        /// <summary>
        /// Get the exit mode.
        /// </summary>
        public ExitMode ExitMode { get; private set; } = exitMode;

        /// <summary>
        /// Get or set the interpreter used for interpreting input.
        /// </summary>
        public IInterpreter Interpreter { get; set; } = new InputInterpreter(
                                                            new FrameCommandInterpreter(),
                                                            new GlobalCommandInterpreter(),
                                                            new GameCommandInterpreter(),
                                                            new CustomCommandInterpreter(),
                                                            new ConversationCommandInterpreter());

        /// <summary>
        /// Get or set the collection of frame builders to use to render the game.
        /// </summary>
        public FrameBuilderCollection FrameBuilders { get; set; } = FrameBuilderCollections.Default;

        /// <summary>
        /// Get or set the prefix to use when displaying errors.
        /// </summary>
        public string ErrorPrefix { get; set; } = "Oops";

        /// <summary>
        /// Get or set if the command list is displayed in scene frames.
        /// </summary>
        public bool DisplayCommandListInSceneFrames { get; set; } = true;

        /// <summary>
        /// Get or set the type of key to use on the scene map.
        /// </summary>
        public KeyType SceneMapKeyType { get; set; } = KeyType.Dynamic;

        /// <summary>
        /// Get the I/O adapter.
        /// </summary>
        public IIOAdapter Adapter { get; private set; } = adapter;

        #endregion
    }
}
