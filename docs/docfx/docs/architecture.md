# Architecture

## Overview
NetAF has a simple architecture and understanding it will help when developing games.

* NetAF runs on a *Target*, this could be the Console, a Blazor web app, WPF app etc.
* A *Game* acts as a container for assets, characters and logic. However it cannot be executed directly.
* The *GameExecutor* is responsible for executing games, and provides a single point for input from the user being fed to the game.
* The *Target* passes user input to the *GameExecutor*.
* The *GameExecutor* passes the input into the executing *Game*.
* The *Game* then and passes the input to its own *Interpreter* (for handling global input) and the *Interpreter* for the current *GameMode* (for handling mode specific input) in order to process it.
* The *Interpreter* tries to parse the input and if successful return an instance of *Command*.
* The returned *Command* is then invoked and returns a *Reaction* that details the result. Some instances of *ICommand* deal with interactions between assets. In this case an *Interaction* between an *Item* and a target is invoked and the result returned the *ICommand* which will return an appropriate *Reaction*.
* The *Game* processes the *Reaction*. Some instances of *Reaction* will trigger the *Game* to change *GameMode* to either display the *Reaction* or enter another*GameMode*.
* When a *GameMode* is rendered a *IFrameBuilder* can be used to generate an instance of *IFrame*.
* An instance of *IFrame* can be rendered on to an *IIOAdapter* which will display the *IFrame* to the user.

![Input-Parsing](~/images/input-parsing-sequence-diagram.png)

## Extensibility
NetAF is designed to be extensible.

* **ICommand** allows commands to be added.
* **IInterpreter** allows commands to be interpreted.
* **IGameMode** allows custom modes to be added to a *Game*.
* **IFrameBuilders** allows custom instances of *IFrame* to be created which are used to render the game state to the user.
* **IIOAdaper** provides an interface to get input from the user and render the game state back to them. *SystemConsoleAdapter* provides a wrapper around *System.Console*, however custom implementations can be added to support different types of application.
