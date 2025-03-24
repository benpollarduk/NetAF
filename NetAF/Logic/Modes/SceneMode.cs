using NetAF.Assets.Locations;
using NetAF.Commands;
using NetAF.Interpretation;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;
using System.Collections.Generic;
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
            var filteredCommands = commands.Where(x => CommandCategories.Contains(x.Category)).ToArray();

            var frame = game.Configuration.FrameBuilders.GetFrameBuilder<ISceneFrameBuilder>().Build(game.Overworld.CurrentRegion.CurrentRoom, ViewPoint.Create(game.Overworld.CurrentRegion), game.Player, FrameProperties.DisplayCommandList ? filteredCommands : null, FrameProperties.KeyType, game.Configuration.DisplaySize);
            game.Configuration.Adapter.RenderFrame(frame);
        }

        #endregion
    }
}