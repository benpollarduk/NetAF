﻿using NetAF.Assets.Locations;
using NetAF.Utilities;

namespace NetAF.Example.Assets.Regions.Everglades.Rooms
{
    public class Cave : IAssetTemplate<Room>
    {
        #region Constants

        private const string Name = "Cave";
        private const string Description = "The cave is so dark you struggling to see. A screeching noise is audible to the east.";

        #endregion

        #region Implementation of IAssetTemplate<Room>

        /// <summary>
        /// Instantiate a new instance of the asset.
        /// </summary>
        /// <returns>The asset.</returns>
        public Room Instantiate()
        {
            return new(Name, Description, [new Exit(Direction.East), new Exit(Direction.South)]);
        }

        #endregion
    }
}
