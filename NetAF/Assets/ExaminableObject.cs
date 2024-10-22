using System;
using System.Linq;
using NetAF.Assets.Attributes;
using NetAF.Commands;
using NetAF.Extensions;
using NetAF.Utilities;

namespace NetAF.Assets
{
    /// <summary>
    /// Represents an object that can be examined.
    /// </summary>
    public class ExaminableObject : IExaminable
    {
        #region Properties

        /// <summary>
        /// Get or set the callback handling all examination of this object.
        /// </summary>
        public ExaminationCallback Examination { get; set; } = request =>
        {
            var description = string.Empty;

            if (request.Examinable.Description != null)
                description = request.Examinable.Description.GetDescription();

            if (request.Examinable.Commands?.Any() ?? false)
            {
                if (!string.IsNullOrEmpty(description))
                    description += " ";

                description += $"{Environment.NewLine}{Environment.NewLine}{request.Examinable.Identifier.Name} provides the following commands: ";

                foreach (var customCommand in request.Examinable.Commands)
                    description += $"{Environment.NewLine}\"{customCommand.Help.Command}\" - {customCommand.Help.Description.RemoveSentenceEnd()}, ";

                if (description.EndsWith(", "))
                {
                    description = description.Remove(description.Length - 2);
                    description.EnsureFinishedSentence();
                }
            }

            if (string.IsNullOrEmpty(description))
                description = request.Examinable.Identifier.Name;

            if (string.IsNullOrEmpty(description))
                description = request.Examinable.GetType().Name;

            if (request.Examinable.Attributes.Count > 0)
                description += $"\n\n{StringUtilities.ConstructAttributesAsString(request.Examinable.Attributes.GetAsDictionary())}";

            return new ExaminationResult(description);
        };

        #endregion

        #region Overrides of Object

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return Identifier.Name;
        }

        #endregion

        #region IExaminable Members

        /// <summary>
        /// Get this objects identifier.
        /// </summary>
        public Identifier Identifier { get; protected set; }

        /// <summary>
        /// Get or set a description of this object.
        /// </summary>
        public Description Description { get; set; }

        /// <summary>
        /// Get or set this objects commands.
        /// </summary>
        public CustomCommand[] Commands { get; set; }

        /// <summary>
        /// Get the attribute manager for this object.
        /// </summary>
        public AttributeManager Attributes { get; } = new AttributeManager();

        /// <summary>
        /// Examine this object.
        /// </summary>
        /// <param name="scene">The scene this object is being examined from.</param>
        /// <returns>A ExaminationResult detailing the examination of this object.</returns>
        public virtual ExaminationResult Examine(ExaminationScene scene)
        {
            return Examination(new ExaminationRequest(this, scene));
        }

        #endregion

        #region Implementation of IPlayerVisible

        /// <summary>
        /// Get or set if this is visible to the player.
        /// </summary>
        public bool IsPlayerVisible { get; set; } = true;

        #endregion
    }
}