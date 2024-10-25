<div align="center">

# NetAF
A C# library that provides a framework for building text adventures and interactive stories in .NET.

[![main-ci](https://github.com/benpollarduk/adventure-framework/actions/workflows/main-ci.yml/badge.svg)](https://github.com/benpollarduk/adventure-framework/actions/workflows/main-ci.yml)
[![GitHub release](https://img.shields.io/github/release/benpollarduk/adventure-framework.svg)](https://github.com/benpollarduk/adventure-framework/releases)
[![NuGet](https://img.shields.io/nuget/v/netaf.svg)](https://www.nuget.org/packages/netaf/)
![NuGet Downloads](https://img.shields.io/nuget/dt/netaf)
[![codecov](https://codecov.io/gh/benpollarduk/NetAF/graph/badge.svg?token=X94GLVVA0T)](https://codecov.io/gh/benpollarduk/NetAF)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=benpollarduk_adventure-framework&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=benpollarduk_adventure-framework)
[![Maintainability Rating](https://sonarcloud.io/api/project_badges/measure?project=benpollarduk_adventure-framework&metric=sqale_rating)](https://sonarcloud.io/summary/new_code?id=benpollarduk_adventure-framework)
[![Security Rating](https://sonarcloud.io/api/project_badges/measure?project=benpollarduk_adventure-framework&metric=security_rating)](https://sonarcloud.io/summary/new_code?id=benpollarduk_adventure-framework)
[![Vulnerabilities](https://sonarcloud.io/api/project_badges/measure?project=benpollarduk_adventure-framework&metric=vulnerabilities)](https://sonarcloud.io/summary/new_code?id=benpollarduk_adventure-framework)
[![Bugs](https://sonarcloud.io/api/project_badges/measure?project=benpollarduk_adventure-framework&metric=bugs)](https://sonarcloud.io/summary/new_code?id=benpollarduk_adventure-framework)
[![License](https://img.shields.io/github/license/benpollarduk/adventure-framework.svg)](https://opensource.org/licenses/MIT)
[![Documentation Status](https://img.shields.io/badge/docs-latest-brightgreen.svg)](https://benpollarduk.github.io/NetAF-docs/)

</div>

## Overview
NetAF is a .NET Standard 2.0 implementation of a framework for building text based adventures.

![NetAF_example](https://github.com/benpollarduk/adventure-framework/assets/129943363/20656e76-4e80-475e-aa73-93976d98c5c9)

At its core NetAF provides simple classes for developing game elements:

### Environments
Environments are broken down in to three elements - Overworld, Region and Room. An Overworld contains one or more Regions. A Region contains one or more Rooms. 
A Room can contain up to six exits (north, south, east, west, up and down).

```
Overworld
├── Region
│   ├── Room
│   ├── Room
│   ├── Room
├── Region
│   ├── Room
│   ├── Room
```

### Exits
Rooms contain exits. Exits can be locked to block progress through the game.

```csharp
// create a test room
var room = new Room("Test Room", "A test room.");
        
// add an exit to the north
room.AddExit(new Exit(Direction.North));
```

### Items
Items add richness to a game. Items support interaction with the player, rooms, other items and NPC's. Items can morph in to other items. 
For example, using item A on item B may cause item B to morph into item C.

```csharp
var sword = new Item("Sword", "The heroes sword.");
```

### Playable Character
Each NetAF game has a single playable character. The game is played through the view point of the playable character.

```csharp
var player = new PlayableChracter("Dave", "The hero of the story.");
```

### Non-playable Characters
Non-playable characters (NPC's) can be added to rooms and can help drive the narrative. NPC's can hold conversations, contains items, 
and interact with items.

```csharp
var npc = new NonPlayableChracter("Gary", "The antagonist of the story.");
```
  
### Commands
NetAF provides commands for interacting with game elements:
  * **Drop X** - drop an item.
  * **Examine X** - allows items, characters and environments to be examined.
  * **Take X** - take an item.
  * **Talk to X** - talk to a NPC, where X is the NPC.
  * **Use X on Y** - use an item. Items can be used on a variety of targets. Where X is the item and Y is the target.
  * **N, S, E, W, U, D** - traverse through the rooms in a region.

NetAF also provides global commands to help with game flow and option management:
  * **About** - display version information.
  * **CommandsOn / CommandsOff** - toggle commands on/off.
  * **Exit** - exit the game.
  * **Help** - display the help screen.
  * **KeyOn / KeyOff** - turn the Key on/off.
  * **Map** - display the map.
  * **New** - start a new game.

Custom commands can be added to games without the need to extend the existing interpretation.

### Interpretation
NetAF provides classes for handling interpretation of input. Interpretation is extensible with the ability for custom interpreters to be added outside of the core NetAF library.

### Conversations
Conversations can be held between the player and a NPC. Conversations support multiple lines of dialogue and responses.

![image](https://github.com/ben-pollard-uk/adventure-framework/assets/129943363/5ed1afc0-1ab8-4d35-9c90-dd848f18bfda)
  
### Attributes
All game assets support customisable attributes. This provides the possibility to build systems within a game, for example adding currency and trading, adding HP to enemies, MP to your character, durability to Items etc.

### Rendering
NetAF provides frames for rendering the various game screens. These are fully extensible and customisable. These include:
   * Scene frame.
   * Help frame.
   * Map frame.
   * Title frame.
   * Completion frame.
   * Game over frame.
   * Transition frame.
   * Conversation frame.

### Maps
Maps are automatically generated for regions and rooms, and can be viewed with the **map** command:

![image](https://github.com/ben-pollard-uk/adventure-framework/assets/129943363/b6c05233-6856-4103-be44-be1c73a85874)

Maps display visited rooms, exits, player position, if an item is in a room, lower floors and more.

## Getting Started

### Clone the repo/pull NuGet
Clone the repo:
```bash
git clone https://github.com/benpollarduk/netaf.git
```
Or add the NuGet package:
```bash
dotnet add package NetAF
```

### Hello World
```csharp
// create the player. this is the character the user plays as
var player = new PlayableCharacter("Dave", "A young boy on a quest to find the meaning of life.");

/// create region maker. the region maker simplifies creating in game regions. a region contains a series of rooms
var regionMaker = new RegionMaker("Mountain", "An imposing volcano just East of town.")
{
    // add a room to the region at position x 0, y 0, z 0
    [0, 0, 0] = new Room("Cavern", "A dark cavern set in to the base of the mountain.")
};

// create overworld maker. the overworld maker simplifies creating in game overworlds. an overworld contains a series or regions
var overworldMaker = new OverworldMaker("Daves World", "An ancient kingdom.", regionMaker);

// create the callback for generating new instances of the game
// - the title of the game
// - an introduction to the game, displayed at the start
// - about the game, displayed on the about screen
// - a callback that provides a new instance of the games overworld
// - a callback that provides a new instance of the player
// - a callback that determines if the game is complete, checked every cycle of the game
// - a callback that determines if it's game over, checked every cycle of the game
var gameCreator = Game.Create(
    "The Life Of Dave",
    "Dave awakes to find himself in a cavern...",
    "A very low budget adventure.",
    overworldMaker.Make,
    () => player,
    _ => EndCheckResult.NotEnded,
    _ => EndCheckResult.NotEnded);

// begin the execution of the game
Game.Execute(gameCreator);
```

### Tutorial
The quickest way to start getting to grips with NetAF is to take a look at the [Getting Started](https://benpollarduk.github.io/NetAF-docs/docs/getting-started.html) page.

### Example game
An example game is provided in the [NetAF.Examples](https://github.com/benpollarduk/adventure-framework/tree/main/NetAF.Examples) directory 
and have been designed with the aim of showcasing the various features.

### Running the examples
The example applications can be used to execute the example NetAF game and demonstrate the core principals of the framework. 
Set the **NetAF.Examples** project as the start up project and build and run to start the application.

## Documentation
Please visit [https://benpollarduk.github.io/NetAF-docs/](https://benpollarduk.github.io/NetAF-docs/) to view the NetAF documentation.

## For Open Questions
Visit https://github.com/benpollarduk/NetAF/issues
