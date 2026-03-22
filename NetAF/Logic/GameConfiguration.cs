using NetAF.Assets;
using NetAF.Interpretation;
using NetAF.Logic.Modes;
using NetAF.Persistence;
using NetAF.Rendering.FrameBuilders;

namespace NetAF.Logic
{
    /// <summary>
    /// Represents a configuration for a console game.
    /// </summary>
    /// <param name="adapter">The I/O adapter.</param>
    /// <param name="frameBuilders">The collection of frame builders to use to render the game.</param>
    /// <param name="displaySize">The size to render the game. To render using the available space use <see cref="Size.Dynamic"/>.</param>
    /// <param name="startModes">The modes to use at the start of the game. Modes will be executed in order. If left null the default start mode will be used.</param>
    /// <param name="finishMode">The mode to use at the end of the game.</param>
    public sealed class GameConfiguration(IIOAdapter adapter, FrameBuilderCollection frameBuilders, Size displaySize, IGameMode[] startModes = null, FinishModes finishMode = FinishModes.Restart)
    {
        #region Properties

        /// <summary>
        /// Get the display size.
        /// </summary>
        public Size DisplaySize
        {
            get
            {
                if (displaySize != Size.Dynamic)
                    return displaySize;

                return Adapter.CurrentOutputSize;
            }
        }

        /// <summary>
        /// Get the start modes. Modes are executed in order.
        /// </summary>
        public IGameMode[] StartModes => startModes;

        /// <summary>
        /// Get the finish mode.
        /// </summary>
        public FinishModes FinishMode => finishMode;

        /// <summary>
        /// Get or set the collection of frame builders to use to render the game.
        /// </summary>
        public FrameBuilderCollection FrameBuilders { get; set; } = frameBuilders;

        /// <summary>
        /// Get the I/O adapter.
        /// </summary>
        public IIOAdapter Adapter => adapter;

        /// <summary>
        /// Get the interpretation provider.
        /// </summary>
        public InterpreterProvider InterpreterProvider { get; } = Interpreters.CreateDefaultInterpreterProvider();

        /// <summary>
        /// Get or set the event to use for auto-saves.
        /// </summary>
        public AutoSaveEvent AutoSaveEvent { get; set; } = AutoSaveEvent.None;

        #endregion
    }
}
