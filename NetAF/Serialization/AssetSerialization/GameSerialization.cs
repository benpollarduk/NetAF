using NetAF.Logic;
using System;

namespace NetAF.Serialization.AssetSerialization
{
    internal class GameSerialization(Game asset) : IAssetSerialization<Game>
    {
        public readonly CharacterSerialization PlayerSerialization = new(asset.Player);
        public readonly OverworldSerialization OverworldSerialization = new(asset.Overworld);

        public void Restore(Game asset)
        {
            throw new NotImplementedException();
        }
    }
}
