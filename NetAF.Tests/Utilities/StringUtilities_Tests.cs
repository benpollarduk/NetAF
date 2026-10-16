using System.Collections.Generic;
using NetAF.Assets;
using NetAF.Assets.Attributes;
using NetAF.Assets.Characters;
using NetAF.Assets.Locations;
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
        public void GivenABCTab_WhenPreenOutput_ThenABCSpaceSpaceSpaceSpace()
        {
            var paragraph = "ABC\t";

            var result = StringUtilities.PreenOutput(paragraph);

            Assert.AreEqual("ABC    ", result);
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
                { new Attribute("Test", string.Empty, 0, 100, true), 1 }
            };

            var result = StringUtilities.ConstructAttributesAsString(attributes);

            Assert.AreEqual("Test: 1", result);
        }

        [TestMethod]
        public void GivenTwoAttributes_WhenConstructAttributesAsString_ThenTestColon1CommaSpaceTest2ColonSpace1()
        {
            Dictionary<Attribute, int> attributes = new()
            {
                { new Attribute("Test", string.Empty, 0, 100, true), 1 },
                { new Attribute("Test2", string.Empty, 0, 100, true), 1 }
            };

            var result = StringUtilities.ConstructAttributesAsString(attributes);

            Assert.AreEqual("Test: 1, Test2: 1", result);
        }

        [TestMethod]
        public void GivenEmptyString_WhenSplitInputToCommandAndArgument_ThenReturnEmptyCommandAndArgs()
        {
            StringUtilities.SplitInputToCommandAndArgument(string.Empty, out var command, out var args);

            Assert.AreEqual(string.Empty, command);
            Assert.AreEqual(string.Empty, args);
        }

        [TestMethod]
        public void GivenABC_WhenSplitInputToCommandAndArgument_ThenCommandABCArgsEmpty()
        {
            StringUtilities.SplitInputToCommandAndArgument("ABC", out var command, out var args);

            Assert.AreEqual("ABC", command);
            Assert.AreEqual(string.Empty, args);
        }

        [TestMethod]
        public void GivenABCSpaceXYZ_WhenSplitInputToCommandAndArgument_ThenCommandABCArgsXYZ()
        {
            StringUtilities.SplitInputToCommandAndArgument("ABC XYZ", out var command, out var args);

            Assert.AreEqual("ABC", command);
            Assert.AreEqual("XYZ", args);
        }

        [TestMethod]
        public void GivenEmptyString_WhenSplitInputToCommandAndArguments_ThenCommandEmptyStringArgsEmpty()
        {
            StringUtilities.SplitInputToCommandAndArguments(string.Empty, out var command, out var args);

            Assert.AreEqual(string.Empty, command);
            Assert.IsEmpty(args);
        }

        [TestMethod]
        public void GivenABC_WhenSplitInputToCommandAndArguments_ThenCommandABCArgsEmpty()
        {
            StringUtilities.SplitInputToCommandAndArguments("ABC", out var command, out var args);

            Assert.AreEqual("ABC", command);
            Assert.IsEmpty(args);
        }

        [TestMethod]
        public void GivenABCSpaceXYZ_WhenSplitInputToCommandAndArguments_ThenCommandABCArgsXYZ()
        {
            StringUtilities.SplitInputToCommandAndArguments("ABC XYZ", out var command, out var args);

            Assert.AreEqual("ABC", command);
            Assert.AreEqual("XYZ", args[0]);
        }

        [TestMethod]
        public void GivenABCSpaceXYZSpaceUVW_WhenSplitInputToCommandAndArguments_ThenCommandABCArgsXYZAndUVW()
        {
            StringUtilities.SplitInputToCommandAndArguments("ABC XYZ UVW", out var command, out var args);

            Assert.AreEqual("ABC", command);
            Assert.AreEqual("XYZ", args[0]);
            Assert.AreEqual("UVW", args[1]);
        }

        [TestMethod]
        public void GivenStringWithNoSpacesThatExceedsLength_WhenCutLineFromParagraph_ThenStringCutAtLength()
        {
            var longLine = "ABCEDFGHIJKLMNOPQRSTUVWXYZ";
            var result = StringUtilities.CutLineFromParagraph(ref longLine, 10);

            Assert.AreEqual("ABCEDFGHIJ", result);
            Assert.AreEqual("KLMNOPQRSTUVWXYZ", longLine);
        }

        [TestMethod]
        public void GivenAAndBWithNoDelimiter_WhenConcatenate_ThenAB()
        {
            string[] values = ["A", "B"];
            var delimiter = string.Empty;
            var result = StringUtilities.Concatenate(values, delimiter);

            Assert.AreEqual("AB", result);
        }

        [TestMethod]
        public void GivenAAndBWithSpaceDelimiter_WhenConcatenate_ThenASpaceB()
        {
            string[] values = ["A", "B"];
            var delimiter = " ";
            var result = StringUtilities.Concatenate(values, delimiter);

            Assert.AreEqual("A B", result);
        }

        [TestMethod]
        public void GivenNoKeyWords_WhenLocateStringMarkers_ThenNoMarkers()
        {
            var result = StringUtilities.LocateStringMarkers("A Dog Sat On The Mat", []);

            Assert.IsEmpty(result);
        }

        [TestMethod]
        public void GivenKeyWordNotPresent_WhenLocateStringMarkers_ThenNoMarkers()
        {
            var result = StringUtilities.LocateStringMarkers("A Dog Sat On The Mat", ["Cat"]);

            Assert.IsEmpty(result);
        }

        [TestMethod]
        public void GivenSingleKeyWordPresentOnce_WhenLocateStringMarkers_ThenOneMarkerWithCorrectStartIndexAndLength()
        {
            var result = StringUtilities.LocateStringMarkers("A Dog Sat On The Mat", ["Dog"]);

            Assert.HasCount(1, result);
            Assert.AreEqual(2, result[0].StartIndex);
            Assert.AreEqual(3, result[0].Length);
            Assert.AreEqual(5, result[0].EndIndex);
        }

        [TestMethod]
        public void GivenSingleKeyWordPresentMultipleTimes_WhenLocateStringMarkers_ThenMarkerForEachOccurrence()
        {
            var result = StringUtilities.LocateStringMarkers("a a a", ["a"]);

            Assert.HasCount(3, result);
            Assert.AreEqual(0, result[0].StartIndex);
            Assert.AreEqual(2, result[1].StartIndex);
            Assert.AreEqual(4, result[2].StartIndex);
        }

        [TestMethod]
        public void GivenMultipleKeyWords_WhenLocateStringMarkers_ThenMarkerForEachKeyWord()
        {
            var result = StringUtilities.LocateStringMarkers("A Dog Sat On The Mat", ["Dog", "Mat"]);

            Assert.HasCount(2, result);
            Assert.AreEqual(2, result[0].StartIndex);
            Assert.AreEqual(3, result[0].Length);
            Assert.AreEqual(17, result[1].StartIndex);
            Assert.AreEqual(3, result[1].Length);
        }

        [TestMethod]
        public void GivenOverlappingKeyWord_WhenLocateStringMarkers_ThenNonOverlappingMarkers()
        {
            var result = StringUtilities.LocateStringMarkers("aa aa", ["aa"]);

            Assert.HasCount(2, result);
            Assert.AreEqual(0, result[0].StartIndex);
            Assert.AreEqual(3, result[1].StartIndex);
        }

        [TestMethod]
        public void GivenKeyWordAppearingMultipleTimesInSentence_WhenLocateStringMarkers_ThenMarkerForEachOccurrence()
        {
            var result = StringUtilities.LocateStringMarkers("Dog. When The dog sat on the dog.", ["dog"]);

            // By default the search is case-insensitive, so the leading "Dog" is also included.
            Assert.HasCount(3, result);
            Assert.AreEqual(0, result[0].StartIndex);
            Assert.AreEqual(14, result[1].StartIndex);
            Assert.AreEqual(29, result[2].StartIndex);
        }

        [TestMethod]
        public void GivenCaseInsensitive_WhenLocateStringMarkers_ThenDifferentCasingsMatched()
        {
            var result = StringUtilities.LocateStringMarkers("Dog dog DOG", ["dog"], caseSensitive: false);

            Assert.HasCount(3, result);
            Assert.AreEqual(0, result[0].StartIndex);
            Assert.AreEqual(4, result[1].StartIndex);
            Assert.AreEqual(8, result[2].StartIndex);
        }

        [TestMethod]
        public void GivenCaseSensitive_WhenLocateStringMarkers_ThenOnlyExactCasingMatched()
        {
            var result = StringUtilities.LocateStringMarkers("Dog dog DOG", ["dog"], caseSensitive: true);

            Assert.HasCount(1, result);
            Assert.AreEqual(4, result[0].StartIndex);
        }

        [TestMethod]
        public void GivenCaseInsensitiveSubstringOfLargerWord_WhenLocateStringMarkers_ThenNoMarker()
        {
            var result = StringUtilities.LocateStringMarkers("The Bedroom is dark", ["bed"], caseSensitive: false);

            Assert.IsEmpty(result);
        }

        [TestMethod]
        public void GivenCaseInsensitiveWholeWordWithDifferentCasing_WhenLocateStringMarkers_ThenMarker()
        {
            var result = StringUtilities.LocateStringMarkers("The BED is here", ["bed"], caseSensitive: false);

            Assert.HasCount(1, result);
            Assert.AreEqual(4, result[0].StartIndex);
        }

        [TestMethod]
        public void GivenKeyWordAsSubstringOfLargerWord_WhenLocateStringMarkers_ThenNoMarker()
        {
            var result = StringUtilities.LocateStringMarkers("The bedroom is dark", ["bed"]);

            Assert.IsEmpty(result);
        }

        [TestMethod]
        public void GivenKeyWordFollowedByPunctuation_WhenLocateStringMarkers_ThenMarker()
        {
            var result = StringUtilities.LocateStringMarkers("bed? bed! bed. bed, bed", ["bed"]);

            Assert.HasCount(5, result);
            Assert.AreEqual(0, result[0].StartIndex);
            Assert.AreEqual(5, result[1].StartIndex);
            Assert.AreEqual(10, result[2].StartIndex);
            Assert.AreEqual(15, result[3].StartIndex);
            Assert.AreEqual(20, result[4].StartIndex);
        }

        [TestMethod]
        public void GivenKeyWordAtStartAndEndOfString_WhenLocateStringMarkers_ThenMarkerForEach()
        {
            var result = StringUtilities.LocateStringMarkers("bed in the bed", ["bed"]);

            Assert.HasCount(2, result);
            Assert.AreEqual(0, result[0].StartIndex);
            Assert.AreEqual(11, result[1].StartIndex);
        }

        [TestMethod]
        public void GivenKeyWordPrecededByWordCharacter_WhenLocateStringMarkers_ThenNoMarker()
        {
            var result = StringUtilities.LocateStringMarkers("The flatbed is here", ["bed"]);

            Assert.IsEmpty(result);
        }

        [TestMethod]
        public void GivenMultiWordKeyWord_WhenLocateStringMarkers_ThenMarker()
        {
            var result = StringUtilities.LocateStringMarkers("The old bed room is dark", ["bed room"]);

            Assert.HasCount(1, result);
            Assert.AreEqual(8, result[0].StartIndex);
        }

        [TestMethod]
        public void GivenNullValue_WhenGetHighlightMask_ThenEmptyMask()
        {
            var result = StringUtilities.GetHighlightMask(null, [new StringMarker("dog", 0)]);

            Assert.IsEmpty(result);
        }

        [TestMethod]
        public void GivenEmptyValue_WhenGetHighlightMask_ThenEmptyMask()
        {
            var result = StringUtilities.GetHighlightMask(string.Empty, [new StringMarker("dog", 0)]);

            Assert.IsEmpty(result);
        }

        [TestMethod]
        public void GivenNullMarkers_WhenGetHighlightMask_ThenAllFalse()
        {
            var result = StringUtilities.GetHighlightMask("The dog sat", null);

            Assert.HasCount(11, result);
            Assert.AreSequenceEqual(new bool[11], result);
        }

        [TestMethod]
        public void GivenNoMarkers_WhenGetHighlightMask_ThenAllFalse()
        {
            var result = StringUtilities.GetHighlightMask("The dog sat", []);

            Assert.HasCount(11, result);
            Assert.AreSequenceEqual(new bool[11], result);
        }

        [TestMethod]
        public void GivenMarkerWithEmptyValue_WhenGetHighlightMask_ThenAllFalse()
        {
            var result = StringUtilities.GetHighlightMask("The dog sat", [new StringMarker(string.Empty, 0)]);

            Assert.HasCount(11, result);
            Assert.AreSequenceEqual(new bool[11], result);
        }

        [TestMethod]
        public void GivenSingleMarker_WhenGetHighlightMask_ThenOnlyMatchingCharactersHighlighted()
        {
            var markers = StringUtilities.LocateStringMarkers("The dog sat", ["dog"]);

            var result = StringUtilities.GetHighlightMask("The dog sat", markers);

            bool[] expected = [false, false, false, false, true, true, true, false, false, false, false];
            Assert.AreSequenceEqual(expected, result);
        }

        [TestMethod]
        public void GivenMultipleOccurrences_WhenGetHighlightMask_ThenAllMatchingCharactersHighlighted()
        {
            var markers = StringUtilities.LocateStringMarkers("dog sat dog", ["dog"]);

            var result = StringUtilities.GetHighlightMask("dog sat dog", markers);

            bool[] expected = [true, true, true, false, false, false, false, false, true, true, true];
            Assert.AreSequenceEqual(expected, result);
        }

        [TestMethod]
        public void GivenMultipleMarkers_WhenGetHighlightMask_ThenAllMatchingCharactersHighlighted()
        {
            var markers = StringUtilities.LocateStringMarkers("The dog sat", ["The", "sat"]);

            var result = StringUtilities.GetHighlightMask("The dog sat", markers);

            bool[] expected = [true, true, true, false, false, false, false, false, true, true, true];
            Assert.AreSequenceEqual(expected, result);
        }

        [TestMethod]
        public void GivenMarkerNotPresentInValue_WhenGetHighlightMask_ThenAllFalse()
        {
            var result = StringUtilities.GetHighlightMask("The dog sat", [new StringMarker("cat", 0)]);

            Assert.HasCount(11, result);
            Assert.AreSequenceEqual(new bool[11], result);
        }

        [TestMethod]
        public void GivenMarkerAsSubstringOfLargerWord_WhenGetHighlightMask_ThenNotHighlighted()
        {
            var result = StringUtilities.GetHighlightMask("The bedroom", [new StringMarker("bed", 4)]);

            Assert.HasCount(11, result);
            Assert.AreSequenceEqual(new bool[11], result);
        }

        [TestMethod]
        public void GivenMarkerAsWholeWordAmongstLargerWord_WhenGetHighlightMask_ThenOnlyWholeWordHighlighted()
        {
            var result = StringUtilities.GetHighlightMask("bedroom bed", [new StringMarker("bed", 8)]);

            bool[] expected = [false, false, false, false, false, false, false, false, true, true, true];
            Assert.AreSequenceEqual(expected, result);
        }

        [TestMethod]
        public void GivenCaseInsensitiveMarkerAsSubstringOfLargerWord_WhenGetHighlightMask_ThenNotHighlighted()
        {
            var result = StringUtilities.GetHighlightMask("The Bedroom", [new StringMarker("bed", 4)], caseSensitive: false);

            Assert.HasCount(11, result);
            Assert.AreSequenceEqual(new bool[11], result);
        }

        [TestMethod]
        public void GivenCaseInsensitiveMarkerAsWholeWordWithDifferentCasing_WhenGetHighlightMask_ThenHighlighted()
        {
            var result = StringUtilities.GetHighlightMask("The BED sat", [new StringMarker("bed", 4)], caseSensitive: false);

            bool[] expected = [false, false, false, false, true, true, true, false, false, false, false];
            Assert.AreSequenceEqual(expected, result);
        }

        [TestMethod]
        public void GivenCaseInsensitive_WhenGetHighlightMask_ThenDifferentCasingHighlighted()
        {
            var result = StringUtilities.GetHighlightMask("The Dog sat", [new StringMarker("dog", 4)], caseSensitive: false);

            bool[] expected = [false, false, false, false, true, true, true, false, false, false, false];
            Assert.AreSequenceEqual(expected, result);
        }

        [TestMethod]
        public void GivenCaseSensitive_WhenGetHighlightMask_ThenDifferentCasingNotHighlighted()
        {
            var result = StringUtilities.GetHighlightMask("The Dog sat", [new StringMarker("dog", 4)], caseSensitive: true);

            Assert.HasCount(11, result);
            Assert.AreSequenceEqual(new bool[11], result);
        }

        [TestMethod]
        public void GivenCaseSensitive_WhenGetHighlightMask_ThenOnlyExactCasingHighlighted()
        {
            var result = StringUtilities.GetHighlightMask("The dog sat", [new StringMarker("dog", 4)], caseSensitive: true);

            bool[] expected = [false, false, false, false, true, true, true, false, false, false, false];
            Assert.AreSequenceEqual(expected, result);
        }

        [TestMethod]
        public void GivenNullViewPointAndNoExaminables_WhenFindKeywords_ThenNoKeywords()
        {
            var room = new Room("Room", "Room");
            var scene = new ExaminationScene(new PlayableCharacter("Player", "Player"), room, null);

            var result = StringUtilities.FindKeywords(scene);

            Assert.IsEmpty(result);
        }

        [TestMethod]
        public void GivenRoomWithItem_WhenFindKeywords_ThenItemNameReturned()
        {
            var room = new Room("Room", "Room", items: [new Item("Key", "A key.")]);
            var scene = new ExaminationScene(new PlayableCharacter("Player", "Player"), room, null);

            var result = StringUtilities.FindKeywords(scene);

            Assert.HasCount(1, result);
            Assert.AreEqual("Key", result[0]);
        }

        [TestMethod]
        public void GivenPlayerWithItem_WhenFindKeywords_ThenItemNameReturned()
        {
            var room = new Room("Room", "Room");
            var player = new PlayableCharacter("Player", "Player");
            player.AddItem(new Item("Sword", "A sword."));
            var scene = new ExaminationScene(player, room, null);

            var result = StringUtilities.FindKeywords(scene);

            Assert.HasCount(1, result);
            Assert.AreEqual("Sword", result[0]);
        }

        [TestMethod]
        public void GivenRoomWithExit_WhenFindKeywords_ThenExitNameReturned()
        {
            var room = new Room("Room", "Room", [new Exit(Direction.North)]);
            var scene = new ExaminationScene(new PlayableCharacter("Player", "Player"), room, null);

            var result = StringUtilities.FindKeywords(scene);

            Assert.HasCount(1, result);
            Assert.AreEqual("North", result[0]);
        }

        [TestMethod]
        public void GivenViewPointWithSurroundingRoomsAndDirections_WhenFindKeywords_ThenRoomNamesAndDirectionsReturned()
        {
            var room = new Room("Room", "Room", [new Exit(Direction.North)]);
            var regionMaker = new RegionMaker("Region", "Region")
            {
                [0, 1, 0] = room,
                [0, 2, 0] = new Room("North Room", "North Room", [new Exit(Direction.South)])
            };

            var region = regionMaker.Make(0, 1, 0);
            region.Enter();
            var viewPoint = ViewPoint.Create(region);
            var scene = new ExaminationScene(new PlayableCharacter("Player", "Player"), room, viewPoint);

            var result = StringUtilities.FindKeywords(scene);

            Assert.Contains("North Room", result);
            Assert.Contains("North", result);
        }

        [TestMethod]
        public void GivenDuplicateKeywords_WhenFindKeywords_ThenKeywordsAreDistinct()
        {
            var room = new Room("Room", "Room", [new Exit(Direction.North)]);
            var regionMaker = new RegionMaker("Region", "Region")
            {
                [0, 1, 0] = room,
                [0, 2, 0] = new Room("North", "North", [new Exit(Direction.South)])
            };

            var region = regionMaker.Make(0, 1, 0);
            region.Enter();
            var viewPoint = ViewPoint.Create(region);
            var scene = new ExaminationScene(new PlayableCharacter("Player", "Player"), room, viewPoint);

            var result = StringUtilities.FindKeywords(scene);

            Assert.HasCount(result.Length, new HashSet<string>(result));
        }

        [TestMethod]
        public void GivenNoSceneOrViewPoint_WhenFindKeywords_ThenNoKeywords()
        {
            var result = StringUtilities.FindKeywords(null);

            Assert.IsEmpty(result);
        }
    }
}