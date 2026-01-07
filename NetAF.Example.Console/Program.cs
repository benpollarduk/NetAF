using NetAF.Assets;
using NetAF.Example;
using NetAF.Logging.Events;
using NetAF.Logic;
using NetAF.Rendering.FrameBuilders;
using NetAF.Targets.Console;

EventBus.Subscribe<ItemUsed>(x => Console.WriteLine($"Item ({x.Item.Identifier.Name} used on {x.Target}!"));

var characterDiedCallback = new Action<CharacterDied>(x => Console.WriteLine($"{x.Character.Identifier.Name} died."));
EventBus.Subscribe(characterDiedCallback);

EventBus.Unsubscribe(characterDiedCallback);

var creator = ExampleGame.Create(new GameConfiguration(new ConsoleAdapter(), FrameBuilderCollections.Console, Size.Dynamic));
GameExecutor.Execute(creator, new ConsoleExecutionController());
