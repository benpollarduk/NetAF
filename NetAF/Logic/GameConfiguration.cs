using NetAF.Assets;
using NetAF.Interpretation;
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
        public static GameConfiguration Default { get; } = new GameConfiguration(new Size(80, 50), FrameBuilderCollections.Default, ExitMode.ReturnToTitleScreen, "Oops", CreateDefaultInterpreter());

        #endregion

        #region Properties

        /// <summary>
        /// Get the display size.
        /// </summary>
        public Size DisplaySize { get; private set; }

        /// <summary>
        /// Get the collection of frame builders to use to render the game.
        /// </summary>
        public FrameBuilderCollection FrameBuilders { get; private set; }

        /// <summary>
        /// Get the exit mode.
        /// </summary>
        public ExitMode ExitMode { get; private set; }

        /// <summary>
        /// Get the prefix to use when displaying errors.
        /// </summary>
        public string ErrorPrefix { get; private set; }

        /// <summary>
        /// Get the interpreter used for interpreting input.
        /// </summary>
        public IInterpreter Interpreter { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the GameConfiguration class.
        /// </summary>
        /// <param name="displaySize">The display size.</param>
        /// <param name="frameBuilders">The collection of frame builders to use to render the game.</param>
        /// <param name="exitMode">The exit mode.</param>
        /// <param name="errorPrefix">The prefix to use when displaying errors.</param>
        /// <param name="interpreter">The interpreter used for interpreting input</param>
        public GameConfiguration(Size displaySize, FrameBuilderCollection frameBuilders, ExitMode exitMode, string errorPrefix, IInterpreter interpreter)
        {
            DisplaySize = displaySize;
            FrameBuilders = frameBuilders;
            ExitMode = exitMode;
            ErrorPrefix = errorPrefix;
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
