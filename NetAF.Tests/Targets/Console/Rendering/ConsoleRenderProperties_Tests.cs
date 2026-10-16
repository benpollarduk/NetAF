using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Targets.Console.Rendering;

namespace NetAF.Tests.Targets.Console.Rendering
{
    [TestClass]
    public class ConsoleRenderProperties_Tests
    {
        [TestMethod]
        public void GivenDefault_WhenClone_ThenNotNull()
        {
            var properties = ConsoleRenderProperties.Default;

            var clone = properties.Clone();

            Assert.IsNotNull(clone);
        }

        [TestMethod]
        public void GivenDefault_WhenClone_ThenNotSameInstance()
        {
            var properties = ConsoleRenderProperties.Default;

            var clone = properties.Clone();

            Assert.AreNotSame(properties, clone);
        }

        [TestMethod]
        public void GivenPopulatedProperties_WhenClone_ThenAllPropertiesMatch()
        {
            var properties = new ConsoleRenderProperties
            {
                LeftBoundary = '[',
                RightBoundary = ']',
                HorizontalDivider = '=',
                BackgroundColor = AnsiColor.Blue,
                BorderColor = AnsiColor.Red,
                TitleColor = AnsiColor.Green,
                CommandColor = AnsiColor.Cyan,
                CommandDescriptionColor = AnsiColor.Magenta,
                OptionColor = AnsiColor.Yellow,
                OptionDescriptionColor = AnsiColor.White,
                MainTextColor = AnsiColor.BrightBlue,
                SecondaryTextColor = AnsiColor.BrightRed,
                InputColor = AnsiColor.BrightGreen,
                MaximumNarrativeLineWidth = 42,
                HighlightKeywords = true,
                HighlightTextColor = AnsiColor.BrightCyan
            };

            var clone = (ConsoleRenderProperties)properties.Clone();

            Assert.AreEqual(properties.LeftBoundary, clone.LeftBoundary);
            Assert.AreEqual(properties.RightBoundary, clone.RightBoundary);
            Assert.AreEqual(properties.HorizontalDivider, clone.HorizontalDivider);
            Assert.AreEqual(properties.BackgroundColor, clone.BackgroundColor);
            Assert.AreEqual(properties.BorderColor, clone.BorderColor);
            Assert.AreEqual(properties.TitleColor, clone.TitleColor);
            Assert.AreEqual(properties.CommandColor, clone.CommandColor);
            Assert.AreEqual(properties.CommandDescriptionColor, clone.CommandDescriptionColor);
            Assert.AreEqual(properties.OptionColor, clone.OptionColor);
            Assert.AreEqual(properties.OptionDescriptionColor, clone.OptionDescriptionColor);
            Assert.AreEqual(properties.MainTextColor, clone.MainTextColor);
            Assert.AreEqual(properties.SecondaryTextColor, clone.SecondaryTextColor);
            Assert.AreEqual(properties.InputColor, clone.InputColor);
            Assert.AreEqual(properties.MaximumNarrativeLineWidth, clone.MaximumNarrativeLineWidth);
            Assert.AreEqual(properties.HighlightKeywords, clone.HighlightKeywords);
            Assert.AreEqual(properties.HighlightTextColor, clone.HighlightTextColor);
        }
    }
}
