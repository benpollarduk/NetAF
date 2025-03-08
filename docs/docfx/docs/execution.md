﻿# Execution

## Overview

The **GameExecutor** is responsible for executing games.

## Execute

Calling *Execute* will begin execution of a game.

```csharp
GameExecutor.Execute(game);
```

A game can be executed in one of 3 ways, *Manual*, *Automatic* and *BackgroundAutomatic*.

### Manual

Execution mode *Manual* is best suited for situations where the game cannot be run on a background thread and therefore blocking calls cannot be made while waiting for input. For example on a Blazor standalone web application. When using this mode the **GameExecutor.Update()** method should be called every time the game needs to be updated, for example following user input.

### Automatic

Execution mode *Automatic* is best suited for situations where the game can be run on a background thread and blocking calls are desirable and be made while waiting for input. For example on a Console application. **GameExecutor.Update()** does not need to be called as the **GameExecutor** handles this automatically.

### BackgroundAutomatic

Execution mode *BackgroundAutomatic* is essentially the *Automatic* mode, but running on a background thread.