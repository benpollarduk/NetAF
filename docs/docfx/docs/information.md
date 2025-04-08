# Information

## Overview
The **InformationManager** can be used to storing any useful information that the player acquires throughout the adventure.

This can then be viewed with the *Info* command.

## Adding a log
To add a new entry use the **Add** method on the games **InformationManager**.

```csharp
game.InformationManager.Add("demo", "This is my log entry.");
```

Each entry has a *name* that allows the entry to be easily referenced and content, which is the content that is displayed to the user.

## Removing a log
To add an existing entry use the **Remove** method on the games **InformationManager**.

```csharp
game.InformationManager.Remove("demo");
```

## Expiring a log
If you want to keep an element active in the log, but want it to be marked as expired use the **Expire** method on the games **InformationManager**.

```csharp
game.InformationManager.Expire("demo");
```

Some frame builders may render expired logs differently.