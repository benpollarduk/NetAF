using NetAF.Assets.Characters;
using System;
using System.Linq;

namespace NetAF.Serialization.AssetSerialization
{
    internal class CharacterSerialization(Character asset) : ExaminableSerialization(asset), IAssetSerialization<PlayableCharacter>
    {
        public readonly string[] Items = asset.Items.Select(x => x.Identifier.IdentifiableName).ToArray();
        public readonly bool IsAlive = asset.IsAlive;

        public void Restore(PlayableCharacter asset)
        {
            throw new NotImplementedException();
        }
    }
}
