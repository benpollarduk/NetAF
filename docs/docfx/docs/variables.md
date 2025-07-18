# Variables

## Overview
The **VariableManager** can be used to store variables or informative about progress that in tern can change the course of the adventure.

## Adding a variable
To add a new variable use the **Add** method on the games **VariableManager**.

```csharp
game.VariableManager.Add(new("value 1", "abc"));
```

Each entry has a *name* that allows the entry to be easily referenced and a *value*.

## Removing a variable
To add an existing entry use the **Remove** method on the games **VariableManager**.

```csharp
game.VariableManager.Remove("value 1");
```

## Finding a variable
To find a value use the **Get** method on the games **VariableManager**.

```csharp
var variable = game.VariableManager.Get("value 1");
```

or a shorter version:

```csharp
var value = game.VariableManager["value 1"];
```

## Updating a variable
To update a value use the **Get** method on the games **VariableManager**.

```csharp
var variable = game.VariableManager.Get("value 1");
variable.Value = "new value";
```

or a shorter version:

```csharp
game.VariableManager["value 1"] = "new value";
```

## Getting a reference to Game
In some contexts no local reference will be available to point to the instance of the running *Game*. In these cases **GameExecutor.ExecutingGame** can be used.

```csharp
GameExecutor.ExecutingGame?.VariableManager.Add(new("value 1", "abc"));
```
