namespace NetAF.Serialization.AssetSerialization
{
    internal interface IAssetSerialization<in T>
    {
        public void Restore(T asset);
    }
}
