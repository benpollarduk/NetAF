using System;
using System.Collections.Generic;
using System.Linq;
using NetAF.Assets;
using NetAF.Assets.Locations;
using NetAF.Commands;
using NetAF.Commands.Scene;
using NetAF.Extensions;
using NetAF.Logic;
using NetAF.Logic.Modes;
using NetAF.Utilities;

namespace NetAF.Interpretation
{
    /// <summary>
    /// Provides an object that can be used for interpreting scene commands.
    /// </summary>
    public sealed class SceneCommandInterpreter : IInterpreter
    {
        #region Constants

        /// <summary>
        /// Get the player identifier.
        /// </summary>
        internal const string Me = "Me";

        /// <summary>
        /// Get the room identifier.
        /// </summary>
        internal const string Room = "Room";

        /// <summary>
        /// Get the region identifier.
        /// </summary>
        internal const string Region = "Region";

        /// <summary>
        /// Get the overworld identifier.
        /// </summary>
        internal const string Overworld = "Overworld";

        #endregion

        #region StaticProperties

        /// <summary>
        /// Get an array of all supported commands.
        /// </summary>
        public static CommandHelp[] DefaultSupportedCommands { get; } =
        [
            Move.NorthCommandHelp,
            Move.EastCommandHelp,
            Move.SouthCommandHelp,
            Move.WestCommandHelp,
            Move.UpCommandHelp,
            Move.DownCommandHelp,
            Drop.CommandHelp,
            Examine.CommandHelp,
            Take.CommandHelp,
            TakeAll.CommandHelp,
            Talk.TalkCommandHelp,
            UseOn.UseCommandHelp,
            UseOn.OnCommandHelp
        ];

        #endregion

        #region StaticMethods

        /// <summary>
        /// Try and parse the Drop command.
        /// </summary>
        /// <param name="text">The text to parse.</param>
        /// <param name="game">The game.</param>
        /// <param name="command">The resolved command.</param>
        /// <returns>True if the input could be parsed, else false.</returns>
        private static bool TryParseDropCommand(string text, Game game, out ICommand command)
        {
            StringUtilities.SplitTextToVerbAndNoun(text, out var verb, out var noun);

            if (!Drop.CommandHelp.Equals(verb))
            {
                command = null;
                return false;
            }

            game.Player.FindItem(noun, out var item);
            command = new Drop(item);
            return true;
        }

        /// <summary>
        /// Try and parse the Take command.
        /// </summary>
        /// <param name="text">The text to parse.</param>
        /// <param name="game">The game.</param>
        /// <param name="command">The resolved command.</param>
        /// <returns>True if the input could be parsed, else false.</returns>
        private static bool TryParseTakeCommand(string text, Game game, out ICommand command)
        {
            StringUtilities.SplitTextToVerbAndNoun(text, out var verb, out var noun);

            if (!Take.CommandHelp.Equals(verb))
            {
                command = null;
                return false;
            }

            Item item;

            // it no item specified then find the first takeable one
            if (string.IsNullOrEmpty(noun))
            {
                item = Array.Find(game.Overworld.CurrentRegion.CurrentRoom.Items, x => x.IsPlayerVisible && x.IsTakeable);

                if (item == null)
                {
                    command = new Unactionable("There are no takeable items in the room.");
                    return true;
                }
            }
            else if (TakeAll.CommandHelp.Equals($"{verb} {noun}"))
            {
                command = new TakeAll();
                return true;
            }
            else if (!game.Overworld.CurrentRegion.CurrentRoom.FindItem(noun, out item))
            {
                command = new Unactionable("There is no such item in the room.");
                return true;
            }

            command = new Take(item);
            return true;
        }

        /// <summary>
        /// Try and parse the Talk command.
        /// </summary>
        /// <param name="text">The text to parse.</param>
        /// <param name="game">The game.</param>
        /// <param name="command">The resolved command.</param>
        /// <returns>True if the input could be parsed, else false.</returns>
        private static bool TryParseTalkCommand(string text, Game game, out ICommand command)
        {
            StringUtilities.SplitTextToVerbAndNoun(text, out var verb, out var noun);

            if (!Talk.TalkCommandHelp.Equals(verb))
            {
                command = null;
                return false;
            }

            // determine if a target has been specified
            if (noun.Length > 3 && Talk.ToCommandHelp.Equals(noun.Substring(0, 2)))
            {
                noun = noun.Remove(0, 3);

                if (game.Overworld.CurrentRegion.CurrentRoom.FindCharacter(noun, out var nPC))
                {
                    command = new Talk(nPC);
                    return true;
                }
            }

            if (game.Overworld.CurrentRegion.CurrentRoom.Characters.Length == 1)
            {
                command = new Talk(game.Overworld.CurrentRegion.CurrentRoom.Characters[0]);
                return true;
            }

            command = new Unactionable("No-one is around to talk to");
            return true;
        }

