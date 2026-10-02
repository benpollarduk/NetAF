using NetAF.Logic;
using NetAF.Logic.Modes;
using NetAF.Rendering;

namespace NetAF.Commands.Global
{
    /// <summary>
    /// Represents the Map command.
    /// </summary>
    public sealed class Map : ICommand
    {
        #region StaticProperties

        /// <summary>
        /// Get the command help.
        /// </summary>
        public static CommandHelp CommandHelp { get; } = new("Map", "View the map of the current region", CommandCategory.Global);

        #endregion

        #region Implementation of ICommand

        /// <inheritdoc/>
        public CommandHelp Help => CommandHelp;

        /// <inheritdoc/>
        public Reaction Invoke(Game game)
        {
            if (game == null)
                return new(ReactionResult.Error, "No game specified.");

            var interpreter = game.Configuration.InterpreterProvider.Find(typeof(RegionMapMode));
            game.ChangeMode(new RegionMapMode(RegionMapMode.Player, FrameProperties.RegionMapRenderOptions, interpreter));
            return new(ReactionResult.GameModeChanged, string.Empty);
        }

        /// <inheritdoc/>
        public Prompt[] GetPrompts(Game game)
        {
            return [];
        }

        #endregion
    }
}