﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetAF.Assets.Characters;
using NetAF.Commands;
using NetAF.Extensions;
using NetAF.Interpretation;
using NetAF.Rendering;
using NetAF.Serialization;
using NetAF.Serialization.Assets;

namespace NetAF.Assets.Locations
{
    /// <summary>
    /// Represents a room.
    /// </summary>
    public sealed class Room : ExaminableObject, IInteractWithItem, IItemContainer, IRestoreFromObjectSerialization<RoomSerialization>
    {
        #region StaticProperties

        /// <summary>
        /// Get the default examination for a Room.
        /// </summary>
        public static ExaminationCallback DefaultRoomExamination => ExamineThis;

        #endregion

        #region Properties

        /// <summary>
        /// Get if this location has been visited.
        /// </summary>
        public bool HasBeenVisited { get; private set; }

        /// <summary>
        /// Get the exits.
        /// </summary>
        public Exit[] Exits { get; private set; }

        /// <summary>
        /// Get all unlocked exits.
        /// </summary>
        public Exit[] UnlockedExits => GetUnlockedExits();

        /// <summary>
        /// Get the characters in this Room.
        /// </summary>
        public NonPlayableCharacter[] Characters { get; private set; } = [];

        /// <summary>
        /// Get the interaction.
        /// </summary>
        public InteractionCallback Interaction { get; private set; }

        /// <summary>
        /// Get an exit.
        /// </summary>
        /// <param name="direction">The direction of an exit.</param>
        /// <returns>The exit.</returns>
        public Exit this[Direction direction] => Array.Find(Exits, e => e.Direction == direction);

        /// <summary>
        /// Get which direction this room was entered from.
        /// </summary>
        public Direction? EnteredFrom { get; private set; }

        /// <summary>
        /// Get an introduction for this room.
        /// </summary>
        public IDescription Introduction { get; private set; }

        /// <summary>
        /// Get the callback to invoke when this room is entered.
        /// </summary>
        public RoomTransitionCallback EnterCallback { get; private set; }

