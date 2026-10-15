using NetAF.Assets;
using NetAF.Example.Lighthouse;
using NetAF.Logic;
using NetAF.Rendering.FrameBuilders;
using NetAF.Targets.Console;

var creator = LighthouseGame.Create(new GameConfiguration(new ConsoleAdapter(), FrameBuilderCollections.ConsoleModern, Size.Dynamic));
GameExecutor.Execute(creator, new ConsoleExecutionController());