        /// <summary>
        /// Try and parse the Examine command for a location.
        /// </summary>
        /// <param name="noun">The noun.</param>
        /// <param name="game">The game.</param>
        /// <param name="command">The resolved command.</param>
        /// <returns>True if the input could be parsed, else false.</returns>
        private static bool TryParseExamineCommandLocations(string noun, Game game, out ICommand command)
        {
            if (string.IsNullOrEmpty(noun))
            {
                // default to current room
                command = new Examine(game.Overworld.CurrentRegion.CurrentRoom);
                return true;
            }

            // check exits to room
            if (TryParseToDirection(noun, out var direction))
            {
                if (game.Overworld.CurrentRegion.CurrentRoom.FindExit(direction, false, out var exit))
                {
                    command = new Examine(exit);
                    return true;
                }

                command = new Unactionable($"There is no exit in this room to the {direction}");
                return true;
            }

            // check room examination
            if (Room.InsensitiveEquals(noun) || noun.EqualsExaminable(game.Overworld.CurrentRegion.CurrentRoom))
            {
                command = new Examine(game.Overworld.CurrentRegion.CurrentRoom);
                return true;
            }

            // check region examination
            if (Region.InsensitiveEquals(noun) || noun.EqualsExaminable(game.Overworld.CurrentRegion))
            {
                command = new Examine(game.Overworld.CurrentRegion);
                return true;
            }

            // check overworld examination
            if (Overworld.InsensitiveEquals(noun) || noun.EqualsExaminable(game.Overworld))
            {
                command = new Examine(game.Overworld);
                return true;
            }

            command = null;
            return false;
        }

        /// <summary>
        /// Try and parse the Examine command.
        /// </summary>
        /// <param name="text">The text to parse.</param>
        /// <param name="game">The game.</param>
        /// <param name="command">The resolved command.</param>
        /// <returns>True if the input could be parsed, else false.</returns>
        private static bool TryParseExamineCommand(string text, Game game, out ICommand command)
        {
            StringUtilities.SplitTextToVerbAndNoun(text, out var verb, out var noun);

            if (!Examine.CommandHelp.Equals(verb))
            {
                command = null;
                return false;
            }

            if (string.IsNullOrEmpty(noun))
            {
                // default to current room
                command = new Examine(game.Overworld.CurrentRegion.CurrentRoom);
                return true;
            }

            // try locations
            if (TryParseExamineCommandLocations(noun, game, out command))
                return true;

            // check player items
            if (game.Player.FindItem(noun, out var item))
            {
                command = new Examine(item);
                return true;
            }

            // check items in room
            if (game.Overworld.CurrentRegion.CurrentRoom.FindItem(noun, out item))
            {
                command = new Examine(item);
                return true;
            }

            // check characters in room
            if (game.Overworld.CurrentRegion.CurrentRoom.FindCharacter(noun, out var character))
            {
                command = new Examine(character);
                return true;
            }

            // check self examination
            if (Me.InsensitiveEquals(noun) || noun.EqualsExaminable(game.Player))
            {
                command = new Examine(game.Player);
                return true;
            }

            // unknown
            if (!string.IsNullOrEmpty(noun))
            {
                command = new Unactionable($"Can't examine {noun}.");
                return true;
            }

            // default to current room
            command = new Examine(game.Overworld.CurrentRegion.CurrentRoom);
            return true;
        }

        /// <summary>
        /// Try and parse the UseOn command.
        /// </summary>
        /// <param name="text">The text to parse.</param>
        /// <param name="game">The game.</param>
        /// <param name="command">The resolved command.</param>
        /// <returns>True if the input could be parsed, else false.</returns>
        private static bool TryParseUseOnCommand(string text, Game game, out ICommand command)
        {
            StringUtilities.SplitTextToVerbAndNoun(text, out var verb, out var noun);

            if (!UseOn.UseCommandHelp.Equals(verb))
            {
                command = null;
                return false;
            }

            IInteractWithItem target;
            var itemName = noun;
            var onPadded = $" {UseOn.OnCommandHelp.Command} ";

            if (noun.CaseInsensitiveContains(onPadded))
            {
                itemName = noun[..noun.IndexOf(onPadded, StringComparison.CurrentCultureIgnoreCase)];
                noun = noun.Replace(itemName, string.Empty);
                var onIndex = noun.IndexOf(onPadded, StringComparison.CurrentCultureIgnoreCase);
                var targetName = noun[(onIndex + onPadded.Length)..];

                if (targetName.InsensitiveEquals(Me) || targetName.EqualsExaminable(game.Player))
                    target = game.Player;
                else if (targetName.InsensitiveEquals(Room) || targetName.EqualsExaminable(game.Overworld.CurrentRegion.CurrentRoom))
                    target = game.Overworld.CurrentRegion.CurrentRoom;
                else
                    target = game.FindInteractionTarget(targetName);

                if (target == null)
                {
                    command = new Unactionable($"{targetName} is not a valid target.");
                    return true;
                }
            }
            else
            {
                target = game.Overworld.CurrentRegion.CurrentRoom;
            }

            if (!game.Player.FindItem(itemName, out var item) && !game.Overworld.CurrentRegion.CurrentRoom.FindItem(itemName, out item))
            {
                command = new Unactionable("You don't have that item.");
                return true;
            }

            command = new UseOn(item, target);
            return true;
        }

