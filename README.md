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


[![Try Now](https://img.shields.io/badge/Try-Now-brightgreen?style=for-the-badge)](https://benpollarduk.github.io/NetAF/)

</div>

## Overview
NetAF is a simple, lightweight library for building text based adventures.

![NetAF_example](https://github.com/benpollarduk/adventure-framework/assets/129943363/20656e76-4e80-475e-aa73-93976d98c5c9)

At its core NetAF provides simple classes for developing and controlling the flow of games.

### Environments
Environments are broken down into three elements - Overworld, Region and Room. An Overworld contains one or more Regions. A Region contains one or more Rooms. 
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
var room = new Room("Test Room", "A test room.", [new Exit(Direction.North)]);
```

### Items
Items add richness to a game. Items support interaction with the player, rooms, other items and NPC's. Items can morph in to other items. 
For example, using item A on item B may cause item B to morph into item C.

```csharp
var sword = new Item("Sword", "The heroes sword.");
```

### Playable Character
Each NetAF game has at least one playable character. The game is played through the view point of a playable character.

```csharp
var player = new PlayableCharacter("Dave", "The hero of the story.");
```

### Non-playable Characters
Non-playable characters (NPC's) can be added to rooms and can help drive the narrative. NPC's can hold conversations, contains items, 
and interact with items.

```csharp
var npc = new NonPlayableCharacter("Gary", "The antagonist of the story.");
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
  * **Exit** - exit the game.
  * **Help X** - display the help screen for a command, where X is the command.
  * **History** - display in-game history.
  * **Notes** - display any gathered in-game notes.
  * **Commands** - display the command list.
  * **Commands On / Commands Off** - toggle commands on/off.
  * **Key On / Key Off** - turn the Key on/off.
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
   * **Note frame** - for displaying any in-game notes.
   * **History frame** - for displaying in-game history.
   * **Narrative frame** - for displaying a section of narrative.

#### Visuals
Although NetAF is primarily focused on text and interactive fiction, there are times where adding a visual can enrich the game.

For more information see the [Visuals](https://benpollarduk.github.io/NetAF-docs/docs/visuals.html) topic.

The [NetAF.Imaging](https://github.com/benpollarduk/NetAF.Imaging) extension package can be used to extend the basic NetAF visual functions to allow conversion of images to visuals that can be displayed in a game.

#### NO_COLOR
NetAF supports the [NO_COLOR](https://no-color.org/) standard through the NO_COLOR environment variable. To disable color in console output 
add the NO_COLOR environment variable and assign it a non-empty value. This will disable color output on the console.

### Maps
Maps are automatically generated for regions and rooms, and can be viewed with the **map** command:

![map](https://github.com/user-attachments/assets/ebbc0925-7252-41d0-9c2d-60e89ecebd1c)

Maps display visited rooms, exits, player position, if an item is in a room, lower floors and more. Maps support panning and switching between vertical levels.

### Persistence
Game state can be serialized allowing progress to be saved to file and restored later.

### Targets
NetAF natively targets console, web and markup which allows other targets to be added as needed. Included in the repo are Console and Blazor examples.

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

// create region maker. the region maker simplifies creating in game regions. a region contains a series of rooms
var regionMaker = new RegionMaker("Mountain", "An imposing volcano just East of town.")
{
    // add a room to the region at position x 0, y 0, z 0
    [0, 0, 0] = new Room("Cavern", "A dark cavern set in to the base of the mountain.")
};

// create overworld maker. the overworld maker simplifies creating in game overworlds. an overworld contains a series or regions
var overworldMaker = new OverworldMaker("Daves World", "An ancient kingdom.", regionMaker);

// create the configuration for running the game. Size.Dynamic will size the game to the console.
// alternatively a fixed size can be specified and the console will size to the game
var configuration = new GameConfiguration(new ConsoleAdapter(), FrameBuilderCollections.Console, Size.Dynamic)

// create the callback for generating new instances of the game
// - information about the game
// - an introduction to the game, displayed at the star
// - asset generation for the overworld and the player
// - the conditions that end the game
// - the configuration for the game
var gameCreator = Game.Create(
    new GameInfo("The Life of Dave", "A very low budget adventure.", "Ben Pollard"),
    "Dave awakes to find himself in a cavern...",
    AssetGenerator.Retained(overworldMaker.Make(), player),
    GameEndConditions.NoEnd,
    configuration);

// begin the execution of the game
Game.Execute(gameCreator);
```

### Tutorial
The quickest way to start getting to grips with NetAF is to take a look at the [Getting Started](https://benpollarduk.github.io/NetAF-docs/docs/getting-started.html) page.

### Example game
An example game is provided in the [NetAF.Example](https://github.com/benpollarduk/adventure-framework/tree/main/NetAF.Example) directory 
and has been designed with the aim of showcasing the various features.

Set either the **NetAF.Example.Console** or **NetAF.Example.Blazor** project as the start up project and then build and run to start the application.

## Projects
Below are some projects that use NetAF:

* [Trouble Aboard the SS Hammerhead](https://benpollarduk.github.io/SSHammerhead-docs/) - A text adventure written in NetAF. The source is available [here](https://github.com/benpollarduk/SSHammerhead). Although not optimised yet, *TASSHH* can be played in the [browser](https://benpollarduk.github.io/SSHammerhead/).
  
## Documentation
Please visit [https://benpollarduk.github.io/NetAF-docs/](https://benpollarduk.github.io/NetAF-docs/) to view the NetAF documentation.

## For Open Questions
Visit https://github.com/benpollarduk/NetAF/issues
