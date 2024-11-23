using NetAF.Assets.Locations;
using System.Linq;

namespace NetAF.Serialization.Assets
{
    /// <summary>
    /// Represents a serialization of a Region.
    /// </summary>
    public sealed class RegionSerialization : ExaminableSerialization, IObjectSerialization<Region>
    {
        #region Properties

        /// <summary>
        /// Get or set the room serializations.
        /// </summary>
        public RoomSerialization[] Rooms { get; set; }

        /// <summary>
        /// Get or set the current room.
        /// </summary>
        public string CurrentRoom { get; set; }

        /// <summary>
        /// Get or set if the region is visible without discovery.
        /// </summary>
        public bool IsVisibleWithoutDiscovery { get; set; }

        #endregion

        #region StaticMethods

        /// <summary>
        /// Create a new serialization from a Region.
        /// </summary>
        /// <param name="region">The Region to create the serialization from.</param>
        /// <returns>The serialization.</returns>
        public static RegionSerialization FromRegion(Region region)
        {
            return new()
            {
                Identifier = region?.Identifier?.IdentifiableName,
                IsPlayerVisible = region?.IsPlayerVisible ?? false,
                AttributeManager = AttributeManagerSerialization.FromAttributeManager(region?.Attributes),
                Commands = region?.Commands?.Select(CustomCommandSerialization.FromCustomCommand).ToArray() ?? [],
                Rooms = region?.ToMatrix()?.ToRooms()?.Select(RoomSerialization.FromRoom).ToArray() ?? [],
                CurrentRoom = region?.CurrentRoom?.Identifier?.IdentifiableName,
                IsVisibleWithoutDiscovery = region?.IsVisibleWithoutDiscovery ?? false
            };
        }

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
