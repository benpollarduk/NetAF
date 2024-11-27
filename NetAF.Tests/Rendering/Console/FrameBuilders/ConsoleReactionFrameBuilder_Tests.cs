﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets;
using NetAF.Rendering.Console;
using NetAF.Rendering.Console.FrameBuilders;

namespace NetAF.Tests.Rendering.Console.FrameBuilders
{
    [TestClass]
    public class ConsoleReactionFrameBuilder_Tests
    {
        [TestMethod]
        public void GivenDefaultsAndRenderErrorMessage_WhenBuild_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var gridStringBuilder = new GridStringBuilder();
                var builder = new ConsoleReactionFrameBuilder(gridStringBuilder);

                builder.Build(string.Empty, string.Empty, true, new Size(80, 50));
            });
        }

        [TestMethod]
        public void GivenDefaultsAndRenderNonMessage_WhenBuild_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var gridStringBuilder = new GridStringBuilder();
                var builder = new ConsoleReactionFrameBuilder(gridStringBuilder);

                builder.Build(string.Empty, string.Empty, false, new Size(80, 50));
            });
        }

    }
}