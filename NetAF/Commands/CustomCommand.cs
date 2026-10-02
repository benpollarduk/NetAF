using NetAF.Assets;
using NetAF.Extensions;
using NetAF.Logic;
using NetAF.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;

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
        #region Fields

        private List<Prompt> prompts = [];

        #endregion

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
        /// Get if this command can be interpreted when the IsPlayerVisible is false.
        /// </summary>
        public bool InterpretIfNotPlayerVisible { get; set; } = interpretIfNotPlayerVisible;

        #endregion

        #region Methods

        /// <summary>
        /// Add a prompt.
        /// </summary>
        /// <param name="prompt">The prompt to add.</param>
        public void AddPrompt(Prompt prompt)
        {
            if (prompts.All(x => !x.Entry.InsensitiveEquals(prompt.Entry)))
                prompts.Add(prompt);
        }

        /// <summary>
        /// Remove a prompt.
        /// </summary>
        /// <param name="prompt">The prompt to remove.</param>
        public void RemovePrompt(Prompt prompt)
        {
            prompts.RemoveAll(x => x.Entry.InsensitiveEquals(prompt.Entry));
        }

        #endregion

        #region Implementation of ICommand

        /// <inheritdoc/>
        public CommandHelp Help { get; } = help;

        /// <inheritdoc/>
        public Reaction Invoke(Game game)
        {
            return Callback.Invoke(game, Arguments);
        }

        /// <inheritdoc/>
        public virtual Prompt[] GetPrompts(Game game)
        {
            return [.. prompts];
        }

        /// <inheritdoc/>
        public void ClearPrompts()
        {
            prompts.Clear();
        }

        #endregion

        #region Implementation of IPlayerVisible

        /// <inheritdoc/>
        public bool IsPlayerVisible { get; set; } = isPlayerVisible;

        #endregion

        #region Implementation of IRestoreFromObjectSerialization<CustomCommandSerialization>

        /// <inheritdoc/>
        void IRestoreFromObjectSerialization<CustomCommandSerialization>.RestoreFrom(CustomCommandSerialization serialization)
        {
            IsPlayerVisible = serialization.IsPlayerVisible;
            prompts = [.. serialization.Prompts.Select(x => new Prompt(x))];
        }

        #endregion

        #region Implementation of ICloneable
 
        /// <inheritdoc/>
        public object Clone()
        {
            Prompt[] clonedPrompts = new Prompt[prompts.Count];
            prompts.CopyTo(clonedPrompts);
            return new CustomCommand(Help, IsPlayerVisible, InterpretIfNotPlayerVisible, Callback) { Arguments = Arguments, prompts = [.. clonedPrompts] };
        }

        #endregion
    }
}
