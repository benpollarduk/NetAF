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
        /// <summary>
        /// Measure the size required for a room map. The returned size includes the size of any key that will be rendered as well as the map itself.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <param name="viewPoint">The viewpoint from the room.</param>
        /// <param name="options">The render options to use.</param>
        Size Measure(Room room, ViewPoint viewPoint, RoomMapRenderOptions options);
    }
}
