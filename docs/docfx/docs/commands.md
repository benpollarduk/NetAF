# Commands

## Overview
There are three main types of Command.
* **Game Commands** are used to interact with the game.
* **Global Commands** are used to interact with the program running the game.
* **Custom Commands** allow developers to add custom commands to the game without having to worry about extended the games interpreters.

## Scene Commands

### Drop
Allows players to drop an item. **R** can be used as a shortcut.

```
drop sword
```

The player can also drop **all** items.

```
drop all
```

### Examine
Allows players to examine any asset. **X** can be used as a shortcut.

Examine will examine the current room.

```
examine
```

The player themselves can be examined with **me** or the players name.

```
examine me
```

or

```
examine ben
```

The same is true for Regions, Overworlds, Items and Exits.

### Take
Allows the player to take an Item. **T** can be used as a shortcut.

```
take sword
```

Take **all** allows the player to take all takeables Items in the current Room.

```
take all
```

### Talk
Talk allows the player to start a conversation with a NonPlayableCharacter. **L** can be used as a shortcut.

If only a single NonPlayableCharacter is in the current Room no argument needs to be specified.

```
talk
```

However, if the current Room contains two or more NonPlayableCharacters then **to** and the NonPlayableCharacters name must be specified.

```
talk to dave
```

### Use
Use allows the player to use the Items that the player has or that are in the current Room.

```
use sword
```

Items can be used on the Player, the Room, an Exit, a NonPlayableCharacter or another Item. The target must be specified with the **on** keyword.

```
use sword on me
```

Or

```
use sword on bush
```

### Move
Regions are traversed with direction commands.

* **North** or **N** moves north.
* **East** or **E** moves east.
* **South** or **S** moves south.
* **West** or **W** moves west.
* **Down** or **D** moves down.
* **Up** or **U** moves up.

### End
Only valid during a conversation with a NonPlayableCharacter, the End command will end the conversation.

```
end
```

## Global Commands

### About
Displays a screen containing information about the game.

```
about
```

### Commands On / Commands Off
Toggles the display of the contextual commands on the screen on and off.

```
commands on
```

Or

```
commands off
```

### Exit
Exit the current game.

```
exit
```

### Commands
Displays a list of all available commands.

```
commands
```

### Log
Displays any gathered information.

```
info
```

### Help
Displays a help screen for a specific command.

```
help examine
```

### Key On / Key Off
Toggles the display of the map key on and off.

```
key on
```

Or

```
key off
```

### Map
Displays the Region map screen.

```
map
```

### New
Starts a new game.

```
new
```

## Custom Commands
Custom commands can be added to many of the assets, including Room, PlayableCharacter, NonPlayableCharacter, Item and Exit.
