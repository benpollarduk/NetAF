namespace NetAF.Serialization
{
    /// <summary>
    /// Represents any object that is a serialization of another object.
    /// </summary>
    /// <typeparam name="T">The type of object that this serialization represents.</typeparam>
    internal interface IObjectSerialization<in T>
    {
        /// <summary>
        /// Restore an instance from this serialization.
        /// </summary>
        /// <param name="obj">The obj to restore.</param>
        void Restore(T obj);
    }
}
