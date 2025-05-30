﻿# Item

## Overview

Items can be used to add interactivity with a game. Items can be something that a player can take with them, or they may be static in a Room.

## Use

An Item can be simply instantiated with a name and description.

```csharp
var sword = new Item("Sword", "A heroes sword.");
```

By default, an Item is not takeable and is tied to a Room. If it is takeable this can be specified in the constructor.

```csharp
var sword = new Item("Sword", "A heroes sword.", true);
```

Like all Examinable objects, an Item can be assigned custom commands.

```csharp
Item bomb = new("Bomb", "A bomb", commands:
[
    new CustomCommand(new CommandHelp("Cut wire", "Cut the red wire."), true, (game, args) =>
    {
        game.Player.Kill();
        return new Reaction(ReactionResult.Fatal, "Boom!");
    })
]);
```

## Interaction

Interactions can be set up between different assets in the game. The **Interaction** contains the result of the interaction, and allows the game to react to the interaction.

```csharp
var dartsBoard = new Item("Darts board", "A darts board.");

var dart = new Item("Dart", "A dart", interaction: item =>
{
    if (item == dartsBoard)
        return new Interaction(InteractionResult.NoChange, item, "The dart stuck in the darts board.");

     return new Interaction(InteractionResult.NoChange, item);
});
```