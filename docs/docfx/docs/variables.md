# Variables

## Overview
The **VariableManager** can be used to storing any variables that change the course of the adventure.

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
To find a value use the **Find** method on the games **VariableManager**.

```csharp
var variable = game.VariableManager.Find("value 1");
```

## Updating a variable
To update a value use the **Find** method on the games **VariableManager**.

```csharp
var variable = game.VariableManager.Find("value 1");
variable.Value = "new value";
```

## Getting a reference to Game
In some contexts no local reference will be available to point to the instance of the running *Game*. In these cases **GameExecutor.ExecutingGame** can be used.

```csharp
GameExecutor.ExecutingGame?.VariableManager.Add(new("value 1", "abc"));
```
