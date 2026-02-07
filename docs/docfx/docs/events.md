# Events

## Overview

Events can be used to receive information about the game during execution. Events are managed through a pub-sub model using the *EventBus*.

## EventBus

The EventBus centralizes events so that they can be easily subscribed to.

```csharp
// create a callback to be invoke when the character dies
var characterDiedCallback = new Action<CharacterDied>(x => Console.WriteLine($"{x.Character.Identifier.Name} died."));

// register the callback with the event bus
EventBus.Subscribe(characterDiedCallback);
```

Events can also be unsubscribed from:

```csharp
// unsubscribe the registered callback from the event bus
EventBus.Unsubscribe(characterDiedCallback);
```

All callbacks can be removed from the event bus at once:

```csharp
// remove subscription for all registered callbacks
EventBus.UnsubscribeAll();
```

## Events

The following events are supported:

* **CharacterDied** - occurs when either a *PlayableCharacter* or *NonPlayableCharacter* dies.
* **RoomEntered** - occurs when a room is entered.
* **RoomExited** - occurs when a room is exited.
* **RegionEntered** - occurs when a region is entered.
* **ReionExited** - occurs when a region is exited.
* **ItemReceived** - occurs when either a *PlayableCharacter* or *NonPlayableCharacter* receives an item.
* **ItemRemoved** - occurs when an item is removed from either a *PlayableCharacter* or *NonPlayableCharacter*.
* **ItemUsed** - occurs when an item is used.
* **GameStarted** - occurs when a game is started.
* **GameUpdated** - occurs when a game is updated.
* **GameFinished** - occurs when a game is finished.
* **GameModeChanged** - occurs when a game mode changes.