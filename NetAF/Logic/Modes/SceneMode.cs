using NetAF.Assets.Locations;
using NetAF.Commands;
using NetAF.Interpretation;
using System.Collections.Generic;

namespace NetAF.Logic.Modes
{
    /// <summary>
    /// Provides a display mode for a scene.
    /// </summary>
    public sealed class SceneMode : IGameMode
    {
        #region Implementation of IGameMode

        /// <summary>
        /// Get the interpreter.
        /// </summary>
        public IInterpreter Interpreter { get; } = Interpreters.SceneInterpreter;

        /// <summary>
        /// Get the type of mode this provides.
        /// </summary>
        public GameModeType Type { get; } = GameModeType.Interactive;

        /// <summary>
        /// Render the current state of a game.
        /// </summary>
        /// <param name="game">The game.</param>
        public void Render(Game game)
        {
            List<CommandHelp> commands = [];
            commands.AddRange(Interpreter.GetContextualCommandHelp(game));
            commands.AddRange(game.Configuration.Interpreter.GetContextualCommandHelp(game));

            var frame = game.Configuration.FrameBuilders.SceneFrameBuilder.Build(game.Overworld.CurrentRegion.CurrentRoom, ViewPoint.Create(game.Overworld.CurrentRegion), game.Player, game.Configuration.DisplayCommandListInSceneFrames ? [.. commands] : null, game.Configuration.SceneMapKeyType, game.Configuration.DisplaySize);
            game.Configuration.Adapter.RenderFrame(frame);
        }

        #endregion
    }
}