        /// <summary>
        /// Get the callback to invoke when this room is exited.
        /// </summary>
        public RoomTransitionCallback ExitCallback { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Room class.
        /// </summary>
        /// <param name="identifier">This rooms identifier.</param>
        /// <param name="description">This rooms description.</param>
        /// <param name="exits">The exits from this room.</param>
        /// <param name="items">The items in this room.</param>
        /// <param name="commands">This objects commands.</param>
        /// <param name="interaction">The interaction.</param>
        /// <param name="examination">The examination.</param>
        /// <param name="enterCallback">The callback to invoke when this room is entered.</param>
        /// <param name="exitCallback">The callback to invoke when this room is exited.</param>
        public Room(string identifier, string description, Exit[] exits = null, Item[] items = null, CustomCommand[] commands = null, InteractionCallback interaction = null, ExaminationCallback examination = null, RoomTransitionCallback enterCallback = null, RoomTransitionCallback exitCallback = null) : this(new Identifier(identifier), new Description(description), Assets.Description.Empty, exits, items, commands, interaction, examination, enterCallback, exitCallback)
        {
        }

        /// <summary>
        /// Initializes a new instance of the Room class.
        /// </summary>
        /// <param name="identifier">This rooms identifier.</param>
        /// <param name="description">This rooms description.</param>
        /// <param name="exits">The exits from this room.</param>
        /// <param name="items">The items in this room.</param>
        /// <param name="commands">This objects commands.</param>
        /// <param name="interaction">The interaction.</param>
        /// <param name="examination">The examination.</param>
        /// <param name="enterCallback">The callback to invoke when this room is entered.</param>
        /// <param name="exitCallback">The callback to invoke when this room is exited.</param>
        public Room(Identifier identifier, IDescription description, Exit[] exits = null, Item[] items = null, CustomCommand[] commands = null, InteractionCallback interaction = null, ExaminationCallback examination = null, RoomTransitionCallback enterCallback = null, RoomTransitionCallback exitCallback = null) : this(identifier, description, Assets.Description.Empty, exits, items, commands, interaction, examination, enterCallback, exitCallback)
        {
        }

        /// <summary>
        /// Initializes a new instance of the Room class.
        /// </summary>
        /// <param name="identifier">This rooms identifier.</param>
        /// <param name="description">This rooms description.</param>
        /// <param name="introduction">An introduction to this room.</param>
        /// <param name="exits">The exits from this room.</param>
        /// <param name="items">The items in this room.</param>
        /// <param name="commands">This objects commands.</param>
        /// <param name="interaction">The interaction.</param>
        /// <param name="examination">The examination.</param>
        /// <param name="enterCallback">The callback to invoke when this room is entered.</param>
        /// <param name="exitCallback">The callback to invoke when this room is exited.</param>
        public Room(string identifier, string description, string introduction, Exit[] exits = null, Item[] items = null, CustomCommand[] commands = null, InteractionCallback interaction = null, ExaminationCallback examination = null, RoomTransitionCallback enterCallback = null, RoomTransitionCallback exitCallback = null) : this(new Identifier(identifier), new Description(description), new Description(introduction), exits, items, commands, interaction, examination, enterCallback, exitCallback)
        {
        }

        /// <summary>
        /// Initializes a new instance of the Room class.
        /// </summary>
        /// <param name="identifier">This rooms identifier.</param>
        /// <param name="description">This rooms description.</param>
        /// <param name="introduction">An introduction to this room.</param>
        /// <param name="exits">The exits from this room.</param>
        /// <param name="items">The items in this room.</param>
        /// <param name="commands">This objects commands.</param>
        /// <param name="interaction">The interaction.</param>
        /// <param name="examination">The examination.</param>
        /// <param name="enterCallback">The callback to invoke when this room is entered.</param>
        /// <param name="exitCallback">The callback to invoke when this room is exited.</param>
        public Room(Identifier identifier, IDescription description, IDescription introduction, Exit[] exits = null, Item[] items = null, CustomCommand[] commands = null, InteractionCallback interaction = null, ExaminationCallback examination = null, RoomTransitionCallback enterCallback = null, RoomTransitionCallback exitCallback = null)
        {
            Identifier = identifier;
            Description = description;
            Introduction = introduction;
            Exits = exits ?? [];
            Items = items ?? [];
            Commands = commands ?? [];
            Interaction = interaction ?? (i => new(InteractionResult.NoChange, i));
            Examination = examination ?? DefaultRoomExamination;
            EnterCallback = enterCallback;
            ExitCallback = exitCallback;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get unlocked exits.
        /// </summary>
        /// <returns>All unlocked exits.</returns>
        private Exit[] GetUnlockedExits()
        {
            return Exits.Where(x => !x.IsLocked).ToArray();
        }

        /// <summary>
        /// Add a character to this room.
        /// </summary>
        /// <param name="character">The character to add.</param>
        public void AddCharacter(NonPlayableCharacter character)
        {
            Characters = Characters.Add(character);
        }

        /// <summary>
        /// Remove a character from the room.
        /// </summary>
        /// <param name="character">The character to remove.</param>
        public void RemoveCharacter(NonPlayableCharacter character)
        {
            Characters = Characters.Remove(character);
        }

        /// <summary>
        /// Remove an interaction target from the room.
        /// </summary>
        /// <param name="target">The target to remove.</param>
        /// <returns>The target removed from this room.</returns>
        public IInteractWithItem RemoveInteractionTarget(IInteractWithItem target)
        {
            if (Items.Contains(target))
            {
                RemoveItem(target as Item);
                return target;
            }

            if (Characters.Contains(target))
            {
                RemoveCharacter(target as NonPlayableCharacter);
                return target;
            }

            return null;
        }

        /// <summary>
        /// Test if a move is possible.
        /// </summary>
        /// <param name="direction">The direction to test.</param>
        /// <returns>If a move in the specified direction is possible.</returns>
        public bool CanMove(Direction direction)
        {
            return Array.Exists(UnlockedExits, x => x.Direction == direction);
        }

        /// <summary>
        /// Interact with a specified item.
        /// </summary>
        /// <param name="item">The item to interact with.</param>
        /// <returns>The interaction.</returns>
        private Interaction InteractWithItem(Item item)
        {
            return Interaction.Invoke(item);
        }

        /// <summary>
        /// Get if this room has a visible locked exit in a specified direction.
        /// </summary>
        /// <param name="direction">The direction to check.</param>
        /// <param name="includeInvisibleExits">Specify if invisible exits should be included.</param>
        /// <returns>If there is a locked exit in the specified direction.</returns>
        public bool HasLockedExitInDirection(Direction direction, bool includeInvisibleExits = false)
        {
            return Array.Exists(Exits, x => x.Direction == direction && x.IsLocked && (includeInvisibleExits || x.IsPlayerVisible));
        }

        /// <summary>
        /// Get if this room has a visible unlocked exit in a specified direction.
        /// </summary>
        /// <param name="direction">The direction to check.</param>
        /// <param name="includeInvisibleExits">Specify if invisible exits should be included.</param>
        /// <returns>If there is a unlocked exit in the specified direction.</returns>
        public bool HasUnlockedExitInDirection(Direction direction, bool includeInvisibleExits = false)
        {
            return Array.Exists(Exits, x => x.Direction == direction && !x.IsLocked && (includeInvisibleExits || x.IsPlayerVisible));
        }

        /// <summary>
        /// Get if this Room contains an exit.
        /// </summary>
        /// <param name="includeInvisibleExits">Specify if invisible exits should be included.</param>
        /// <returns>True if the exit exists, else false.</returns>
        public bool ContainsExit(bool includeInvisibleExits = false)
        {
            return Array.Exists(Exits, exit => includeInvisibleExits || exit.IsPlayerVisible);
        }

        /// <summary>
        /// Get if this Room contains an exit.
        /// </summary>
        /// <param name="direction">The direction of the exit to check for.</param>
        /// <param name="includeInvisibleExits">Specify if invisible exits should be included.</param>
        /// <returns>True if the exit exists, else false.</returns>
        public bool ContainsExit(Direction direction, bool includeInvisibleExits = false)
        {
            return Array.Exists(Exits, exit => exit.Direction == direction && (includeInvisibleExits || exit.IsPlayerVisible));
        }

        /// <summary>
        /// Find an exit.
        /// </summary>
        /// <param name="direction">The exits direction.</param>
        /// <param name="includeInvisibleExits">Specify if invisible exists should be included.</param>
        /// <param name="exit">The exit.</param>
        /// <returns>True if the exit was found.</returns>
        public bool FindExit(Direction direction, bool includeInvisibleExits, out Exit exit)
        {
            var exits = Exits.Where(x => x.Direction == direction && (includeInvisibleExits || x.IsPlayerVisible)).ToArray();

            if (exits.Length > 0)
            {
                exit = exits[0];
                return true;
            }

            exit = null;
            return false;
        }

        /// <summary>
        /// Get if this Room contains an item.
        /// </summary>
        /// <param name="item">The item to check for.</param>
        /// <param name="includeInvisibleItems">Specify if invisible exits should be included.</param>
        /// <returns>True if the item is in this room, else false.</returns>
        public bool ContainsItem(Item item, bool includeInvisibleItems = false)
        {
            
            return Array.Exists(Items, i => i == item && (includeInvisibleItems || i.IsPlayerVisible));
        }

        /// <summary>
        /// Find an item. This will not include items whose ExaminableObject.IsPlayerVisible property is set to false
        /// </summary>
        /// <param name="itemName">The items name. This is case insensitive</param>
        /// <param name="item">The item</param>
        /// <returns>True if the item was found</returns>
        public bool FindItem(string itemName, out Item item)
        {
            return FindItem(itemName, out item, false);
        }

        /// <summary>
        /// Find an item.
        /// </summary>
        /// <param name="itemName">The items name.</param>
        /// <param name="item">The item.</param>
        /// <param name="includeInvisibleItems">Specify is invisible items should be included.</param>
        /// <returns>True if the item was found.</returns>
        public bool FindItem(string itemName, out Item item, bool includeInvisibleItems)
        {
            var items = Items.Where(x => itemName.EqualsExaminable(x) && (includeInvisibleItems || x.IsPlayerVisible)).ToArray();

            if (items.Length > 0)
            {
                item = items[0];
                return true;
            }

            item = null;
            return false;
        }

        /// <summary>
        /// Get all interaction targets for this room.
        /// </summary>
        /// <returns>An array containing all interaction targets.</returns>
        public IInteractWithItem[] GetAllInteractionTargets()
        {
            var all = new List<IInteractWithItem>();
            all.AddRange(Items?.Where(x => x.IsPlayerVisible) ?? []);
            all.AddRange(Characters?.Where(x => x.IsPlayerVisible) ?? []);
            all.AddRange(Exits?.Where(x => x.IsPlayerVisible) ?? []);
            return [.. all];
        }

        /// <summary>
        /// Find an interaction target.
        /// </summary>
        /// <param name="targetName">The targets name.</param>
        /// <param name="target">The target.</param>
        /// <returns>True if the target was found.</returns>
        public bool FindInteractionTarget(string targetName, out IInteractWithItem target)
        {
            var items = Items.Where(targetName.EqualsExaminable).ToArray();

            if (items.Length > 0)
            {
                target = items[0];
                return true;
            }

            var nPCS = Characters.Where(targetName.EqualsExaminable).ToArray();

            if (nPCS.Length > 0)
            {
                target = nPCS[0];
                return true;
            }

            var exits = Exits.Where(targetName.EqualsExaminable).ToArray();

            if (exits.Length > 0)
            {
                target = exits[0];
                return true;
            }

            if (SceneCommandInterpreter.TryParseToDirection(targetName, out var direction))
            {
                exits = Exits.Where(x => x.Direction == direction).ToArray();

                if (exits.Length > 0)
                {
                    target = exits[0];
                    return true;
                }
            }

            target = null;
            return false;
        }

        /// <summary>
        /// Get if this Room contains a character.
        /// </summary>
        /// <param name="character">The character.</param>
        /// <param name="includeInvisibleCharacters">Specify if invisible characters should be included.</param>
        /// <returns>True if the item is in this room, else false.</returns>
        public bool ContainsCharacter(NonPlayableCharacter character, bool includeInvisibleCharacters = false)
        {
            return Characters.Contains(character) && (includeInvisibleCharacters || character.IsPlayerVisible);
        }

        /// <summary>
        /// Find a character. This will not include characters whose ExaminableObject.IsPlayerVisible property is set to false.
        /// </summary>
        /// <param name="character">The character name.</param>
        /// <param name="characterName">The character.</param>
        /// <returns>True if the character was found.</returns>
        public bool FindCharacter(string characterName, out NonPlayableCharacter character)
        {
            return FindCharacter(characterName, out character, false);
        }

        /// <summary>
        /// Find a character.
        /// </summary>
        /// <param name="characterName">The character name.</param>
        /// <param name="character">The character.</param>
        /// <param name="includeInvisibleCharacters">Specify if invisible characters should be included.</param>
        /// <returns>True if the character was found.</returns>
        public bool FindCharacter(string characterName, out NonPlayableCharacter character, bool includeInvisibleCharacters)
        {
            var characters = Characters.Where(x => characterName.EqualsExaminable(x) && (includeInvisibleCharacters || x.IsPlayerVisible)).ToArray();

            if (characters.Length > 0)
            {
                character = characters[0];
                return true;
            }

            character = null;
            return false;
        }

        /// <summary>
        /// Handle movement into this room.
        /// </summary>
        /// <param name="region">The region the transition happened within.</param>
        /// <param name="adjoiningRoom">The adjoining room.</param>
        /// <param name="direction">The direction of movement into this room.</param>
        /// <returns>The reaction to the transition.</returns>
        internal RoomTransitionReaction MovedInto(Region region, Room adjoiningRoom, Direction? direction = null)
        {
            EnteredFrom = direction;
            HasBeenVisited = true;

            return EnterCallback?.Invoke(GetTransition(region, adjoiningRoom, direction)) ?? RoomTransitionReaction.Silent;
        }

        /// <summary>
        /// Handle movement out of this room.
        /// </summary>
        /// <param name="region">The region the transition happened within.</param>
        /// <param name="adjoiningRoom">The adjoining room.</param>
        /// <param name="direction">The direction of movement out of this room.</param>
        /// <returns>The reaction to the transition.</returns>
        internal RoomTransitionReaction MovedOutOf(Region region, Room adjoiningRoom, Direction? direction = null)
        {
            return ExitCallback?.Invoke(GetTransition(region, adjoiningRoom, direction)) ?? RoomTransitionReaction.Silent;
        }

        /// <summary>
        /// Get a transition between this room and an adjoining room.
        /// </summary>
        /// <param name="region">The region the transition happened within.</param>
        /// <param name="adjoiningRoom">The adjoining room.</param>
        /// <param name="direction">The direction of movement out of this room.</param>
        private RoomTransition GetTransition(Region region, Room adjoiningRoom, Direction? direction = null)
        {
            Exit inExit = null;
            Exit outExit = null;

            if (direction.HasValue)
            {
                FindExit(direction.Value, true, out inExit);
                adjoiningRoom?.FindExit(direction.Value.Inverse(), true, out outExit);
            }

            return new RoomTransition(region, this, adjoiningRoom, inExit, outExit, direction);
        }

        #endregion

        #region StaticMethods

        /// <summary>
        /// Examine this Room.
        /// </summary>
        /// <param name="request">The examination request.</param>
        /// <returns>The examination.</returns>
        private static Examination ExamineThis(ExaminationRequest request)
        {
            if (request.Examinable is not Room room)
                return DefaultExamination(request);

            var visibleItems = room.Items?.Count(x => x.IsPlayerVisible) ?? 0;
            var visibleCharacters = room.Characters?.Count(x => x.IsPlayerVisible) ?? 0;

            if (visibleItems == 0 && visibleCharacters == 0)
                return new($"{room.Identifier.Name} is empty.");

            StringBuilder examinationBuilder = new();

            if (visibleItems > 0)
                examinationBuilder.AppendLine(SceneHelper.CreateItemsString(room));

            if (visibleItems > 0 && visibleCharacters > 0)
                examinationBuilder.AppendLine();

            if (visibleCharacters > 0)
                examinationBuilder.AppendLine(SceneHelper.CreateCharactersString(room));

            return new(examinationBuilder.ToString());
        }

        #endregion

        #region IInteractWithItem Members

        /// <summary>
        /// Interact with an item.
        /// </summary>
        /// <param name="item">The item to interact with.</param>
        /// <returns>The interaction.</returns>
        public Interaction Interact(Item item)
        {
            return InteractWithItem(item);
        }

        #endregion

        #region Implementation of IItemContainer

        /// <summary>
        /// Get the items.
        /// </summary>
        public Item[] Items { get; private set; } = [];

        /// <summary>
        /// Add an item.
        /// </summary>
        /// <param name="item">The item to add.</param>
        public void AddItem(Item item)
        {
            Items = Items.Add(item);
        }

        /// <summary>
        /// Remove an item.
        /// </summary>
        /// <param name="item">The item to remove.</param>
        public void RemoveItem(Item item)
        {
            Items = Items.Remove(item);
        }

        #endregion

        #region Implementation of IRestoreFromObjectSerialization<RoomSerialization>

        /// <summary>
        /// Restore this object from a serialization.
        /// </summary>
        /// <param name="serialization">The serialization to restore from.</param>
        void IRestoreFromObjectSerialization<RoomSerialization>.RestoreFrom(RoomSerialization serialization)
        {
            ((IRestoreFromObjectSerialization<ExaminableSerialization>)this).RestoreFrom(serialization);

            HasBeenVisited = serialization.HasBeenVisited;

            foreach (var exit in Exits)
            {
                var exitSerialization = Array.Find(serialization.Exits, x => exit.Identifier.Equals(x.Identifier));
                ((IObjectSerialization<Exit>)exitSerialization)?.Restore(exit);
            }

            foreach (var item in Items)
            {
                var itemSerialization = Array.Find(serialization.Items, x => item.Identifier.Equals(x.Identifier));
                ((IObjectSerialization<Item>)itemSerialization)?.Restore(item);
            }

            foreach (var character in Characters)
            {
                var characterSerialization = Array.Find(serialization.Characters, x => character.Identifier.Equals(x.Identifier));
                ((IObjectSerialization<NonPlayableCharacter>)characterSerialization)?.Restore(character);
            }
        }

        #endregion
    }
}
