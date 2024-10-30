using NetAF.Assets;
using System;
using System.Collections.Generic;

namespace NetAF.Serialization.AssetSerialization
{
    internal abstract class ExaminableSerialization : IAssetSerialization<IExaminable>
    {
        public readonly string Identifier;
        public readonly bool IsPlayerVisible;
        public readonly Dictionary<string, int> Attributes = [];

        protected ExaminableSerialization(IExaminable asset)
        {
            Identifier = asset.Identifier.IdentifiableName;
            IsPlayerVisible = asset.IsPlayerVisible;

            foreach (var attribute in asset.Attributes.GetAsDictionary())
                Attributes.Add(attribute.Key.Name, attribute.Value);
        }

        public virtual void Restore(IExaminable asset)
        {
            throw new NotImplementedException();
        }
    }
}
