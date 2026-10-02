using NetAF.Interpretation;
using NetAF.Logging.Notes;
using NetAF.Rendering.FrameBuilders;
using System.Linq;

namespace NetAF.Logic.Modes
{
    /// <summary>
    /// Provides a display mode for the in-game notes.
    /// </summary>
    /// <param name="noteManager">The note manager.</param>
    public sealed class NoteMode(NoteManager noteManager) : IGameMode
    {
        #region Implementation of IGameMode

        /// <inheritdoc/>
        public IInterpreter Interpreter { get; }

        /// <inheritdoc/>
        public GameModeType Type { get; } = GameModeType.SingleFrameInformation;

        /// <inheritdoc/>
        public void Render(Game game)
        {
            var frame = game.Configuration.FrameBuilders.GetFrameBuilder<INoteFrameBuilder>().Build("Notes", string.Empty, noteManager?.GetAll().Where(x => !string.IsNullOrEmpty(x.Content)).ToArray(), game.Configuration.DisplaySize);
            game.Configuration.Adapter.RenderFrame(frame);
        }

        #endregion
    }
}
