using NetAF.Assets.Locations;

namespace NetAF.Assets
{
    /// <summary>
    /// Provides information on a transition between rooms.
    /// </summary>
    /// <param name="Region">The region the transition happened within.</param>
    /// <param name="Room">The room.</param>
    /// <param name="AdjoiningRoom">The adjoining room.</param>
    /// <param name="Exit">The exit that was used during the transition.</param>
    /// <param name="AdjoiningExit">The adjoining exit being used in the transition.</param>
    /// <param name="Direction">The direction of the transition.</param>
    public sealed record RoomTransition(Region Region, Room Room, Room AdjoiningRoom, Exit Exit, Exit AdjoiningExit, Direction? Direction);
}
