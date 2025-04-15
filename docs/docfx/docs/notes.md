# Notes

## Overview
The **NoteManager** can be used to storing any useful information that the player acquires throughout the adventure.

This can then be viewed with the *Notes* command.

## Adding a note
To add a new entry use the **Add** method on the games **NoteManager**.

```csharp
game.NoteManager.Add(new("demo", "This is my note."));
```

Each entry has a *name* that allows the entry to be easily referenced and content, which is the content that is displayed to the user.

## Removing a note
To add an existing entry use the **Remove** method on the games **NoteManager**.

```csharp
game.NoteManager.Remove("demo");
```

## Expiring a note
If you want to keep an element active in the notes, but want it to be marked as expired use the **Expire** method on the games **NoteManager**.

```csharp
game.NoteManager.Expire("demo");
```

Some frame builders may render expired notes differently.

## Getting a reference to Game
In some contexts no local reference will be available to point to the instance of the running *Game*. In these cases **GameExecutor.ExecutingGame** can be used.

```csharp
GameExecutor.ExecutingGame?.NoteManager.Add(new("demo", "This is my note."));
```
