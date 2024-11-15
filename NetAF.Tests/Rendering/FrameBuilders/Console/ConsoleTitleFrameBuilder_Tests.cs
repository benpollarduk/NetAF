﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets;
using NetAF.Rendering.FrameBuilders;
using NetAF.Rendering.FrameBuilders.Console;

namespace NetAF.Tests.Rendering.FrameBuilders.Console
{
    [TestClass]
    public class ConsoleTitleFrameBuilder_Tests
    {
        [TestMethod]
        public void GivenDefaults_WhenBuild_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var gridStringBuilder = new GridStringBuilder();
                var builder = new ConsoleTitleFrameBuilder(gridStringBuilder);

                builder.Build(string.Empty, string.Empty, new Size(80, 50));
            });
        }
    }
}