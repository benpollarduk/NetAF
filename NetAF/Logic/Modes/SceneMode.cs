using NetAF.Assets.Locations;
using NetAF.Commands;
using NetAF.Interpretation;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;
using System.Linq;

namespace NetAF.Logic.Modes
{
    /// <summary>
    /// Provides a display mode for a scene.
    /// </summary>
    public sealed class SceneMode : IGameMode
    {
        #region StaticProperties

        /// <summary>
        /// Get or set the command categories to display.
        /// </summary>
        public static CommandCategory[] CommandCategories { get; set; } = [CommandCategory.Scene, CommandCategory.Custom, CommandCategory.Global, CommandCategory.Uncategorized, CommandCategory.Movement];

        #endregion

        #region Implementation of IGameMode

        /// <summary>
        /// Get the interpreter.
        /// </summary>
        public IInterpreter Interpreter { get; } = new InputInterpreter
        (
            new FrameCommandInterpreter(),
            new GlobalCommandInterpreter(),
            new ExecutionCommandInterpreter(),
            new PersistenceCommandInterpreter(),
            new CustomCommandInterpreter(),
            new SceneCommandInterpreter()
        );

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
            var filteredCommands = Interpreter.GetContextualCommandHelp(game).Where(x => CommandCategories.Contains(x.Category)).ToArray();
            var frame = game.Configuration.FrameBuilders.GetFrameBuilder<ISceneFrameBuilder>().Build(game.Overworld.CurrentRegion.CurrentRoom, ViewPoint.Create(game.Overworld.CurrentRegion), game.Player, FrameProperties.DisplayCommandList ? filteredCommands : null, FrameProperties.KeyType, game.Configuration.DisplaySize);
            game.Configuration.Adapter.RenderFrame(frame);
        }

        #endregion
    }
}