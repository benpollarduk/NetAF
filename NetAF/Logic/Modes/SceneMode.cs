using NetAF.Assets.Locations;
using NetAF.Commands;
using NetAF.Interpretation;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;
using System;
using System.Linq;

namespace NetAF.Logic.Modes
{
    /// <summary>
    /// Provides a display mode for a scene.
    /// <param name="interpreter">Specify the interpreter used for interpreting commands in this mode.</param>
    /// </summary>
    public sealed class SceneMode(IInterpreter interpreter) : IGameMode
    {
        #region StaticProperties

        /// <summary>
        /// Get or set the command categories to display.
        /// </summary>
        public static CommandCategory[] CommandCategories { get; set; } = [CommandCategory.Scene, CommandCategory.Custom, CommandCategory.Global, CommandCategory.Uncategorized, CommandCategory.Movement];

        /// <summary>
        /// Get the minimal command categories.
        /// </summary>
        private static CommandCategory[] MinimalCommandCategories => [CommandCategory.Uncategorized, CommandCategory.Scene, CommandCategory.Custom, CommandCategory.Movement];

        #endregion

        #region Implementation of IGameMode

        /// <inheritdoc/>
        public IInterpreter Interpreter { get; } = interpreter;

        /// <inheritdoc/>
        public GameModeType Type { get; } = GameModeType.Interactive;

        /// <inheritdoc/>
        public void Render(Game game)
        {
            CommandHelp[] commands = FrameProperties.CommandListType switch
            {
                CommandListType.All => Interpreter?.GetContextualCommandHelp(game).Where(x => CommandCategories.Contains(x.Category)).ToArray() ?? [],
                CommandListType.Minimal => Interpreter?.GetContextualCommandHelp(game).Where(x => CommandCategories.Contains(x.Category) && MinimalCommandCategories.Contains(x.Category)).ToArray() ?? [],
                CommandListType.None => [],
                _ => throw new NotImplementedException()
            };

            var frame = game.Configuration.FrameBuilders.GetFrameBuilder<ISceneFrameBuilder>().Build(game.Overworld.CurrentRegion.CurrentRoom, ViewPoint.Create(game.Overworld.CurrentRegion), game.Player, commands, FrameProperties.ShowMapInScenes, FrameProperties.RoomMapRenderOptions, game.Configuration.DisplaySize);
            game.Configuration.Adapter.RenderFrame(frame);
        }

        #endregion
    }
}