using NetAF.Assets.Locations;
using System.Linq;

namespace NetAF.Serialization.Assets
{
    /// <summary>
    /// Represents a serialization of a Region.
    /// </summary>
    /// <param name="region">The region to serialize.</param>
    public sealed class RegionSerialization(Region region) : ExaminableSerialization(region), IObjectSerialization<Region>
    {
        #region Properties

        /// <summary>
        /// Get or set the room serializations.
        /// </summary>
        public RoomSerialization[] Rooms { get; set; } = region?.ToMatrix()?.ToRooms()?.Select(x => new RoomSerialization(x))?.ToArray() ?? [];

        /// <summary>
        /// Get or set the current room.
        /// </summary>
        public string CurrentRoom { get; set; } = region?.CurrentRoom?.Identifier?.IdentifiableName;

        /// <summary>
        /// Get or set if the region is visible without discovery.
        /// </summary>
        public bool IsVisibleWithoutDiscovery { get; set; } = region?.IsVisibleWithoutDiscovery ?? false;

        #endregion

        #region Implementation of IObjectSerialization<Region>

        /// <summary>
        /// Restore an instance from this serialization.
        /// </summary>
        /// <param name="region">The region to restore.</param>
        public void Restore(Region region)
        {
            region.RestoreFrom(this);
        }

        #endregion
    }
}
