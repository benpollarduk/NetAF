namespace NetAF.Logic.Arrangement
{
    /// <summary>
    /// Provides a mapping between an object and its container.
    /// </summary>
    /// <param name="obj">The obj.</param>
    /// <param name="container">The objects container.</param>
    internal class ObjectToContainerMapping(string obj, string container)
    {
        #region Properties

        /// <summary>
        /// Get the object.
        /// </summary>
        public string Obj { get; private set; } = obj;

        /// <summary>
        /// Get the container.
        /// </summary>
        public string Container { get; private set; } = container;

        #endregion
    }
}
