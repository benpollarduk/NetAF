using NetAF.Assets;
using NetAF.Commands.Prompts;
using NetAF.Logic;
using NetAF.Serialization;
using System;

namespace NetAF.Commands
{
    /// <summary>
    /// Provides a custom command.
    /// </summary>
    /// <param name="help">The help for this command.</param>
    /// <param name="isPlayerVisible">If this is visible to the player.</param>
    /// <param name="interpretIfNotPlayerVisible">If this command can be interpreted when the IsPlayerVisible is false.</param>
    /// <param name="callback">The callback to invoke when this command is invoked.</param>
    public class CustomCommand(CommandHelp help, bool isPlayerVisible, bool interpretIfNotPlayerVisible, CustomCommandCallback callback) : ICommand, IPlayerVisible, IRestoreFromObjectSerialization<CustomCommandSerialization>, ICloneable
    {
        #region Properties

        /// <summary>
        /// Get the callback.
        /// </summary>
        private CustomCommandCallback Callback { get; } = callback;

        /// <summary>
        /// Get or set the arguments.
        /// </summary>
        public string[] Arguments { get; set; }

        /// <summary>
        /// Get the help for this command.
        /// </summary>
        public CommandHelp Help { get; } = help;

        /// <summary>
        /// Get if this command can be interpreted when the IsPlayerVisible is false.
        /// </summary>
        public bool InterpretIfNotPlayerVisible { get; set; } = interpretIfNotPlayerVisible;

        #endregion

        #region Implementation of ICommand

        /// <summary>
        /// Invoke the command.
        /// </summary>
        /// <param name="game">The game to invoke the command on.</param>
        /// <returns>The reaction.</returns>
        public Reaction Invoke(Game game)
        {
            return Callback.Invoke(game, Arguments);
        }

        /// <summary>
        /// Get all prompts for this command.
        /// </summary>
        /// <param name="game">The game to get the prompts for.</param>
        /// <returns>And array of prompts.</returns>
        public virtual Prompt[] GetPrompts(Game game)
        {
            return [];
        }

        #endregion

        #region Implementation of IPlayerVisible

        /// <summary>
        /// Get or set if this is visible to the player.
        /// </summary>
        public bool IsPlayerVisible { get; set; } = isPlayerVisible;

        #endregion

        #region Implementation of IRestoreFromObjectSerialization<CustomCommandSerialization>

        /// <summary>
        /// Restore this object from a serialization.
        /// </summary>
        /// <param name="serialization">The serialization to restore from.</param>
        void IRestoreFromObjectSerialization<CustomCommandSerialization>.RestoreFrom(CustomCommandSerialization serialization)
        {
            IsPlayerVisible = serialization.IsPlayerVisible;
        }

        #endregion

        #region Implementation of ICloneable
 
        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        public object Clone()
        {
            return new CustomCommand(Help, IsPlayerVisible, InterpretIfNotPlayerVisible, Callback) { Arguments = Arguments };
        }

        #endregion
    }
}
