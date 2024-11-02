using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Logic;
using NetAF.Serialization.Assets;

namespace NetAF.Tests.Serialization.Assets
{
    [TestClass]
    public class PlayableCharacterLocationSerialization_Tests
    {
        [TestMethod]
        public void GivenPlayableCharacterLocationWhenPlayerIdentifierIsA_ThenPlayerIdentifierIsA()
        {
            PlayableCharacterLocation location = new("A", string.Empty, string.Empty);

            PlayableCharacterLocationSerialization result = new(location);

            Assert.AreEqual("A", result.PlayerIdentifier);
        }

        [TestMethod]
        public void GivenPlayableCharacterLocationWhenRegionIdentifierIsA_ThenRegionIdentifierIsA()
        {
            PlayableCharacterLocation location = new(string.Empty, "A", string.Empty);

            PlayableCharacterLocationSerialization result = new(location);

            Assert.AreEqual("A", result.RegionIdentifier);
        }

        [TestMethod]
        public void GivenPlayableCharacterLocationWhenRoomIdentifierIsA_ThenRoomIdentifierIsA()
        {
            PlayableCharacterLocation location = new(string.Empty, string.Empty, "A");

            PlayableCharacterLocationSerialization result = new(location);

            Assert.AreEqual("A", result.RoomIdentifier);
        }

        [TestMethod]
        public void GivenEmptyPlayableCharacterLocation_WhenRestoreFrom_ThenPlayableCharacterLocationRestoredCorrectly()
        {
            PlayableCharacterLocation location = new("a", "b", "c");
            PlayableCharacterLocation location2 = new(string.Empty, string.Empty, string.Empty);
            PlayableCharacterLocationSerialization serialization = new(location);

            serialization.Restore(location2);

            Assert.AreEqual("a", location2.PlayerIdentifier);
            Assert.AreEqual("b", location2.RegionIdentifier);
            Assert.AreEqual("c", location2.RoomIdentifier);
        }
    }
}
