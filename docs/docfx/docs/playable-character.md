# PlayableCharacter

## Overview

A PlayableCharacter represents the character that the player plays as throughout the game. Each game has only a single PlayableCharacter.

## Use

A PlayableCharacter can be simply instantiated with a name and description.

```csharp
var player = new PlayableCharacter("Ben", "A 39 year old man.");
```

A PlayableCharacter can be also be instantiated with a list of Items.

```csharp
var player = new PlayableCharacter("Ben", "A 39 year old man.",
[
    new Item("Guitar", "A PRS Custom 22, in whale blue, of course."),
    new Item("Wallet", "An empty wallet, of course.")
]);
```

A PlayableCharacter can be given items with the **AddItem** method.

```csharp
player.AddItem(new Item("Mallet", "A large mallet."));
```

A PlayableCharacter can lose an item with the **RemoveItem** method.

```csharp
player.RemoveItem(mallet);
```

A PlayableCharacter can give an item to a non-playable character.

```csharp
var goblin = new NonPlayableCharacter("Goblin", "A vile goblin.");
var daisy = new Item("Daisy", "A beautiful daisy that is sure to cheer up even the most miserable creature.");
player.Give(daisy, goblin);
```

PlayableCharacters can contain custom commands that allow the user to directly interact with the character or other assets.

```csharp
PlayableCharacter player = new("Daisy", "A beautiful daisy that is sure to cheer up even the most miserable creature.", commands:
[
    new CustomCommand(new CommandHelp("Punch wall", "Punch the wall."), true, (game, args) =>
    {
        return new Reaction(ReactionResult.OK, "You punched the wall.");
    })
]);
```
