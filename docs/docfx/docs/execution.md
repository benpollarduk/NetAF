# Execution

## Overview

The **GameExecutor** is responsible for executing games.

## Contol

Calling *Execute* will begin execution of a game.

```csharp
GameExecutor.Execute(game);
```

When an update needs to be made to the game the **GameExecutor.Update()** method should be called.

```csharp
GameExecutor.Update("my command");
```

If a game should be cancelled then the **GameExecutor.CancelExecution()** method should be called. This will cancel the execution of any running game.