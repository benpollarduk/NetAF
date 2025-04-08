using NetAF.Information;
using System.Collections.Generic;
using System.Linq;

namespace NetAF.Serialization.Assets
{
    /// <summary>
    /// Represents a serialization of an InformationManager.
    /// </summary>
    public sealed class InformationManagerSerialization : IObjectSerialization<InformationManager>
    {
        #region Properties

        /// <summary>
        /// Get or set the entries.
        /// </summary>
        public List<InformationEntrySerialization> Entries { get; set; }

        #endregion

        #region StaticMethods

        /// <summary>
        /// Create a new serialization from an InformationManager.
        /// </summary>
        /// <param name="informationManager">The InformationManager to create the serialization from.</param>
        /// <returns>The serialization.</returns>
        public static InformationManagerSerialization FromInformationManager(InformationManager informationManager)
        {
            return new()
            {
                Entries = informationManager.GetAll()?.Select(InformationEntrySerialization.FromInformationEntry).ToList() ?? []
            };
        }

        #endregion

        #region Implementation of IObjectSerialization<InformationManager>

        /// <summary>
        /// Restore an instance from this serialization.
        /// </summary>
        /// <param name="informationManager">The information manager to restore.</param>
        void IObjectSerialization<InformationManager>.Restore(InformationManager informationManager)
        {
            ((IRestoreFromObjectSerialization<InformationManagerSerialization>)informationManager).RestoreFrom(this);
        }

        #endregion
    }
}
