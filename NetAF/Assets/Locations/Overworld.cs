using System;
using System.Linq;
using NetAF.Commands;
using NetAF.Extensions;
using NetAF.Serialization;
using NetAF.Serialization.Assets;

namespace NetAF.Assets.Locations
{
    /// <summary>
    /// Represents an entire overworld.
    /// </summary>
    public sealed class Overworld : ExaminableObject, IRestoreFromObjectSerialization<OverworldSerialization>
    {
        #region StaticProperties

        /// <summary>
        /// Get the default examination for an Overworld.
        /// </summary>
        public static ExaminationCallback DefaultOverworldExamination => ExamineThis;

        #endregion

        #region Fields

        private Region currentRegion;

        #endregion

        #region Properties

        /// <summary>
        /// Get the regions in this overworld.
        /// </summary>
        public Region[] Regions { get; private set; } = [];

        /// <summary>
        /// Get the current region.
        /// </summary>
        public Region CurrentRegion
        {
            get { return currentRegion ?? (Regions.Length > 0 ? Regions[0] : null); }
            private set { currentRegion = value; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the overworld class.
        /// </summary>
        /// <param name="identifier">The identifier for this overworld.</param>
        /// <param name="description">A description of this overworld.</param>
        /// <param name="commands">This objects commands.</param>
        /// <param name="examination">The examination.</param>
        public Overworld(string identifier, string description, CustomCommand[] commands = null, ExaminationCallback examination = null) : this(new Identifier(identifier), new Description(description), commands, examination)
        {
        }

        /// <summary>
        /// Initializes a new instance of the overworld class.
        /// </summary>
        /// <param name="identifier">The identifier for this overworld.</param>
        /// <param name="description">A description of this overworld.</param>
        /// <param name="commands">This objects commands.</param>
        /// <param name="examination">The examination.</param>
        public Overworld(Identifier identifier, IDescription description, CustomCommand[] commands = null, ExaminationCallback examination = null)
        {
            Identifier = identifier;
            Description = description;
            Commands = commands ?? [];
            Examination = examination ?? DefaultOverworldExamination;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Add a region to this overworld.
        /// </summary>
        /// <param name="region">The region to add.</param>
        public void AddRegion(Region region)
        {
            Regions = Regions.Add(region);
        }

        /// <summary>
        /// Remove a region from this overworld.
        /// </summary>
        /// <param name="region">The region to remove.</param>
        public void RemoveRegion(Region region)
        {
            Regions = Regions.Remove(region);
        }

        /// <summary>
        /// Find a region.
        /// </summary>
        /// <param name="regionName">The regions name.</param>
        /// <param name="region">The region.</param>
        /// <returns>True if the region was found.</returns>
        public bool FindRegion(string regionName, out Region region)
        {
            var regions = Regions.Where(regionName.EqualsExaminable).ToArray();

            if (regions.Length > 0)
            {
                region = regions[0];
                return true;
            }

            region = null;
            return false;
        }

        /// <summary>
        /// Move to a region.
        /// </summary>
        /// <param name="region">The region to move to.</param>
        /// <returns>True if the region could be moved to, else false.</returns>
        public bool Move(Region region)
        {
            if (!Regions.Contains(region))
                return false;

            CurrentRegion = region;
            return true;
        }

        #endregion

        #region StaticMethods

        /// <summary>
        /// Examine this Overworld.
        /// </summary>
        /// <param name="request">The examination request.</param>
        /// <returns>The examination.</returns>
        private static Examination ExamineThis(ExaminationRequest request)
        {
            if (request.Examinable is not Overworld overworld)
                return DefaultExamination(request);

            return new(overworld.Description.GetDescription());
        }

        #endregion

        #region Implementation of IRestoreFromObjectSerialization<OverworldSerialization>

        /// <summary>
        /// Restore this object from a serialization.
        /// </summary>
        /// <param name="serialization">The serialization to restore from.</param>
        void IRestoreFromObjectSerialization<OverworldSerialization>.RestoreFrom(OverworldSerialization serialization)
        {
            ((IRestoreFromObjectSerialization<ExaminableSerialization>)this).RestoreFrom(serialization);

            foreach (var region in Regions)
            {
                var regionSerialization = Array.Find(serialization.Regions, x => region.Identifier.Equals(x.Identifier));
                ((IObjectSerialization<Region>)regionSerialization)?.Restore(region);
            }

            CurrentRegion = Array.Find(Regions, x => x.Identifier.Equals(serialization.CurrentRegion));
        }

        #endregion
    }
}