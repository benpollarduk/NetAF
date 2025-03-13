﻿using NetAF.Assets.Locations;
using NetAF.Commands;
using NetAF.Interpretation;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;
using System.Collections.Generic;

namespace NetAF.Logic.Modes
{
    /// <summary>
    /// Provides a display mode for a scene.
    /// </summary>
    public sealed class SceneMode : IGameMode
    {
        #region StaticProperties

        /// <summary>
        /// Get or set if the command list is displayed.
        /// </summary>
        public static bool DisplayCommandList { get; set; } = true;

        /// <summary>
        /// Get or set the type of key to use on the map.
        /// </summary>
        public static KeyType KeyType { get; set; } = KeyType.Dynamic;

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
            var commands = Interpreter.GetContextualCommandHelp(game);
            var frame = game.Configuration.FrameBuilders.GetFrameBuilder<ISceneFrameBuilder>().Build(game.Overworld.CurrentRegion.CurrentRoom, ViewPoint.Create(game.Overworld.CurrentRegion), game.Player, DisplayCommandList ? commands : null, KeyType, game.Configuration.DisplaySize);
            game.Configuration.Adapter.RenderFrame(frame);
        }

        #endregion
    }
}
