using NetAF.Assets.Characters;
using NetAF.Assets.Locations;
using NetAF.Assets;
using System.Collections.Generic;
using System.Linq;
using NetAF.Utilities;

namespace NetAF.Logic
{
    /// <summary>
    /// Provides a catalog of all assets in a game.
    /// </summary>
    public class AssetCatalog
    {
        #region Properties

        /// <summary>
        /// Get the items.
        /// </summary>
        public Item[] Items { get; private set; }

        /// <summary>
        /// Get the rooms.
        /// </summary>
        public Room[] Rooms { get; private set; }

        /// <summary>
        /// Get the characters.
        /// </summary>
        public NonPlayableCharacter[] Characters { get; private set; }

        /// <summary>
        /// Get the item containers.
        /// </summary>
        public IItemContainer[] ItemContainers { get; private set; }

        /// <summary>
        /// Get the examinables.
        /// </summary>
        public IExaminable[] Examinables { get; private set; }

        #endregion

        #region Constructors

        private AssetCatalog() { }

        #endregion

        #region Methods

        /// <summary>
        /// Registers a collection of examinables.
        /// </summary>
        /// <param name="templates">The templates to register.</param>
        public void Register(params IAssetTemplate<IExaminable>[] templates)
        {
            Register(templates.Select(x => x.Instantiate()).ToArray());
        }

        /// <summary>
        /// Registers a collection of examinables.
        /// </summary>
        /// <param name="examinables">The examinables to register.</param>
        public void Register(params IExaminable[] examinables)
        {
            Rooms = Register(Rooms, examinables.Where(x => x is Room room && !Rooms.Contains(room)).Cast<Room>().ToArray());
            Items = Register(Items, examinables.Where(x => x is Item item && !Items.Contains(item)).Cast<Item>().ToArray());
            Characters = Register(Characters, examinables.Where(x => x is NonPlayableCharacter character && !Characters.Contains(character)).Cast<NonPlayableCharacter>().ToArray());
            ItemContainers = Register(ItemContainers, examinables.Where(x => x is IItemContainer container && !ItemContainers.Contains(container)).Cast<IItemContainer>().ToArray());
            Examinables = Register(Examinables, examinables.Where(x => !Examinables.Contains(x)).ToArray());
        }

        #endregion

        #region StaticMethods

        /// <summary>
        /// Register all elements in a collection.
        /// </summary>
        /// <typeparam name="T">The type of element to register.</typeparam>
        /// <param name="collection">The collection to register the element in.</param>
        /// <param name="elements">The elements to register in the collection.</param>
        /// <returns>The original collection, including the extra elements.</returns>
        private static T[] Register<T>(T[] collection, T[] elements)
        {
            if (elements.Length == 0)
                return collection;

            var list = collection.ToList();

            foreach (var element in elements)
                list.Add(element);

            return [.. list];
        }

        /// <summary>
        /// Create a new AssetCatalog from a game.
        /// </summary>
        /// <param name="game">The game to create the catalog from.</param>
        /// <returns>The populate asset catalog.</returns>
        public static AssetCatalog FromGame(Game game)
        {
            AssetCatalog catalog = new()
            {
                ItemContainers = GetAllItemContainers(game)
            };

            catalog.Items = GetAllItems(catalog.ItemContainers);
            catalog.Rooms = GetAllRooms(game);
            catalog.Characters = GetAllCharacters(catalog.Rooms);

            List<IExaminable> all = [];
            all.AddRange(catalog.Items);
            all.AddRange(catalog.ItemContainers);
            all.AddRange(catalog.Rooms);
            all.AddRange(catalog.Characters);
            all.Add(game.Player);

            catalog.Examinables = all.Distinct().ToArray();

            return catalog;
        }

        /// <summary>
        /// Get all rooms in a game.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <returns>An array containing all rooms.</returns>
        private static Room[] GetAllRooms(Game game)
        {
            if (game.Overworld?.Regions == null)
                return [];

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

            return [.. characters];
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

            if (game.Player != null)
                itemContainers.Add(game.Player);

            var rooms = GetAllRooms(game);

            foreach (var room in rooms)
                itemContainers.Add(room);

            itemContainers.AddRange(GetAllCharacters(rooms));

            return [.. itemContainers];
        }


        #endregion
    }
}
