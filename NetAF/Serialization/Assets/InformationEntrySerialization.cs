using NetAF.Information;

namespace NetAF.Serialization.Assets
{
    /// <summary>
    /// Represents a serialization of an InformationEntry.
    /// </summary>
    public sealed class InformationEntrySerialization : IObjectSerialization<InformationEntry>
    {
        #region Properties

        /// <summary>
        /// Get or set the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Get or set the content.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Get or set if this has expired.
        /// </summary>
        public bool HasExpired { get; set; }

        #endregion

        #region StaticMethods

        /// <summary>
        /// Create a new serialization from a InformationEntry.
        /// </summary>
        /// <param name="informationEntry">The InformationEntry to create the serialization from.</param>
        /// <returns>The serialization.</returns>
        public static InformationEntrySerialization FromInformationEntry(InformationEntry informationEntry)
        {
            return new()
            {
                Name = informationEntry.Name,
                Content = informationEntry.Content,
                HasExpired = informationEntry.HasExpired
            };
        }

        #endregion

        #region Implementation of IObjectSerialization<Exit>

        /// <summary>
        /// Restore an instance from this serialization.
        /// </summary>
        /// <param name="informationEntry">The InformationEntry to restore.</param>
        void IObjectSerialization<InformationEntry>.Restore(InformationEntry informationEntry)
        {
            ((IRestoreFromObjectSerialization<InformationEntrySerialization>)informationEntry).RestoreFrom(this);
        }

        #endregion
    }
}
