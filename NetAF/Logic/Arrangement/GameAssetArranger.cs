using NetAF.Assets;
using NetAF.Assets.Characters;
using NetAF.Assets.Locations;
using NetAF.Serialization;
using System;
using System.Collections.Generic;

namespace NetAF.Logic.Arrangement
{
    /// <summary>
    /// Provides functionality to arrange assets within a game to match a serialization.
    /// </summary>
    internal static class GameAssetArranger
    {
        #region StaticMethods

        /// <summary>
        /// Arrange the assets with the game to match the serialization.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <param name="serialization">The serialization.</param>
        public static void Arrange(Game game, GameSerialization serialization)
        {
            // get serialization of game in current state
            GameSerialization currentState = new(game);

            // get all item containers
            IItemContainer[] itemContainers = GetAllItemContainers(game);

            // get all item instances
            Item[] items = GetAllItems(itemContainers);

            // get all rooms
            Room[] rooms = GetAllRooms(game);

            // get all NPC's
            NonPlayableCharacter[] characters = GetAllCharacters(rooms);

            // determine which items have moved
            var itemRecords = DetermineItemMoves(currentState, serialization, itemContainers, items);
            
            // determine which characters have moved
            var characterRecords = DetermineCharacterMoves(currentState, serialization, rooms, characters);

            // move the items to the new locations
            Rearrange(itemRecords);

            // move the characters to the new locations
            Rearrange(characterRecords);
        }

        /// <summary>
        /// Get all rooms in a game.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <returns>An array containing all rooms.</returns>
        private static Room[] GetAllRooms(Game game)
        {
            List<Room> rooms = [];

            foreach (var region in game.Overworld.Regions)
                foreach (var room in region.ToMatrix().ToRooms())
                    rooms.Add(room);

            return [.. rooms];
        }

        /// <summary>
        /// Get all characters in a collection of rooms.
        /// </summary>
        /// <param name="rooms">The rooms.</param>
        /// <returns>An array containing all characters.</returns>
        private static NonPlayableCharacter[] GetAllCharacters(Room[] rooms)
        {
            List<NonPlayableCharacter> characters = [];

            foreach (var room in rooms)
            {
                foreach (var character in room.Characters)
                    characters.Add(character);
            }

            return [..  characters];
        }

        /// <summary>
        /// Get all items in an array of item containers.
        /// </summary>
        /// <param name="itemContainers">The item containers.</param>
        /// <returns>An array containing all items.</returns>
        private static Item[] GetAllItems(IItemContainer[] itemContainers)
        {
            List<Item> items = [];

            foreach (var itemContainer in itemContainers)
                items.AddRange(itemContainer.Items);

            return [.. items];
        }

        /// <summary>
        /// Get all item containers in a game.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <returns>An array containing all item containers.</returns>
        private static IItemContainer[] GetAllItemContainers(Game game)
        {
            List<IItemContainer> itemContainers = [];

            itemContainers.Add(game.Player);

            var rooms = GetAllRooms(game);

            foreach (var room in rooms)
                itemContainers.Add(room);

            itemContainers.AddRange(GetAllCharacters(rooms));

            return [.. itemContainers];
        }

        /// <summary>
        /// Get an array of ObjectToContainerMapping that all items to their containers.
        /// </summary>
        /// <param name="serialization">The serialization to get item container mappings from.</param>
        /// <returns>An array containing all item to container mappings.</returns>
        private static ObjectToContainerMapping[] GetItemsToContainerMappings(GameSerialization serialization)
        {
            List<ObjectToContainerMapping> records = [];

            foreach (var item in serialization.Player.Items)
                records.Add(new(item.Identifier, serialization.Player.Identifier));

            foreach (var region in serialization.Overworld.Regions)
            {
                foreach (var room in region.Rooms)
                {
                    foreach (var item in room.Items)
                        records.Add(new(item.Identifier, room.Identifier));

                    foreach (var character in room.Characters)
                    {
                        foreach (var item in character.Items)
                            records.Add(new(item.Identifier, character.Identifier));
                    }
                }
            }

            return [.. records];
        }

