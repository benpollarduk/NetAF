# Frame Builders

## Overview
In NetAF output is handled using the **FrameBuilders**. A FrameBuilder is essentially a class that builds a **Frame** that can render a specific state in the game. This **Frame** can then be rendered on a target by calling its **Render** method. Think of the **FrameBuilder** as the builder of the output and the **Frame** as the output itself.

There are a few types of **FrameBuilder**, each responsible for rendering a specific game state.
* **SceneFrameBuilder** is responsible for building frames that render the reactions to input in a game.
* **ReactionFrameBuilder** is responsible for building frames that render the scenes in a game.
* **TitleFrameBuilder** is responsible for building the title screen frame.
* **RegionMapFrameBuilder** is responsible for building a frame that displays a map of a Region.
* **TransitionFrameBuilder** is responsible for building frames that display transitions.
* **AboutFrameBuilder** is responsible for building a frame to display the about information.
* **HelpFrameBuilder** is responsible for building frames to display the help.
* **GameOverFrameBuilder** is responsible for building a frame to display the game over screen.
* **CompletionFrameBuilder** is responsible for building a frame to display the completion screen.
* **ConversationFrameBuilder** is responsible for building a frame that can render a conversation.
* **VisualFrameBuilder** is responsible for building a frame to render a visual.
* **InformationFrameBuilder** is responsible for building a frame to render any gathered information.

A game accepts a **FrameBuilderCollection**. A **FrameBuilderCollection** is a collection of all the different **FrameBuilders** required to render a game. All **FrameBuilders** are extensible, so the output for all parts of the game can be fully customised.
