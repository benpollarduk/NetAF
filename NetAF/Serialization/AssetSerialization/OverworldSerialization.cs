using NetAF.Assets.Locations;
using System;
using System.Linq;

namespace NetAF.Serialization.AssetSerialization
{
    internal class OverworldSerialization(Overworld asset) : ExaminableSerialization(asset), IAssetSerialization<Overworld>
    {
        public readonly RegionSerialization[] Regions = asset.Regions.Select(x => new RegionSerialization(x)).ToArray();

        public void Restore(Overworld asset)
        {
            throw new NotImplementedException();
        }
    }
}
