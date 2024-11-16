﻿using NetAF.Adapters;
using NetAF.Assets;
using NetAF.Interpretation;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;

namespace NetAF.Logic.Configuration
{
    /// <summary>
    /// Represents a configuration for a game.
    /// </summary>
    public interface IGameConfiguration
    {
        #region Properties

        /// <summary>
        /// Get the display size.
        /// </summary>
        public Size DisplaySize { get; }

        /// <summary>
        /// Get the exit mode.
        /// </summary>
        public ExitMode ExitMode { get; }

        /// <summary>
        /// Get or set the interpreter used for interpreting input.
        /// </summary>
        public IInterpreter Interpreter { get; set; }

        /// <summary>
        /// Get or set the collection of frame builders to use to render the game.
        /// </summary>
        public FrameBuilderCollection FrameBuilders { get; set; }

        /// <summary>
        /// Get or set the prefix to use when displaying errors.
        /// </summary>
        public string ErrorPrefix { get; set; }

        /// <summary>
        /// Get or set if the command list is displayed in scene frames.
        /// </summary>
        public bool DisplayCommandListInSceneFrames { get; set; }

        /// <summary>
        /// Get or set the type of key to use on the scene map.
        /// </summary>
        public KeyType SceneMapKeyType { get; set; }

        /// <summary>
        /// Get the I/O adapter.
        /// </summary>
        public IIOAdapter Adapter { get; }

        #endregion
    }
}