using NetAF.Example;
using NetAF.Logic;
using NetAF.Rendering.FrameBuilders;
using NetAF.Targets.Console;

var creator = ExampleGame.Create(new GameConfiguration(new ConsoleAdapter(), FrameBuilderCollections.Console, new(80, 50)));
GameExecutor.Execute(creator, new ConsoleExecutionController());
