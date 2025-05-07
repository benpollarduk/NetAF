using NetAF.Assets.Locations;

namespace NetAF.Assets
{
    /// <summary>
    /// Represents the callback for handling transitioning between rooms.
    /// </summary>
    /// <param name="room">The room.</param>
    /// <param name="direction">The direction of the transition.</param>
    public delegate void RoomTransitionCallback(Room room, Direction? direction);
}