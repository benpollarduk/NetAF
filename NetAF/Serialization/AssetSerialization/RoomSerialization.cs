using NetAF.Assets.Locations;
using System;
using System.Linq;

namespace NetAF.Serialization.AssetSerialization
{
    internal class RoomSerialization(Room asset) : ExaminableSerialization(asset), IAssetSerialization<Room>
    {
        public readonly string[] Items = asset.Items.Select(x => x.Identifier.IdentifiableName).ToArray();
        public readonly bool HasBeenVisited = asset.HasBeenVisited;
        public readonly ExitSerialization[] Exits = asset.Exits.Select(x => new ExitSerialization(x)).ToArray();
        public readonly CharacterSerialization[] CharactersSerialization = asset.Characters.Select(x => new CharacterSerialization(x)).ToArray();

        public void Restore(Room asset)
        {
            throw new NotImplementedException();
        }
    }
}
