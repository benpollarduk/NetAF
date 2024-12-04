using NetAF.Assets.Locations;
using System.Linq;

namespace NetAF.Serialization.Assets
{
    /// <summary>
    /// Represents a serialization of an Overworld.
    /// </summary>
    public sealed class OverworldSerialization : ExaminableSerialization, IObjectSerialization<Overworld>
    {
        #region Properties

        /// <summary>
        /// Get or set the region serializations.
        /// </summary>
        public RegionSerialization[] Regions { get; set; }

        /// <summary>
        /// Get or set the current region.
        /// </summary>
        public string CurrentRegion { get; set; }

        #endregion

        #region StaticMethods

        /// <summary>
        /// Create a new serialization from an Overworld.
        /// </summary>
        /// <param name="overworld">The Overworld to create the serialization from.</param>
        /// <returns>The serialization.</returns>
        public static OverworldSerialization FromOverworld(Overworld overworld)
        {
            return new()
            {
                Identifier = overworld?.Identifier?.IdentifiableName,
                IsPlayerVisible = overworld?.IsPlayerVisible ?? false,
                AttributeManager = AttributeManagerSerialization.FromAttributeManager(overworld?.Attributes),
                Commands = overworld?.Commands?.Select(CustomCommandSerialization.FromCustomCommand).ToArray() ?? [],
                Regions = overworld?.Regions?.Select(RegionSerialization.FromRegion).ToArray() ?? [],
                CurrentRegion = overworld?.CurrentRegion?.Identifier?.IdentifiableName
            };
        }

        #endregion

        #region Implementation of IObjectSerialization<Overworld>

        /// <summary>
        /// Restore an instance from this serialization.
        /// </summary>
        /// <param name="overworld">The overworld to restore.</param>
        void IObjectSerialization<Overworld>.Restore(Overworld overworld)
        {
            ((IRestoreFromObjectSerialization<OverworldSerialization>)overworld).RestoreFrom(this);
        }

        #endregion
    }
}
