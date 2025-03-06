<div align="center">

# NetAF
A lightweight .NET 8 library for building text based adventures.

![icon](.nuget/Icon.bmp)

[![main-ci](https://github.com/benpollarduk/adventure-framework/actions/workflows/main-ci.yml/badge.svg)](https://github.com/benpollarduk/adventure-framework/actions/workflows/main-ci.yml)
[![GitHub release](https://img.shields.io/github/release/benpollarduk/adventure-framework.svg)](https://github.com/benpollarduk/adventure-framework/releases)
[![NuGet](https://img.shields.io/nuget/v/netaf.svg)](https://www.nuget.org/packages/netaf/)
[![NuGet Downloads](https://img.shields.io/nuget/dt/netaf)](https://www.nuget.org/packages/netaf/)
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
NetAF is a simple, lightweight library for building text based adventures.

![NetAF_example](https://github.com/benpollarduk/adventure-framework/assets/129943363/20656e76-4e80-475e-aa73-93976d98c5c9)

At its core NetAF provides simple classes for developing and controlling the flow of games.

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
Room room = new("Test Room", "A test room.", [new Exit(Direction.North)]);
```

### Items
Items add richness to a game. Items support interaction with the player, rooms, other items and NPC's. Items can morph in to other items. 
For example, using item A on item B may cause item B to morph into item C.

```csharp
Item sword = new("Sword", "The heroes sword.");
```

### Playable Character
Each NetAF game has a single playable character. The game is played through the view point of the playable character.

```csharp
PlayableCharacter player = new("Dave", "The hero of the story.");
```

### Non-playable Characters
Non-playable characters (NPC's) can be added to rooms and can help drive the narrative. NPC's can hold conversations, contains items, 
and interact with items.

```csharp
NonPlayableCharacter npc = new("Gary", "The antagonist of the story.");
```
  
### Commands
NetAF provides commands for interacting with game elements:
  * **Drop X** - drop an item, where X is the item.
  * **Examine X** - allows items, characters and environments to be examined.
  * **Take X** - take an item, where X is the item.
  * **Talk to X** - talk to a NPC, where X is the NPC.
  * **Use X on Y** - use an item. Items can be used on a variety of targets. Where X is the item and Y is the target.
  * **N, S, E, W, U, D** - traverse through the rooms in a region.

NetAF also provides global commands to help with game flow and option management:
  * **About** - display version information.
  * **CommandsOn / CommandsOff** - toggle commands on/off.
  * **Exit** - exit the game.
  * **Help X** - display the help screen for a command, where X is the command.
  * **Commands** - display the command list.
  * **KeyOn / KeyOff** - turn the Key on/off.
  * **Map** - display the map.
  * **New** - start a new game.

Custom commands can be added to games without the need to extend the existing interpretation.

### Interpretation
NetAF provides classes for handling interpretation of input. Interpretation is extensible with the ability for custom interpreters to be added outside of the core NetAF library.

### Conversations
Conversations can be held between the player and a NPC. Conversations support multiple lines of dialogue and responses.

![image](https://github.com/user-attachments/assets/ad5261cf-8918-4955-8536-73644930614a)

### Attributes
All game assets support customisable attributes. This provides the possibility to build systems within a game, for example adding currency and trading, adding HP to enemies, MP to your character, durability to Items etc.

### Rendering
NetAF provides frames for rendering the various game screens. These are fully extensible and customisable. These include:
   * **Scene frame** - for displaying scenes in the game.
   * **Help frame** - for displaying in-game help for a specific command.
   * **Map frame** - for displaying the map.
   * **Title frame** - for displaying the title screen.
   * **Completion frame** - for displaying game complete.
   * **Game over frame** - for displaying game over.
   * **Conversation frame** - for displaying a conversation.
   * **Visual frame** - for displaying a visual.
   * **Command list frame** - for displaying a list of commands.

#### Visuals
Although NetAF is primarily focused on text and interactive fiction, there are times where adding a visual can enrich the game.

For more information see the [Visuals](https://benpollarduk.github.io/NetAF-docs/docs/visuals.html) topic.

The [NetAF.Imaging](https://github.com/benpollarduk/NetAF.Imaging) extension package can be used to extend the basic NetAF visual functions to allow conversion of images to visuals that can be displayed in a game.

#### NO_COLOR
NetAF supports the [NO_COLOR](https://no-color.org/) standard through the NO_COLOR environment variable. To disable color in console output 
add the NO_COLOR environment variable and assign it a non-empty value. This will disable color output on the console.

### Maps
Maps are automatically generated for regions and rooms, and can be viewed with the **map** command:

![image](https://github.com/user-attachments/assets/fa649806-32e4-493b-89a2-0a3faceaa489)

Maps display visited rooms, exits, player position, if an item is in a room, lower floors and more. Maps support panning and switching between vertical levels.

### Persistence
Game state can be serialized allowing progress to be saved to file and restored later.

### Targets
NetAF targets both the console and web, but custom targets can be also be added. The examples in this repo focus on the console, but a Blazor example targeting web is available at [https://github.com/benpollarduk/NetAF.Blazor](https://github.com/benpollarduk/NetAF.Blazor).

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
PlayableCharacter player = new("Dave", "A young boy on a quest to find the meaning of life.");

// create region maker. the region maker simplifies creating in game regions. a region contains a series of rooms
RegionMaker regionMaker = new("Mountain", "An imposing volcano just East of town.")
{
    // add a room to the region at position x 0, y 0, z 0
    [0, 0, 0] = new("Cavern", "A dark cavern set in to the base of the mountain.")
};

// create overworld maker. the overworld maker simplifies creating in game overworlds. an overworld contains a series or regions
OverworldMaker overworldMaker = new("Daves World", "An ancient kingdom.", regionMaker);

// create the callback for generating new instances of the game
// - information about the game
// - an introduction to the game, displayed at the star
// - asset generation for the overworld and the player
// - the conditions that end the game
// - the configuration for the game
var gameCreator = Game.Create(
    new("The Life of Dave", "A very low budget adventure.", "Ben Pollard"),
    "Dave awakes to find himself in a cavern...",
    AssetGenerator.Retained(overworldMaker.Make(), player),
    GameEndConditions.NoEnd,
    new GameConfiguration(new ConsoleAdapter(), FrameBuilderCollections.Console, new(80, 50)));

// begin the execution of the game
GameExecutor.Execute(gameCreator);
```

### Tutorial
The quickest way to start getting to grips with NetAF is to take a look at the [Getting Started](https://benpollarduk.github.io/NetAF-docs/docs/getting-started.html) page.

### Example game
An example game is provided in the [NetAF.Examples](https://github.com/benpollarduk/adventure-framework/tree/main/NetAF.Examples) directory 
and has been designed with the aim of showcasing the various features.

Set the **NetAF.Examples** project as the start up project and then build and run to start the application.

## Projects
Below are some projects that use NetAF:

* [Trouble Aboard the SS Hammerhead](https://benpollarduk.github.io/SSHammerhead-docs/)

## Documentation
Please visit [https://benpollarduk.github.io/NetAF-docs/](https://benpollarduk.github.io/NetAF-docs/) to view the NetAF documentation.

## For Open Questions
Visit https://github.com/benpollarduk/NetAF/issues
