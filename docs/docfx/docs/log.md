# Log

## Overview
The **LogManager** can be used to storing any useful information that the player acquires throughout the adventure.

This can then be viewed with the *Log* command.

## Adding a log
To add a new entry use the **Add** method on the games **LogManager**.

```csharp
game.LogManager.Add(new("demo", "This is my log entry."));
```

Each entry has a *name* that allows the entry to be easily referenced and content, which is the content that is displayed to the user.

## Removing a log
To add an existing entry use the **Remove** method on the games **LogManager**.

```csharp
game.LogManager.Remove("demo");
```

## Expiring a log
If you want to keep an element active in the log, but want it to be marked as expired use the **Expire** method on the games **LogManager**.

```csharp
game.LogManager.Expire("demo");
```

Some frame builders may render expired logs differently.

## Getting a reference to Game
In some contexts no local reference will be available to point to the instance of the running *Game*. In these cases **GameExecutor.ExecutingGame** can be used.

```csharp
GameExecutor.ExecutingGame?.LogManager.Add(new("demo", "This is my log entry."));
```
