﻿using NetAF.Assets;
using NetAF.Interpretation;
using NetAF.Logic;
using NetAF.Logic.Configuration;
using NetAF.Rendering.FrameBuilders;

namespace NetAF.Tests
{
    internal class TestGameConfiguration(Size displaySize, ExitMode exitMode, IIOAdapter adapter) : IGameConfiguration
    {
        public static TestGameConfiguration Default => new(new Size(80, 50), ExitMode.ReturnToTitleScreen, new TestConsoleAdapter());

        public Size DisplaySize { get; private set; } = displaySize;
        public ExitMode ExitMode { get; private set; } = exitMode;
        public IInterpreter Interpreter { get; set; } = new InputInterpreter(
                                                            new FrameCommandInterpreter(),
                                                            new GlobalCommandInterpreter(),
                                                            new CustomCommandInterpreter());

        public FrameBuilderCollection FrameBuilders { get; set; } = FrameBuilderCollections.Console;
        public IIOAdapter Adapter { get; private set; } = adapter;
    }
}
