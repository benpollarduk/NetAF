# Architecture Overview

## Overview
NetAF has a simple architecture and understanding it will help when developing games.

* A **Game** encapsulates all assets and provides top level logic.
* When the **Game** is executing the following loop runs for the duration of the execution.
  * A **Game** accepts user input and passes it to its own **Interpreter** and the **Interpreter** for the current **GameMode** in order to process it.
  * The **Interpreter** tries to parse the input and return an instance of **Command**.
  * The returned **Command** is then invoked and returns a **Reaction** that details the result.
    * Some instances of **Command** deal with interactions between assets. In this case an **Interaction** between an **Item** and a target is invoked and the result returned the **Command** which will return an appropriate **Reaction**.
  * The **Game** processes the **Reaction** as required.
