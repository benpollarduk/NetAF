﻿using System;
using System.IO;
using System.Linq;
using NetAF.Assets;
using NetAF.Rendering.FrameBuilders;
using NetAF.Rendering.FrameBuilders.Color;
using NetAF.Rendering.Frames;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NetAF.Tests.Rendering.Frames
{
    [TestClass]
    public class GridTextFrame_Tests
    {
        [TestMethod]
        public void GivenNoColorEnvironmentVariableSetToEmptyString_WhenIsColorSuppressed_ThenReturnFalse()
        {
            Environment.SetEnvironmentVariable(GridTextFrame.NO_COLOR, "");

            var result = GridTextFrame.IsColorSuppressed();

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenNoColorEnvironmentVariableSetTo0_WhenIsColorSuppressed_ThenReturnFalse()
        {
            Environment.SetEnvironmentVariable(GridTextFrame.NO_COLOR, "0");

            var result = GridTextFrame.IsColorSuppressed();

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenNoColorEnvironmentVariableSetToFalse_WhenIsColorSuppressed_ThenReturnFalse()
        {
            Environment.SetEnvironmentVariable(GridTextFrame.NO_COLOR, "False");

            var result = GridTextFrame.IsColorSuppressed();

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenNoColorEnvironmentVariableSetTo1_WhenIsColorSuppressed_ThenReturnTrue()
        {
            Environment.SetEnvironmentVariable(GridTextFrame.NO_COLOR, "1");

            var result = GridTextFrame.IsColorSuppressed();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenNoColorEnvironmentVariableSetToTrue_WhenIsColorSuppressed_ThenReturnTrue()
        {
            Environment.SetEnvironmentVariable(GridTextFrame.NO_COLOR, "True");

            var result = GridTextFrame.IsColorSuppressed();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Given10x10GridWithBorder_WhenRender_ThenStreamContainsData()
        {
            var gridStringBuilder = new GridStringBuilder();
            gridStringBuilder.Resize(new Size(10, 10));
            gridStringBuilder.DrawBoundary(AnsiColor.Black);
            var frame = new GridTextFrame(gridStringBuilder, 0, 0, AnsiColor.Black);
            byte[] data;

            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    frame.Render(writer);
                    writer.Flush();
                    data = stream.ToArray();
                }
            }

            Assert.IsTrue(data.Any(x => x != 0));
        }

        [TestMethod]
        public void Given10x10GridWithBorder_WhenToString_ThenStringWithDataReturned()
        {
            var gridStringBuilder = new GridStringBuilder();
            gridStringBuilder.Resize(new Size(10, 10));
            gridStringBuilder.DrawBoundary(AnsiColor.Black);
            var frame = new GridTextFrame(gridStringBuilder, 0, 0, AnsiColor.Black);

            var result = frame.ToString();

            Assert.IsTrue(result.Any(x => x != 0));
        }
    }
}
