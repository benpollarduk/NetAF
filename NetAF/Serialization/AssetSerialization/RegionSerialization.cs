using NetAF.Assets.Locations;
using System;
using System.Linq;

namespace NetAF.Serialization.AssetSerialization
{
    internal class RegionSerialization(Region asset) : ExaminableSerialization(asset), IAssetSerialization<Region>
    {
        public readonly RoomSerialization[] Rooms = asset.ToMatrix().ToRooms().Select(x => new RoomSerialization(x)).ToArray();

        public void Restore(Region asset)
        {
            throw new NotImplementedException();
        }
    }
}
