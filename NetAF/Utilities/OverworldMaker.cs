using NetAF.Assets;
using NetAF.Assets.Locations;

namespace NetAF.Utilities
{
    /// <summary>
    /// Provides a class for helping to make Regions.
    /// </summary>
    /// <param name="identifier">An identifier for the region.</param>
    /// <param name="description">A description for the region.</param>
    /// <param name="regionMakers">The region makes to use to construct regions.</param>
    public sealed class OverworldMaker(Identifier identifier, Description description, params RegionMaker[] regionMakers)
    {
        #region Properties

        /// <summary>
        /// Get the identifier.
        /// </summary>
        private Identifier Identifier { get; } = identifier;

        /// <summary>
        /// Get the description.
        /// </summary>
        private Description Description { get; } = description;

        /// <summary>
        /// Get the region makers.
        /// </summary>
        private RegionMaker[] RegionMakers { get; } = regionMakers;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the OverworldMaker class.
        /// </summary>
        /// <param name="identifier">An identifier for the region.</param>
        /// <param name="description">A description for the region.</param>
        /// <param name="regionMakers">The region makes to use to construct regions.</param>
        public OverworldMaker(string identifier, string description, params RegionMaker[] regionMakers) : this(new Identifier(identifier), new Description(description), regionMakers)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Make an overworld.
        /// </summary>
        /// <returns>The created overworld.</returns>
        public Overworld Make()
        {
            var overworld = new Overworld(Identifier, Description);

            foreach (var regionMaker in RegionMakers)
                overworld.AddRegion(regionMaker.Make());

            return overworld;
        }

        #endregion
    }
}