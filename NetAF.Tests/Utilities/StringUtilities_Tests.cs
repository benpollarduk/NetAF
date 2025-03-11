using System.Collections.Generic;
using NetAF.Assets.Attributes;
using NetAF.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NetAF.Tests.Utilities
{
    [TestClass]
    public class StringUtilities_Tests
    {
        [TestMethod]
        public void GivenOneTwoThree_WhenExtractNextWordFromString_ThenExtractOne()
        {
            var test = "One two three";

            var result = StringUtilities.ExtractNextWordFromString(ref test);

            Assert.AreEqual("One", result);
        }

        [TestMethod]
        public void GivenOneTwoThreeAndExtractTwice_WhenExtractNextWordFromString_ThenExtractTwo()
        {
            var test = "One two three";

            StringUtilities.ExtractNextWordFromString(ref test);
            var result = StringUtilities.ExtractNextWordFromString(ref test);

            Assert.AreEqual("two", result);
        }

        [TestMethod]
        public void GivenOneTwoThreeAndExtractThreeTimes_WhenExtractNextWordFromString_ThenExtractThree()
        {
            var test = "One two three";

            StringUtilities.ExtractNextWordFromString(ref test);
            StringUtilities.ExtractNextWordFromString(ref test);
            var result = StringUtilities.ExtractNextWordFromString(ref test);

            Assert.AreEqual("three", result);
        }

        [TestMethod]
        public void GivenADogSatAndWidth10_WhenCutLineFromParagraph_ThenADogSat()
        {
            var paragraph = "A Dog Sat";

            var result = StringUtilities.CutLineFromParagraph(ref paragraph, 10);

            Assert.AreEqual("A Dog Sat", result);
        }

        [TestMethod]
        public void GivenADogSatAndWidth10_WhenCutLineFromParagraph_ThenParagraphEmpty()
        {
            var paragraph = "A Dog Sat";

            StringUtilities.CutLineFromParagraph(ref paragraph, 10);

            Assert.AreEqual(string.Empty, paragraph);
        }

        [TestMethod]
        public void GivenADogSatOnTheMatAndWidth10_WhenCutLineFromParagraph_ThenADogSat()
        {
            var paragraph = "A Dog Sat On The Mat";

            var result = StringUtilities.CutLineFromParagraph(ref paragraph, 10);

            Assert.AreEqual("A Dog Sat", result);
        }

        [TestMethod]
        public void GivenADogSatAndWidth10_WhenCutLineFromParagraph_ThenParagraphOnTheMat()
        {
            var paragraph = "A Dog Sat On The Mat";

            StringUtilities.CutLineFromParagraph(ref paragraph, 10);

            Assert.AreEqual(" On The Mat", paragraph);
        }

        [TestMethod]
        public void GivenABC_WhenPreenInput_ThenABC()
        {
            var paragraph = "ABC";

            var result = StringUtilities.PreenInput(paragraph);

            Assert.AreEqual("ABC", result);
        }

        [TestMethod]
        public void GivenABCLF_WhenPreenInput_ThenABCLF()
        {
            var paragraph = $"ABC{StringUtilities.LF}";

            var result = StringUtilities.PreenInput(paragraph);

            Assert.AreEqual($"ABC{StringUtilities.LF}", result);
        }

        [TestMethod]
        public void GivenABCCR_WhenPreenInput_ThenABCLF()
        {
            var paragraph = $"ABC{StringUtilities.CR}";

            var result = StringUtilities.PreenInput(paragraph);

            Assert.AreEqual($"ABC{StringUtilities.LF}", result);
        }

        [TestMethod]
        public void GivenABCCRLF_WhenPreenInput_ThenABCLF()
        {
            var paragraph = $"ABC{StringUtilities.CR}{StringUtilities.LF}";

            var result = StringUtilities.PreenInput(paragraph);

            Assert.AreEqual($"ABC{StringUtilities.LF}", result);
        }

        [TestMethod]
        public void GivenABCLFCR_WhenPreenInput_ThenABCLF()
        {
            var paragraph = $"ABC{StringUtilities.LF}{StringUtilities.CR}";

            var result = StringUtilities.PreenInput(paragraph);

            Assert.AreEqual($"ABC{StringUtilities.LF}", result);
        }

        [TestMethod]
        public void GivenNull_WhenConstructAttributesAsString_ThenEmptyString()
        {
            var result = StringUtilities.ConstructAttributesAsString(null);

            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void GivenNoAttributes_WhenConstructAttributesAsString_ThenEmptyString()
        {
            Dictionary<Attribute, int> attributes = [];

            var result = StringUtilities.ConstructAttributesAsString(attributes);

            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void GivenOneAttribute_WhenConstructAttributesAsString_ThenTestColon1()
        {
            Dictionary<Attribute, int> attributes = new()
            {
                { new Attribute("Test", string.Empty, 0, 100), 1 }
            };

            var result = StringUtilities.ConstructAttributesAsString(attributes);

            Assert.AreEqual("Test: 1", result);
        }

        [TestMethod]
        public void GivenTwoAttributes_WhenConstructAttributesAsString_ThenTestColon1TabTest2ColonSpace1()
        {
            Dictionary<Attribute, int> attributes = new()
            {
                { new Attribute("Test", string.Empty, 0, 100), 1 },
                { new Attribute("Test2", string.Empty, 0, 100), 1 }
            };

            var result = StringUtilities.ConstructAttributesAsString(attributes);

            Assert.AreEqual("Test: 1\tTest2: 1", result);
        }

        [TestMethod]
        public void GivenEmptyString_WhenSplitTextToVerbAndNoun_ThenReturnEmptyVerbAndNoun()
        {
            StringUtilities.SplitTextToVerbAndNoun(string.Empty, out var verb, out var noun);

            Assert.AreEqual(string.Empty, verb);
            Assert.AreEqual(string.Empty, noun);
        }

        [TestMethod]
        public void GivenABC_WhenSplitTextToVerbAndNoun_TheNounABCVerbEmpty()
        {
            StringUtilities.SplitTextToVerbAndNoun("ABC", out var verb, out var noun);

            Assert.AreEqual("ABC", verb);
            Assert.AreEqual(string.Empty, noun);
        }

        [TestMethod]
        public void GivenABCSpaceXYZ_WhenSplitTextToVerbAndNoun_TheNounABCVerbXYZ()
        {
            StringUtilities.SplitTextToVerbAndNoun("ABC XYZ", out var verb, out var noun);

            Assert.AreEqual("ABC", verb);
            Assert.AreEqual("XYZ", noun);
        }

        [TestMethod]
        public void GivenStringWithNoSpacesThatExceedsLength_WhenCutLineFromParagraph_TheStringCutAtLength()
        {
            var longLine = "ABCEDFGHIJKLMNOPQRSTUVWXYZ";
            var result = StringUtilities.CutLineFromParagraph(ref longLine, 10);

            Assert.AreEqual("ABCEDFGHIJ", result);
            Assert.AreEqual("KLMNOPQRSTUVWXYZ", longLine);
        }
    }
}