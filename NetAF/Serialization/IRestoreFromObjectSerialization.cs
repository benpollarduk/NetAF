namespace NetAF.Serialization
{
    /// <summary>
    /// Represents any object that can restore from an IObjectSerialization.
    /// </summary>
    /// <typeparam name="T">The type of serialization that this object restores from.</typeparam>
    internal interface IRestoreFromObjectSerialization<in T>
    {
        /// <summary>
        /// Restore this object from a serialization.
        /// </summary>
        /// <param name="serialization">The serialization to restore from.</param>
        void RestoreFrom(T serialization);
    }
}
