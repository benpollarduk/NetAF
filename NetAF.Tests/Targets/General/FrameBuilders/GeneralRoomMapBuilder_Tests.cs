using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets.Locations;
using NetAF.Rendering;
using NetAF.Targets.Console.Rendering;
using NetAF.Targets.General.FrameBuilders;

namespace NetAF.Tests.Targets.General.FrameBuilders
{
    [TestClass]
    public class GeneralRoomMapBuilder_Tests
    {
        private sealed class TestBuilder : GeneralRoomMapBuilder
        {
            public int AdaptCallCount { get; private set; }
            public GridStringBuilder LastAdapted { get; private set; }

            protected override void Adapt(GridStringBuilder roomMapBuilder)
            {
                AdaptCallCount++;
                LastAdapted = roomMapBuilder;
            }
        }

        [TestMethod]
        public void GivenDefault_WhenRenderedSize_ThenWidth9Height7()
        {
            var builder = new TestBuilder();

            var result = builder.RenderedSize;

            Assert.AreEqual(9, result.Width);
            Assert.AreEqual(7, result.Height);
        }

        [TestMethod]
        public void GivenRoom_WhenBuildRoomMap_ThenAdaptIsCalled()
        {
            var builder = new TestBuilder();
            Room room = new("ROOM", string.Empty);
            ViewPoint viewPoint = ViewPoint.NoView;

            builder.BuildRoomMap(room, viewPoint, RoomMapRenderOptions.Default);

            Assert.AreEqual(1, builder.AdaptCallCount);
        }

        [TestMethod]
        public void GivenRoom_WhenBuildRoomMap_ThenAdaptedBuilderIsNotNull()
        {
            var builder = new TestBuilder();
            Room room = new("ROOM", string.Empty);
            ViewPoint viewPoint = ViewPoint.NoView;

            builder.BuildRoomMap(room, viewPoint, RoomMapRenderOptions.Default);

            Assert.IsNotNull(builder.LastAdapted);
        }

        [TestMethod]
        public void GivenRoomAndKeyPlacementLeft_WhenBuildRoomMap_ThenAdaptedBuilderIsNotNull()
        {
            var builder = new TestBuilder();
            Room room = new("ROOM", string.Empty);
            ViewPoint viewPoint = ViewPoint.NoView;

            builder.BuildRoomMap(room, viewPoint, new RoomMapRenderOptions { KeyPlacement = KeyPlacement.Left });

            Assert.IsNotNull(builder.LastAdapted);
        }

        [TestMethod]
        public void GivenRoomAndKeyPlacementAbove_WhenBuildRoomMap_ThenAdaptedBuilderIsNotNull()
        {
            var builder = new TestBuilder();
            Room room = new("ROOM", string.Empty);
            ViewPoint viewPoint = ViewPoint.NoView;

            builder.BuildRoomMap(room, viewPoint, new RoomMapRenderOptions { KeyPlacement = KeyPlacement.Above });

            Assert.IsNotNull(builder.LastAdapted);
        }

        [TestMethod]
        public void GivenRoomAndKeyPlacementRight_WhenBuildRoomMap_ThenAdaptedBuilderIsNotNull()
        {
            var builder = new TestBuilder();
            Room room = new("ROOM", string.Empty);
            ViewPoint viewPoint = ViewPoint.NoView;

            builder.BuildRoomMap(room, viewPoint, new RoomMapRenderOptions { KeyPlacement = KeyPlacement.Right });

            Assert.IsNotNull(builder.LastAdapted);
        }

        [TestMethod]
        public void GivenRoomAndKeyPlacementBelow_WhenBuildRoomMap_ThenAdaptedBuilderIsNotNull()
        {
            var builder = new TestBuilder();
            Room room = new("ROOM", string.Empty);
            ViewPoint viewPoint = ViewPoint.NoView;

            builder.BuildRoomMap(room, viewPoint, new RoomMapRenderOptions { KeyPlacement = KeyPlacement.Below });

            Assert.IsNotNull(builder.LastAdapted);
        }
    }
}
