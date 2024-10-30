using NetAF.Assets.Locations;
using System.Linq;

namespace NetAF.Serialization.Assets
{
    /// <summary>
    /// Represents a serialization of an Overworld.
    /// </summary>
    /// <param name="overworld">The overworld to serialize.</param>
    public class OverworldSerialization(Overworld overworld) : ExaminableSerialization(overworld), IObjectSerialization<Overworld>
    {
        #region Properties

        /// <summary>
        /// Get the region serializations.
        /// </summary>
        public readonly RegionSerialization[] Regions = overworld?.Regions?.Select(x => new RegionSerialization(x))?.ToArray() ?? [];

        /// <summary>
        /// Get the current region.
        /// </summary>
        public readonly string CurrentRegion = overworld?.CurrentRegion?.Identifier?.IdentifiableName;

        #endregion

        #region Implementation of IObjectSerialization<Overworld>

        /// <summary>
        /// Restore an instance from this serialization.
        /// </summary>
        /// <param name="overworld">The overworld to restore.</param>
        public void Restore(Overworld overworld)
        {
            overworld.RestoreFrom(this);
        }

        #endregion
    }
}
