using NetAF.Assets;
using NetAF.Assets.Locations;

namespace NetAF.Rendering.FrameBuilders
{
    /// <summary>
    /// Represents any object that can build room maps.
    /// </summary>
    public interface IRoomMapBuilder
    {
        /// <summary>
        /// Get the rendered size of the room, excluding any keys.
        /// </summary>
        Size RenderedSize { get; }
        /// <summary>
        /// Build a map for a room.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <param name="viewPoint">The viewpoint from the room.</param>
        /// <param name="options">The render options to use.</param>
        void BuildRoomMap(Room room, ViewPoint viewPoint, RoomMapRenderOptions options);
    }
}
