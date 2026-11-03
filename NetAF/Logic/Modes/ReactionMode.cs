using NetAF.Assets;
using NetAF.Assets.Locations;
using NetAF.Commands;
using NetAF.Interpretation;
using NetAF.Rendering.FrameBuilders;
using NetAF.Utilities;

namespace NetAF.Logic.Modes
{
    /// <summary>
    /// Provides a display mode for reaction.
    /// </summary>
    /// <param name="reaction">The reaction.</param>
    public sealed class ReactionMode(Reaction reaction) : IGameMode
    {
        #region Implementation of IGameMode

        /// <inheritdoc/>
        public IInterpreter Interpreter { get; }

        /// <inheritdoc/>
        public GameModeType Type { get; } = GameModeType.SingleFrameInformation;

        /// <inheritdoc/>
        public void Render(Game game)
        {
            var keywords = StringUtilities.FindKeywords(new ExaminationScene(game.Player, game.Overworld.CurrentRegion.CurrentRoom, ViewPoint.Create(game.Overworld.CurrentRegion)));
            var frame = game.Configuration.FrameBuilders.GetFrameBuilder<IReactionFrameBuilder>().Build(reaction.Title, reaction.Description, reaction.Result == ReactionResult.Error, keywords, game.Configuration.DisplaySize);
            game.Configuration.Adapter.RenderFrame(frame);
        }

        #endregion
    }
}
