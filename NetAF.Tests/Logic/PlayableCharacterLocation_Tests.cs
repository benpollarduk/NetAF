using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Logic;
using NetAF.Serialization.Assets;

namespace NetAF.Tests.Logic
{
    [TestClass]
    public class PlayableCharacterLocation_Tests
    {
        [TestMethod]
        public void GivenSerialization_WhenFromSerialization_ThenRestoredCorrectly()
        {
            PlayableCharacterLocation location = new("a", "b", "c");
            PlayableCharacterLocationSerialization serialization = PlayableCharacterLocationSerialization.FromPlayableCharacterLocation(location);

            var result = PlayableCharacterLocation.FromSerialization(serialization);

            Assert.AreEqual("a", result.PlayerIdentifier);
            Assert.AreEqual("b", result.RegionIdentifier);
            Assert.AreEqual("c", result.RoomIdentifier);
        }
    }
}
