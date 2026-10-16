using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Targets.Console.Rendering;

namespace NetAF.Tests.Targets.Console.Rendering
{
    [TestClass]
    public class ConsoleMapRenderProperties_Tests
    {
        [TestMethod]
        public void GivenDefault_WhenClone_ThenNotNull()
        {
            var properties = ConsoleMapRenderProperties.Default;

            var clone = properties.Clone();

            Assert.IsNotNull(clone);
        }

        [TestMethod]
        public void GivenDefault_WhenClone_ThenNotSameInstance()
        {
            var properties = ConsoleMapRenderProperties.Default;

            var clone = properties.Clone();

            Assert.AreNotSame(properties, clone);
        }

        [TestMethod]
        public void GivenPopulatedProperties_WhenClone_ThenAllPropertiesMatch()
        {
            var properties = new ConsoleMapRenderProperties
            {
                LockedExit = 'L',
                PointOfInterest = 'P',
                NoPointOfInterest = 'N',
                VerticalBoundary = 'V',
                HorizontalBoundary = 'H',
                VerticalExitBorder = 'v',
                HorizontalExitBorder = 'h',
                Corner = 'C',
                EmptySpace = 'E',
                UnLockedExit = 'U',
                LowerLevel = 'l',
                Player = 'p',
                Focus = 'F',
                CurrentFloorIndicator = '#',
                HorizontalKeyPadding = 7,
                VerticalKeyPadding = 3,
                BoundaryColor = AnsiColor.Blue,
                PointOfInterestColor = AnsiColor.Red,
                LockedExitColor = AnsiColor.Green,
                VisitedExitColor = AnsiColor.Cyan,
                UnvisitedExitColor = AnsiColor.Magenta,
                FocusedBoundaryColor = AnsiColor.Yellow,
                VisitedBoundaryColor = AnsiColor.White,
                UnvisitedBoundaryColor = AnsiColor.BrightBlue,
                PlayerColor = AnsiColor.BrightRed,
                LowerLevelColor = AnsiColor.BrightGreen,
                DisplayDirections = true,
                ShowLowerFloors = true
            };

            var clone = (ConsoleMapRenderProperties)properties.Clone();

            Assert.AreEqual(properties.LockedExit, clone.LockedExit);
            Assert.AreEqual(properties.PointOfInterest, clone.PointOfInterest);
            Assert.AreEqual(properties.NoPointOfInterest, clone.NoPointOfInterest);
            Assert.AreEqual(properties.VerticalBoundary, clone.VerticalBoundary);
            Assert.AreEqual(properties.HorizontalBoundary, clone.HorizontalBoundary);
            Assert.AreEqual(properties.VerticalExitBorder, clone.VerticalExitBorder);
            Assert.AreEqual(properties.HorizontalExitBorder, clone.HorizontalExitBorder);
            Assert.AreEqual(properties.Corner, clone.Corner);
            Assert.AreEqual(properties.EmptySpace, clone.EmptySpace);
            Assert.AreEqual(properties.UnLockedExit, clone.UnLockedExit);
            Assert.AreEqual(properties.LowerLevel, clone.LowerLevel);
            Assert.AreEqual(properties.Player, clone.Player);
            Assert.AreEqual(properties.Focus, clone.Focus);
            Assert.AreEqual(properties.CurrentFloorIndicator, clone.CurrentFloorIndicator);
            Assert.AreEqual(properties.HorizontalKeyPadding, clone.HorizontalKeyPadding);
            Assert.AreEqual(properties.VerticalKeyPadding, clone.VerticalKeyPadding);
            Assert.AreEqual(properties.BoundaryColor, clone.BoundaryColor);
            Assert.AreEqual(properties.PointOfInterestColor, clone.PointOfInterestColor);
            Assert.AreEqual(properties.LockedExitColor, clone.LockedExitColor);
            Assert.AreEqual(properties.VisitedExitColor, clone.VisitedExitColor);
            Assert.AreEqual(properties.UnvisitedExitColor, clone.UnvisitedExitColor);
            Assert.AreEqual(properties.FocusedBoundaryColor, clone.FocusedBoundaryColor);
            Assert.AreEqual(properties.VisitedBoundaryColor, clone.VisitedBoundaryColor);
            Assert.AreEqual(properties.UnvisitedBoundaryColor, clone.UnvisitedBoundaryColor);
            Assert.AreEqual(properties.PlayerColor, clone.PlayerColor);
            Assert.AreEqual(properties.LowerLevelColor, clone.LowerLevelColor);
            Assert.AreEqual(properties.DisplayDirections, clone.DisplayDirections);
            Assert.AreEqual(properties.ShowLowerFloors, clone.ShowLowerFloors);
        }
    }
}
