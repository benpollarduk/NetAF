﻿# Room

## Overview

A Room is the lowest level location in a game. A Region can contain multiple Rooms.

```
Overworld
├── Region
│   ├── Room
│   ├── Room
│   ├── Room
├── Region
│   ├── Room
│   ├── Room
```

A Room can contain up to six Exits, one for each of the directions **north**, **east**, **south**, **west**, **up** and **down**.

## Use

A Region can be simply instantiated with a name and description.

```csharp
var room = new Room("Name", "Description.");
```

Items can be added to the Room with the **AddItem** method.

```csharp
room.AddItem(new Item("Name", "Description."));
```

Items can be removed from a Room with the **RemoveItem** method.

```csharp
region.RemoveItem(item);
```

Characters can be added to the Room with the **AddCharacter** method.

```csharp
room.AddCharacter(new NonPlayableCharacter("Name", "Description."));
```

Characters can be removed from a Room with the **RemoveCharacter** method.

```csharp
region.RemoveCharacter(character);
```

Rooms can contain custom commands that allow the user to directly interact with the Room.

```csharp
Room room = null;
room = new("Dungeon", "A grim dungeon.", commands:
[
    new CustomCommand(new CommandHelp("Pull lever", "Pull the lever."), true, (game, args) =>
    {
        room.FindExit(Direction.East, true, out var exit);
        exit.Unlock();
        return new Reaction(ReactionResult.OK, "The exit was unlocked.");
    })
]);
```
