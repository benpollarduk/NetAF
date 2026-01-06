using NetAF.Assets.Characters;
using NetAF.Assets.Locations;

namespace NetAF.Logging.Events
{
    /// <summary>
    /// Provides a base event.
    /// </summary>
    public record BaseEvent
    {
    }

    /// <summary>
    /// Provides an event for when a character died.
    /// </summary>
    /// <param name="Character">The character that died.</param>
    public record CharacterDied(Character Character) : BaseEvent;

    /// <summary>
    /// Provides an event for when a room is entered.
    /// </summary>
    /// <param name="Room">The room that was entered.</param>
    public record RoomEntered(Room Room) : BaseEvent;

    /// <summary>
    /// Provides an event for when a room is exited.
    /// </summary>
    /// <param name="Room">The room that was exited.</param>
    public record RoomExited(Room Room) : BaseEvent;

    /// <summary>
    /// Provides an event for when a region is entered.
    /// </summary>
    /// <param name="Region">The region that was entered.</param>
    public record RegionEntered(Region Region) : BaseEvent;

    /// <summary>
    /// Provides an event for when a region is exited.
    /// </summary>
    /// <param name="Region">The region that was exited.</param>
    public record RegionExited(Region Region) : BaseEvent;
}
