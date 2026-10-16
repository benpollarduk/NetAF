using NetAF.Logic;
using NetAF.Rendering;
using System;
using System.Linq;

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
        /// Get the prompt for light commands.
        /// </summary>
        public static Prompt CommandsLight => new("commands-light");

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
        /// Get the prompt for key placement to the right.
        /// </summary>
        public static Prompt KeyRight => new("key-right");

        /// <summary>
        /// Get the prompt for key placement to the left.
        /// </summary>
        public static Prompt KeyLeft => new("key-left");

        /// <summary>
        /// Get the prompt for key placement above.
        /// </summary>
        public static Prompt KeyAbove => new("key-above");

        /// <summary>
        /// Get the prompt for key placement below.
        /// </summary>
        public static Prompt KeyBelow => new("key-below");

        /// <summary>
        /// Get the prompt for point of interest none.
        /// </summary>
        public static Prompt PointOfInterestNone => new("poi-none");

        /// <summary>
        /// Get the prompt for point of interest low.
        /// </summary>
        public static Prompt PointOfInterestLow => new("poi-low");

        /// <summary>
        /// Get the prompt for point of interest high.
        /// </summary>
        public static Prompt PointOfInterestHigh => new("poi-high");

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

        private static (Prompt Prompt, Action Apply, string Message)[] GetOptionHandlers()
        {
            return
            [
                (CommandsNone, () => FrameProperties.CommandListType = CommandListType.None, "Commands have been set to none."),
                (CommandsLight, () => FrameProperties.CommandListType = CommandListType.Light, "Commands have been set to light."),
                (CommandsAll, () => FrameProperties.CommandListType = CommandListType.All, "Commands have been set to all."),
                (KeyNone, () => FrameProperties.RoomMapRenderOptions.KeyType = KeyType.None, "Key has been set to none."),
                (KeyDynamic, () => FrameProperties.RoomMapRenderOptions.KeyType = KeyType.Dynamic, "Key has been set to dynamic."),
                (KeyFull, () => FrameProperties.RoomMapRenderOptions.KeyType = KeyType.Full, "Key has been set to full."),
                (KeyRight, () => FrameProperties.RoomMapRenderOptions.KeyPlacement = KeyPlacement.Right, "Key placement has been set to right."),
                (KeyLeft, () => FrameProperties.RoomMapRenderOptions.KeyPlacement = KeyPlacement.Left, "Key placement has been set to left."),
                (KeyAbove, () => FrameProperties.RoomMapRenderOptions.KeyPlacement = KeyPlacement.Above, "Key placement has been set to above."),
                (KeyBelow, () => FrameProperties.RoomMapRenderOptions.KeyPlacement = KeyPlacement.Below, "Key placement has been set to below."),
                (PointOfInterestNone, () => FrameProperties.RoomMapRenderOptions.PointOfInterestDetail = PointOfInterestDetail.None, "Point of interest detail has been set to none."),
                (PointOfInterestLow, () => FrameProperties.RoomMapRenderOptions.PointOfInterestDetail = PointOfInterestDetail.Low, "Point of interest detail has been set to low."),
                (PointOfInterestHigh, () => FrameProperties.RoomMapRenderOptions.PointOfInterestDetail = PointOfInterestDetail.High, "Point of interest detail has been set to high."),
                (MapInScenesOff, () => FrameProperties.ShowMapInScenes = false, "Map in scenes has been turned off."),
                (MapInScenesOn, () => FrameProperties.ShowMapInScenes = true, "Map in scenes has been turned on.")
            ];
        }

        #endregion

        #region Implementation of ICommand

        /// <inheritdoc/>
        public CommandHelp Help => CommandHelp;

        /// <inheritdoc/>
        public Reaction Invoke(Game game)
        {
            if (game == null)
                return new(ReactionResult.Error, "No game specified.");

            if (string.IsNullOrEmpty(arg))
                return new(ReactionResult.Error, "No argument specified.");

            foreach (var handler in GetOptionHandlers())
            {
                if (!IsPrompt(arg, handler.Prompt))
                    continue;

                handler.Apply();
                return new(ReactionResult.Inform, handler.Message);
            }

            return new(ReactionResult.Error, $"Unrecognised argument {arg}.");
        }

        /// <inheritdoc/>
        public Prompt[] GetPrompts(Game game)
        {
            return [.. GetOptionHandlers().Select(x => x.Prompt)];
        }

        #endregion
    }
}