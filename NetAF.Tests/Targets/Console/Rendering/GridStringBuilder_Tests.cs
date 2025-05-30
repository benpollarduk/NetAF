﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Targets.Console.Rendering;

namespace NetAF.Tests.Targets.Console.Rendering
{
    [TestClass]
    public class GridStringBuilder_Tests
    {
        [TestMethod]
        public void GivenBlank_WhenDrawHorizontalDivider_ThenDividerIsDrawn()
        {
            var builder = new GridStringBuilder();
            builder.Resize(new(10, 10));

            builder.DrawHorizontalDivider(0, AnsiColor.Black);
            var left = builder.GetCharacter(1, 0);
            var right = builder.GetCharacter(8, 0);

            Assert.AreEqual(builder.HorizontalDividerCharacter, left);
            Assert.AreEqual(builder.HorizontalDividerCharacter, right);
        }

        [TestMethod]
        public void GivenBlank_WhenDrawUnderline_ThenUnderlineIsDrawn()
        {
            var builder = new GridStringBuilder();
            builder.Resize(new(10, 10));

            builder.DrawUnderline(0, 0, 1, AnsiColor.Black);
            var result = builder.GetCharacter(0, 0);

            Assert.AreEqual(builder.HorizontalDividerCharacter, result);
        }

        [TestMethod]
        public void GivenBlank_WhenDrawBoundary_ThenBoundaryIsDrawn()
        {
            var builder = new GridStringBuilder();
            builder.Resize(new(10, 10));

            builder.DrawBoundary(AnsiColor.Black);
            var topLeft = builder.GetCharacter(0, 0);
            var topRight = builder.GetCharacter(0, 9);
            var bottomLeft = builder.GetCharacter(0, 9);
            var bottomRight = builder.GetCharacter(9, 9);

            Assert.AreEqual(builder.LeftBoundaryCharacter, topLeft);
            Assert.AreEqual(builder.LeftBoundaryCharacter, bottomLeft);
            Assert.AreEqual(builder.RightBoundaryCharacter, topRight);
            Assert.AreEqual(builder.RightBoundaryCharacter, bottomRight);
        }

        [TestMethod]
        public void GivenA_WhenDrawWrapped_ThenDrawnCharacterIsA()
        {
            var builder = new GridStringBuilder();
            builder.Resize(new(10, 10));

            builder.DrawWrapped("A", 0, 0, 10, AnsiColor.Black, out _, out _);
            var result = builder.GetCharacter(0, 0);

            Assert.AreEqual("A", result.ToString());
        }

        [TestMethod]
        public void GivenAA_WhenDrawWrapped_ThenOutXIs1()
        {
            var builder = new GridStringBuilder();
            builder.Resize(new(10, 10));

            builder.DrawWrapped("AA", 0, 0, 10, AnsiColor.Black, out var x, out _);

            Assert.AreEqual(1, x);
        }

        [TestMethod]
        public void GivenAA_WhenDrawCentralisedWrapped_ThenDrawnCharacterIsA()
        {
            var builder = new GridStringBuilder();
            builder.Resize(new(10, 10));

            builder.DrawCentralisedWrapped("AA", 0, 10, AnsiColor.Black, out _, out _);
            var result = builder.GetCharacter(5, 0);

            Assert.AreEqual("A", result.ToString());
        }

        [TestMethod]
        public void GivenA_WhenDrawCentralisedWrapped_ThenOutXIs5()
        {
            var builder = new GridStringBuilder();
            builder.Resize(new(10, 10));

            builder.DrawCentralisedWrapped("A", 0, 10, AnsiColor.Black, out var x, out _);

            Assert.AreEqual(5, x);
        }

        [TestMethod]
        public void GivenWidth10And10Characters_WhenGetNumberOfLines_Then1()
        {
            var result = GridStringBuilder.GetNumberOfLines("AND THE DO", 0, 10);

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void GivenWidth10And11Characters_WhenGetNumberOfLines_Then2()
        {
            var result = GridStringBuilder.GetNumberOfLines("AND THE DOG", 0, 10);

            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void GivenX4Y9IsLastUsedSpaceAndCroppingWidthAndHeight_WhenToCropped_ThenWidthIs5HeightIs10()
        {
            var builder = new GridStringBuilder();
            builder.Resize(new(50, 50));

            builder.SetCell(4, 9, 'A', AnsiColor.Yellow);

            var result = builder.ToCropped(true, true);

            Assert.AreEqual(5, result.DisplaySize.Width);
            Assert.AreEqual(10, result.DisplaySize.Height);
        }

        [TestMethod]
        public void GivenX4Y9IsLastUsedSpaceAndCroppingWidth_WhenToCropped_ThenWidthIs5HeightIs50()
        {
            var builder = new GridStringBuilder();
            builder.Resize(new(50, 50));

            builder.SetCell(4, 9, 'A', AnsiColor.Yellow);

            var result = builder.ToCropped(true, false);

            Assert.AreEqual(5, result.DisplaySize.Width);
            Assert.AreEqual(50, result.DisplaySize.Height);
        }

        [TestMethod]
        public void GivenX4Y9IsLastUsedSpaceAndCroppingHeight_WhenToCropped_ThenWidthIs50HeightIs10()
        {
            var builder = new GridStringBuilder();
            builder.Resize(new(50, 50));

            builder.SetCell(4, 9, 'A', AnsiColor.Yellow);

            var result = builder.ToCropped(false, true);

            Assert.AreEqual(50, result.DisplaySize.Width);
            Assert.AreEqual(10, result.DisplaySize.Height);
        }
    }
}
