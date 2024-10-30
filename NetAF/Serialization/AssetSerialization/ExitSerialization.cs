using NetAF.Assets.Locations;
using System;

namespace NetAF.Serialization.AssetSerialization
{
    internal class ExitSerialization(Exit asset) : ExaminableSerialization(asset), IAssetSerialization<Exit>
    {
        public readonly bool IsLocked = asset.IsLocked;

        public void Restore(Exit asset)
        {
            throw new NotImplementedException();
        }
    }
}