        /// <summary>
        /// Get an array of ObjectToContainerMapping that all characters to their rooms.
        /// </summary>
        /// <param name="serialization">The serialization to get character room mappings from.</param>
        /// <returns>An array containing all character to room mappings.</returns>
        private static ObjectToContainerMapping[] GetCharacterToRoomMappings(GameSerialization serialization)
        {
            List<ObjectToContainerMapping> records = [];

            foreach (var region in serialization.Overworld.Regions)
            {
                foreach (var room in region.Rooms)
                {
                    foreach (var character in room.Characters)
                        records.Add(new(character.Identifier, room.Identifier));
                }
            }

            return [.. records];
        }

        /// <summary>
        /// Determine which items need to move.
        /// </summary>
        /// <param name="currentState">The current state of the game.</param>
        /// <param name="desiredState">The desired state of the game.</param>
        /// <param name="itemContainers">All instances of item containers.</param>
        /// <param name="instances">All instances of items.</param>
        /// <returns>An array detailing the required item moves.</returns>
        private static ItemFromTo[] DetermineItemMoves(GameSerialization currentState, GameSerialization desiredState, IItemContainer[] itemContainers, Item[] instances)
        {
            List<ItemFromTo> itemRecords = [];

            var currentMappings = GetItemsToContainerMappings(currentState);
            var destinationMappings = GetItemsToContainerMappings(desiredState);

            foreach (var mapping in currentMappings)
            {
                // find the matching item
                var match = Array.Find(destinationMappings, x => x.Obj.Equals(mapping.Obj));
                
                // if the containers match, continue
                if (match != null && match.Container.Equals(mapping.Container))
                    continue;

                var item = Array.Find(instances, x => x.Identifier.Equals(mapping.Obj));
                var from = Array.Find(itemContainers, x => x.Identifier.Equals(mapping.Container));
                var to = match != null ? Array.Find(itemContainers, x => x.Identifier.Equals(match.Container)) : null;

                // item needs to be relocated
                itemRecords.Add(new ItemFromTo(item, from, to));
            }

            return [.. itemRecords];
        }

        /// <summary>
        /// Determine which characters need to move.
        /// </summary>
        /// <param name="currentState">The current state of the game.</param>
        /// <param name="desiredState">The desired state of the game.</param>
        /// <param name="rooms">All instances of rooms.</param>
        /// <param name="instances">All instances of characters.</param>
        /// <returns>An array detailing the required character moves.</returns>
        private static NonPlayableCharacterFromTo[] DetermineCharacterMoves(GameSerialization currentState, GameSerialization desiredState, Room[] rooms, NonPlayableCharacter[] instances)
        {
            List<NonPlayableCharacterFromTo> characterRecords = [];

            var currentMappings = GetCharacterToRoomMappings(currentState);
            var destinationMappings = GetCharacterToRoomMappings(desiredState);

            foreach (var mapping in currentMappings)
            {
                // find the matching item
                var match = Array.Find(destinationMappings, x => x.Obj.Equals(mapping.Obj));

                // if the containers match, continue
                if (match != null && match.Container.Equals(mapping.Container))
                    continue;

                var character = Array.Find(instances, x => x.Identifier.Equals(mapping.Obj));
                var from = Array.Find(rooms, x => x.Identifier.Equals(mapping.Container));
                var to = match != null ? Array.Find(rooms, (x) => x.Identifier.Equals(match?.Container)) : null;

                // character needs to be relocated
                characterRecords.Add(new NonPlayableCharacterFromTo(character, from, to));
            }

            return [.. characterRecords];
        }

        /// <summary>
        /// Rearrange items between containers.
        /// </summary>
        /// <param name="records">The records to rearrange.</param>
        private static void Rearrange(ItemFromTo[] records)
        {
            foreach (var record in records)
            {
                record.From?.RemoveItem(record.Item);
                record.To?.AddItem(record.Item);
            }
        }

        /// <summary>
        /// Rearrange characters between rooms.
        /// </summary>
        /// <param name="records">The records to rearrange.</param>
        private static void Rearrange(NonPlayableCharacterFromTo[] records)
        {
            foreach (var record in records)
            {
                record.From?.RemoveCharacter(record.Character);
                record.To?.AddCharacter(record.Character);
            }
        }
    }

    #endregion
}
