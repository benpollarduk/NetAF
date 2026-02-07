using NetAF.Assets;
using NetAF.Assets.Characters;
using NetAF.Assets.Locations;
using NetAF.Logic;
using NetAF.Logic.Modes;

namespace NetAF.Events
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

    /// <summary>
    /// Provides an event for when a character received an item.
    /// </summary>
    /// <param name="Character">The character that received the item.</param>
    /// <param name="Item">The item.</param>
    public record ItemReceived(Character Character, Item Item) : BaseEvent;

    /// <summary>
    /// Provides an event for when an item is removed from a character.
    /// </summary>
    /// <param name="Character">The character that the item was removed from.</param>
    /// <param name="Item">The item.</param>
    public record ItemRemoved(Character Character, Item Item) : BaseEvent;

    /// <summary>
    /// Provides an event for when an item is used on a target.
    /// </summary>
    /// <param name="Item">The item.</param>
    /// <param name="Target">The target.</param>
    public record ItemUsed(Item Item, IInteractWithItem Target) : BaseEvent;

    /// <summary>
    /// Provides an event for when a game is started.
    /// </summary>
    /// <param name="Game">The game that was started.</param>
    public record GameStarted(Game Game) : BaseEvent;

    /// <summary>
    /// Provides an event for when a game finishes.
    /// </summary>
    /// <param name="Game">The game that was finished.</param>
    public record GameFinished(Game Game) : BaseEvent;

    /// <summary>
    /// Provides an event for when a game is updated.
    /// </summary>
    /// <param name="Game">The game that was updated.</param>
    public record GameUpdated(Game Game) : BaseEvent;

    /// <summary>
    /// Provides an event for when a game mode changes.
    /// </summary>
    /// <param name="Mode">The mode the game changed to.</param>
    public record GameModeChanged(IGameMode Mode) : BaseEvent;
}
