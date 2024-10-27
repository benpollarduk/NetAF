﻿using NetAF.Rendering.FrameBuilders;
using NetAF.Rendering.FrameBuilders.Color;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NetAF.Tests.Rendering.FrameBuilders.Color
{
    [TestClass]
    public class ColorCompletionFrameBuilder_Tests
    {
        [TestMethod]
        public void GivenDefaults_WhenBuild_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var gridStringBuilder = new GridStringBuilder();
                var builder = new ColorCompletionFrameBuilder(gridStringBuilder);

                builder.Build(string.Empty, string.Empty, 80, 50);
            });
        }
    }
}