using NetAF.Logic;
using NetAF.Rendering;

namespace NetAF.Commands.Frame
{
    /// <summary>
    /// Represents the Options command.
    /// </summary>
    /// <param name="arg">The argument detailing the option to adjust.</param>
    public sealed class Option(string arg) : ICommand
    {
        #region StaticProperties

        /// <summary>
        /// Get the command help.
        /// </summary>
        public static CommandHelp CommandHelp { get; } = new("Option", "Adjust in-game options", CommandCategory.Frame);

        /// <summary>
        /// Get the prompt for no commands.
        /// </summary>
        public static Prompt CommandsNone => new("commands-none");

        /// <summary>
        /// Get the prompt for all commands.
        /// </summary>
        public static Prompt CommandsAll => new("commands-all");

        /// <summary>
        /// Get the prompt for minimal commands.
        /// </summary>
        public static Prompt CommandsMinimal => new("commands-minimal");

        /// <summary>
        /// Get the prompt for no key.
        /// </summary>
        public static Prompt KeyNone => new("key-none");

        /// <summary>
        /// Get the prompt for full key.
        /// </summary>
        public static Prompt KeyFull => new("key-full");

        /// <summary>
        /// Get the prompt for dynamic key.
        /// </summary>
        public static Prompt KeyDynamic => new("key-dynamic");

        /// <summary>
        /// Get the prompt for map in scenes on.
        /// </summary>
        public static Prompt MapInScenesOn => new("map-on");

        /// <summary>
        /// Get the prompt for map in scenes off.
        /// </summary>
        public static Prompt MapInScenesOff => new("map-off");

        #endregion

        #region StaticMethods

        private static bool IsPrompt(string arg, Prompt prompt)
        {
            return prompt.Entry.Equals(arg, System.StringComparison.InvariantCultureIgnoreCase);
        }

        #endregion

        #region Implementation of ICommand

        /// <summary>
        /// Get the help for this command.
        /// </summary>
        public CommandHelp Help => CommandHelp;

        /// <summary>
        /// Invoke the command.
        /// </summary>
        /// <param name="game">The game to invoke the command on.</param>
        /// <returns>The reaction.</returns>
        public Reaction Invoke(Game game)
        {
            if (game == null)
                return new(ReactionResult.Error, "No game specified.");

            if (string.IsNullOrEmpty(arg))
                return new(ReactionResult.Error, "No argument specified.");

            if (IsPrompt(arg, CommandsNone))
            {
                FrameProperties.CommandListType = CommandListType.None;
                return new(ReactionResult.Inform, "Commands have been set to none.");
            }

            if (IsPrompt(arg, CommandsMinimal))
            {
                FrameProperties.CommandListType = CommandListType.Minimal;
                return new(ReactionResult.Inform, "Commands have been set to minimal.");
            }

            if (IsPrompt(arg, CommandsAll))
            {
                FrameProperties.CommandListType = CommandListType.All;
                return new(ReactionResult.Inform, "Commands have been set to all.");
            }

            if (IsPrompt(arg, KeyNone))
            {
                FrameProperties.KeyType = KeyType.None;
                return new(ReactionResult.Inform, "Key has been set to none.");
            }

            if (IsPrompt(arg, KeyDynamic))
            {
                FrameProperties.KeyType = KeyType.Dynamic;
                return new(ReactionResult.Inform, "Key has been set to dynamic.");
            }

            if (IsPrompt(arg, KeyFull))
            {
                FrameProperties.KeyType = KeyType.Full;
                return new(ReactionResult.Inform, "Key has been set to full.");
            }

            if (IsPrompt(arg, MapInScenesOff))
            {
                FrameProperties.ShowMapInScenes = false;
                return new(ReactionResult.Inform, "Map in scenes has been turned off.");
            }

            if (IsPrompt(arg, MapInScenesOn))
            {
                FrameProperties.ShowMapInScenes = true;
                return new(ReactionResult.Inform, "Map in scenes has been turned on.");
            }

            return new(ReactionResult.Error, $"Unrecognised argument {arg}.");
        }

        /// <summary>
        /// Get all prompts for this command.
        /// </summary>
        /// <param name="game">The game to get the prompts for.</param>
        /// <returns>And array of prompts.</returns>
        public Prompt[] GetPrompts(Game game)
        {
            return 
            [
                CommandsNone,
                CommandsMinimal,
                CommandsAll,
                KeyNone,
                KeyDynamic,
                KeyFull,
                MapInScenesOff,
                MapInScenesOn
            ];
        }

        #endregion
    }
}