        /// <summary>
        /// Try and parse a string to a Direction.
        /// </summary>
        /// <param name="text">The string to parse.</param>
        /// <param name="direction">The direction.</param>
        /// <returns>The result of the parse.</returns>
        internal static bool TryParseToDirection(string text, out Direction direction)
        {
            if (Move.NorthCommandHelp.Equals(text))
            {
                direction = Direction.North;
                return true;
            }

            if (Move.EastCommandHelp.Equals(text))
            {
                direction = Direction.East;
                return true;
            }

            if (Move.SouthCommandHelp.Equals(text))
            {
                direction = Direction.South;
                return true;
            }

            if (Move.WestCommandHelp.Equals(text))
            {
                direction = Direction.West;
                return true;
            }

            if (Move.UpCommandHelp.Equals(text))
            {
                direction = Direction.Up;
                return true;
            }

            if (Move.DownCommandHelp.Equals(text))
            {
                direction = Direction.Down;
                return true;
            }

            direction = Direction.East;
            return false;
        }

        #endregion

        #region Implementation of IInterpreter

        /// <summary>
        /// Get an array of all supported commands.
        /// </summary>
        public CommandHelp[] SupportedCommands { get; } = DefaultSupportedCommands;

        /// <summary>
        /// Interpret a string.
        /// </summary>
        /// <param name="input">The string to interpret.</param>
        /// <param name="game">The game.</param>
        /// <returns>The result of the interpretation.</returns>
        public InterpretationResult Interpret(string input, Game game)
        {
            // try and parse as movement
            if (TryParseToDirection(input, out var direction))
                return new(true, new Move(direction));

            // handle as drop command
            if (TryParseDropCommand(input, game, out var drop))
                return new(true, drop);

            // handle as examine command
            if (TryParseExamineCommand(input, game, out var examine))
                return new(true, examine);

            // handle as take command
            if (TryParseTakeCommand(input, game, out var take))
                return new(true, take);

            // handle as talk command
            if (TryParseTalkCommand(input, game, out var talk))
                return new(true, talk);

            // handle as use on command
            if (TryParseUseOnCommand(input, game, out var useOn))
                return new(true, useOn);

            return new(false, new Unactionable("Invalid input."));
        }

        /// <summary>
        /// Get contextual command help for a game, based on its current state.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <returns>The contextual help.</returns>
        public CommandHelp[] GetContextualCommandHelp(Game game)
        {
            var mode = game.Mode as ConversationMode;

            if (mode?.Converser?.Conversation != null)
                return [];

            List<CommandHelp> commands = [];

            if (game.Overworld.CurrentRegion.CurrentRoom.CanMove(Direction.North))
                commands.Add(Move.NorthCommandHelp);

            if (game.Overworld.CurrentRegion.CurrentRoom.CanMove(Direction.East))
                commands.Add(Move.EastCommandHelp);

            if (game.Overworld.CurrentRegion.CurrentRoom.CanMove(Direction.South))
                commands.Add(Move.SouthCommandHelp);

            if (game.Overworld.CurrentRegion.CurrentRoom.CanMove(Direction.West))
                commands.Add(Move.WestCommandHelp);

            if (game.Overworld.CurrentRegion.CurrentRoom.CanMove(Direction.Up))
                commands.Add(Move.UpCommandHelp);

            if (game.Overworld.CurrentRegion.CurrentRoom.CanMove(Direction.Down))
                commands.Add(Move.DownCommandHelp);

            commands.Add(Examine.CommandHelp);

            if (game.Player.Items.Any())
                commands.Add(Drop.CommandHelp);

            if (game.Overworld.CurrentRegion.CurrentRoom.Items.Any())
            {
                commands.Add(Take.CommandHelp);
                commands.Add(TakeAll.CommandHelp);
            }

            if (game.Player.CanConverse && game.Overworld.CurrentRegion.CurrentRoom.Characters.Any())
                commands.Add(Talk.TalkCommandHelp);

            if (game.Overworld.CurrentRegion.CurrentRoom.Items.Any() || game.Player.Items.Any())
            {
                commands.Add(UseOn.UseCommandHelp);
                commands.Add(UseOn.OnCommandHelp);
            }

            return [.. commands];
        }

        #endregion
    }
}
