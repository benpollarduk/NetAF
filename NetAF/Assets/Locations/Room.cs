using System;
using System.Collections.Generic;
using System.Linq;
using NetAF.Assets.Characters;
using NetAF.Commands;
using NetAF.Extensions;
using NetAF.Serialization;
using NetAF.Serialization.Assets;
using NetAF.Utilities;

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
        public Room(string identifier, string description, Exit[] exits = null, Item[] items = null, CustomCommand[] commands = null, InteractionCallback interaction = null, ExaminationCallback examination = null) : this(new Identifier(identifier), new Description(description), Assets.Description.Empty, exits, items, commands, interaction, examination)
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
        public Room(Identifier identifier, IDescription description, Exit[] exits = null, Item[] items = null, CustomCommand[] commands = null, InteractionCallback interaction = null, ExaminationCallback examination = null) : this(identifier, description, Assets.Description.Empty, exits, items, commands, interaction, examination)
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
        public Room(string identifier, string description, string introduction, Exit[] exits = null, Item[] items = null, CustomCommand[] commands = null, InteractionCallback interaction = null, ExaminationCallback examination = null) : this(new Identifier(identifier), new Description(description), new Description(introduction), exits, items, commands, interaction, examination)
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
        public Room(Identifier identifier, IDescription description, IDescription introduction, Exit[] exits = null, Item[] items = null, CustomCommand[] commands = null, InteractionCallback interaction = null, ExaminationCallback examination = null)
        {
            Identifier = identifier;
            Description = description;
            Introduction = introduction;
            Exits = exits ?? [];
            Items = items ?? [];
            Commands = commands ?? [];
            Interaction = interaction ?? (i => new(InteractionResult.NoChange, i));
            Examination = examination ?? DefaultRoomExamination;
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
        /// Get if this Room contains an item. This will not include items whose ExaminableObject.IsPlayerVisible property is set to false.
        /// </summary>
        /// <param name="item">The item to check for.</param>
        /// <returns>True if the item is in this room, else false.</returns>
        public bool ContainsItem(Item item)
        {
            return Items.Contains(item);
        }

        /// <summary>
        /// Get if this Room contains an item.
        /// </summary>
        /// <param name="itemName">The item name to check for.</param>
        /// <param name="includeInvisibleItems">Specify if invisible items should be included.</param>
        /// <returns>True if the item is in this room, else false.</returns>
        public bool ContainsItem(string itemName, bool includeInvisibleItems = false)
        {
            return Array.Exists(Items, item => itemName.EqualsExaminable(item) && (includeInvisibleItems || item.IsPlayerVisible));
        }

        /// <summary>
        /// Get if this Room contains an interaction target.
        /// </summary>
        /// <param name="targetName">The name of the target to check for.</param>
        /// <returns>True if the target is in this room, else false.</returns>
        public bool ContainsInteractionTarget(string targetName)
        {
            return Array.Exists(Items, i => targetName.EqualsExaminable(i) || Array.Exists(Characters, targetName.EqualsExaminable));
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
        /// Find an interaction target.
        /// </summary>
        /// <param name="targetName">The targets name.</param>
        /// <param name="target">The target.</param>
        /// <returns>True if the target was found.</returns>
        public bool FindInteractionTarget(string targetName, out IInteractWithItem target)
        {
            var items = Items.Where(targetName.EqualsExaminable).ToArray();
            var nPCS = Characters.Where(targetName.EqualsExaminable).ToArray();
            var exits = Exits.Where(targetName.EqualsExaminable).ToArray();
            List<IInteractWithItem> interactions = [];

            if (items.Length > 0)
                interactions.AddRange(items);

            if (nPCS.Length > 0)
                interactions.AddRange(nPCS);

            if (exits.Length > 0)
                interactions.AddRange(exits);

            if (interactions.Count > 0)
            {
                target = interactions[0];
                return true;
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
        /// Get if this Room contains a character.
        /// </summary>
        /// <param name="characterName">The character name to check for.</param>
        /// <param name="includeInvisibleCharacters">Specify if invisible characters should be included.</param>
        /// <returns>True if the item is in this room, else false.</returns>
        public bool ContainsCharacter(string characterName, bool includeInvisibleCharacters = false)
        {
            return Array.Exists(Characters, character => characterName.EqualsExaminable(character) && (includeInvisibleCharacters || character.IsPlayerVisible));
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
        public void MovedInto()
        {
            HasBeenVisited = true;
        }

        /// <summary>
        /// Handle movement into this room.
        /// </summary>
        /// <param name="fromDirection">The direction movement into this room.</param>
        public void MovedInto(Direction fromDirection)
        {
            EnteredFrom = fromDirection;
            MovedInto();
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

            if (!Array.Exists(room.Items, i => i.IsPlayerVisible))
                return new("There is nothing to examine.");

            if (room.Items.Count(i => i.IsPlayerVisible) == 1)
            {
                Item singularItem = room.Items.Where(i => i.IsPlayerVisible).ToArray()[0];
                return new($"There {(singularItem.Identifier.Name.IsPlural() ? "are" : "is")} {singularItem.Identifier.Name.GetObjectifier()} {singularItem.Identifier}");
            }

            var items = room.Items.Cast<IExaminable>().ToArray();
            var sentence = StringUtilities.ConstructExaminablesAsSentence(items);
            var firstItemName = sentence.Substring(0, sentence.Contains(", ") ? sentence.IndexOf(", ", StringComparison.Ordinal) : sentence.IndexOf(" and ", StringComparison.Ordinal));
            return new($"There {(firstItemName.IsPlural() ? "are" : "is")} {sentence.StartWithLower()}");
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
                exitSerialization?.Restore(exit);
            }

            foreach (var item in Items)
            {
                var itemSerialization = Array.Find(serialization.Items, x => item.Identifier.Equals(x.Identifier));
                itemSerialization?.Restore(item);
            }

            foreach (var character in Characters)
            {
                var characterSerialization = Array.Find(serialization.Characters, x => character.Identifier.Equals(x.Identifier));
                characterSerialization?.Restore(character);
            }
        }

        #endregion
    }
}
