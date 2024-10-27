using NetAF.Assets;
using NetAF.Interpretation;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;

namespace NetAF.Logic
{
    /// <summary>
    /// Represents a configuration for a game.
    /// </summary>
    public sealed class GameConfiguration
    {
        #region StaticProperties

        /// <summary>
        /// Get the default game configuration.
        /// </summary>
        public static GameConfiguration Default => new GameConfiguration(new Size(80, 50), ExitMode.ReturnToTitleScreen, CreateDefaultInterpreter());

        #endregion

        #region Properties

        /// <summary>
        /// Get the display size.
        /// </summary>
        public Size DisplaySize { get; private set; }

        /// <summary>
        /// Get the exit mode.
        /// </summary>
        public ExitMode ExitMode { get; private set; }

        /// <summary>
        /// Get the interpreter used for interpreting input.
        /// </summary>
        public IInterpreter Interpreter { get; private set; }

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

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the GameConfiguration class.
        /// </summary>
        /// <param name="displaySize">The display size.</param>
        /// <param name="exitMode">The exit mode.</param>
        /// <param name="interpreter">The interpreter used for interpreting input</param>
        public GameConfiguration(Size displaySize, ExitMode exitMode, IInterpreter interpreter)
        {
            DisplaySize = displaySize;
            ExitMode = exitMode;
            Interpreter = interpreter;
        }

        #endregion

        #region StaticMethods

        /// <summary>
        /// Create the default interpreter.
        /// </summary>
        /// <returns></returns>
        private static IInterpreter CreateDefaultInterpreter()
        {
            return new InputInterpreter(
                new FrameCommandInterpreter(),
                new GlobalCommandInterpreter(),
                new GameCommandInterpreter(),
                new CustomCommandInterpreter(),
                new ConversationCommandInterpreter());
        }

        #endregion
    }
}
