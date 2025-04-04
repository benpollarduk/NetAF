﻿using NetAF.Assets.Locations;
using NetAF.Example.Assets.Regions.Zelda.Items;
using NetAF.Utilities;

namespace NetAF.Example.Assets.Regions.Zelda.Rooms
{
    public class LinksHouse : IAssetTemplate<Room>
    {
        #region Constants

        private const string Name = "Links House";
        private const string Description = "You are in your house, as it is in the hollow trunk of the tree the room is small and round, and very wooden. There is a small table in the center of the room. The front door leads to the Kokiri forest to the north.";

        #endregion

        #region Implementation of IAssetTemplate<Room>

        /// <summary>
        /// Instantiate a new instance of the asset.
        /// </summary>
        /// <returns>The asset.</returns>
        public Room Instantiate()
        {
            var room = new Room(Name, Description, [new Exit(Direction.North)]);

            room.AddItem(new Shield().Instantiate());
            room.AddItem(new Table().Instantiate());
            room.AddItem(new Sword().Instantiate());
            room.AddItem(new YoshiDoll().Instantiate());

            return room;
        }

        #endregion
    }
}
