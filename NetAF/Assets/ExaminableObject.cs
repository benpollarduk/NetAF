using System;
using System.Linq;
using System.Text;
using NetAF.Assets.Attributes;
using NetAF.Commands;
using NetAF.Extensions;
using NetAF.Serialization.Assets;
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
        /// Get the callback handling all examination of this object.
        /// </summary>
        public ExaminationCallback Examination { get; protected set; } = request =>
        {
            StringBuilder description = new();

            if (request.Examinable.Description != null)
                description.Append(request.Examinable.Description.GetDescription());

            if (request.Examinable.Commands?.Any() ?? false)
            {
                if (description.Length > 0)
                    description.Append(" ");

                description.Append($"{Environment.NewLine}{Environment.NewLine}{request.Examinable.Identifier.Name} provides the following commands: ");

                for (int i = 0; i < request.Examinable.Commands.Length; i++)
                {
                    CustomCommand customCommand = request.Examinable.Commands[i];
                    description.Append($"{Environment.NewLine}\"{customCommand.Help.Command}\" - {customCommand.Help.Description.RemoveSentenceEnd()}, ");
                }

                if (description.ToString().EndsWith(", "))
                {
                    description.Remove(description.Length - 2, 2);
                    description.EnsureFinishedSentence();
                }
            }

            if (description.Length == 0)
                description.Append(request.Examinable.Identifier.Name);

            if (description.Length == 0)
                description.Append(request.Examinable.GetType().Name);

            if (request.Examinable.Attributes.Count > 0)
                description.Append($"\n\n{StringUtilities.ConstructAttributesAsString(request.Examinable.Attributes.GetAsDictionary())}");

            return new(description.ToString());
        };

        #endregion

        #region Implementation of IExaminable

        /// <summary>
        /// Get this objects identifier.
        /// </summary>
        public Identifier Identifier { get; protected set; }

        /// <summary>
        /// Get a description of this object.
        /// </summary>
        public Description Description { get; protected set; }

        /// <summary>
        /// Get this objects commands.
        /// </summary>
        public CustomCommand[] Commands { get; protected set; }

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
            return Examination(new(this, scene));
        }

        #endregion

        #region Implementation of IPlayerVisible

        /// <summary>
        /// Get or set if this is visible to the player.
        /// </summary>
        public bool IsPlayerVisible { get; set; } = true;

        #endregion

        #region Implementation of IRestoreFromObjectSerialization<ExaminableSerialization>

        /// <summary>
        /// Restore this object from a serialization.
        /// </summary>
        /// <param name="serialization">The serialization to restore from.</param>
        public void RestoreFrom(ExaminableSerialization serialization)
        {
            IsPlayerVisible = serialization.IsPlayerVisible;
            Attributes.RestoreFrom(serialization.AttributeManager);

            foreach (var command in serialization.Commands)
            {
                var match = Array.Find(Commands, x => x.Help.Command.Equals(command.Command));
                match?.RestoreFrom(command);
            }
        }

        #endregion
    }
}