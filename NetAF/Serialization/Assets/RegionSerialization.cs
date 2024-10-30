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
        /// Get the room serializations.
        /// </summary>
        public readonly RoomSerialization[] Rooms = region?.ToMatrix()?.ToRooms()?.Select(x => new RoomSerialization(x))?.ToArray() ?? [];

        /// <summary>
        /// Get the current room.
        /// </summary>
        public readonly string CurrentRoom = region?.CurrentRoom?.Identifier?.IdentifiableName;

        #endregion

        #region Implementation of IObjectSerialization<Region>

        /// <summary>
        /// Restore an instance from this serialization.
        /// </summary>
        /// <param name="region">The region to restore.</param>
        public void Restore(Region region)
        {
            //region.RestoreFrom(this);
        }

        #endregion
    }
}
