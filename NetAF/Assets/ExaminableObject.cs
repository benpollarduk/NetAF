using System;
using System.Linq;
using System.Text;
using NetAF.Assets.Attributes;
using NetAF.Commands;
using NetAF.Extensions;
using NetAF.Serialization;
using NetAF.Serialization.Assets;
using NetAF.Utilities;

namespace NetAF.Assets
{
    /// <summary>
    /// Represents an object that can be examined.
    /// </summary>
    public class ExaminableObject : IExaminable, IRestoreFromObjectSerialization<ExaminableSerialization>
    {
        #region StaticProperties

        /// <summary>
        /// Get a default examination for an ExaminableObject.
        /// </summary>
        public static ExaminationCallback DefaultExamination => request =>
        {
            StringBuilder description = new();

            if (request.Examinable.Description != null)
                description.Append(request.Examinable.Description.GetDescription());

            AddCommandsToDescription(request, ref description);
            EnsureAtleastABasicDescription(request, ref description);

            if (request.Examinable.Attributes.Count > 0)
                description.Append($"{StringUtilities.Newline}{StringUtilities.Newline}{StringUtilities.ConstructAttributesAsString(request.Examinable.Attributes.GetAsDictionary())}");

            return new(description.ToString());
        };

        #endregion

        #region Properties

        /// <summary>
        /// Get the callback handling all examination of this object.
        /// </summary>
        public ExaminationCallback Examination { get; protected set; }

        #endregion

        #region StaticMethods

        /// <summary>
        /// Ensure that at least a basic description has been added.
        /// </summary>
        /// <param name="request">The examination request.</param>
        /// <param name="description">The current description.</param>
        private static void EnsureAtleastABasicDescription(ExaminationRequest request, ref StringBuilder description)
        {
            if (description.Length == 0)
                description.Append(request.Examinable.Identifier.Name);

            if (description.Length == 0)
                description.Append(request.Examinable.GetType().Name);
        }

        /// <summary>
        /// Add any commands to the description.
        /// </summary>
        /// <param name="request">The examination request.</param>
        /// <param name="description">The current description.</param>
        private static void AddCommandsToDescription(ExaminationRequest request, ref StringBuilder description)
        {
            var commands = request.Examinable.Commands?.Where(x => x.IsPlayerVisible).ToArray() ?? [];

            if (commands.Length == 0)
                return;

            if (description.Length > 0)
                description.Append(' ');

            description.Append($"{Environment.NewLine}{Environment.NewLine}{request.Examinable.Identifier.Name} provides the following commands: ");

            for (int i = 0; i < commands.Length; i++)
            {
                CustomCommand customCommand = commands[i];
                description.Append($"{Environment.NewLine}\"{customCommand.Help.Command}\" - {customCommand.Help.Description.RemoveSentenceEnd()}, ");
            }

            if (description.ToString().EndsWith(", "))
            {
                description.Remove(description.Length - 2, 2);
                description.EnsureFinishedSentence();
            }
        }

        #endregion

        #region Implementation of IExaminable

        /// <summary>
        /// Get this objects identifier.
        /// </summary>
        public Identifier Identifier { get; protected set; }

        /// <summary>
        /// Get a description of this object.
        /// </summary>
        public IDescription Description { get; protected set; }

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
        /// <returns>The examination.</returns>
        public Examination Examine(ExaminationScene scene)
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
        void IRestoreFromObjectSerialization<ExaminableSerialization>.RestoreFrom(ExaminableSerialization serialization)
        {
            IsPlayerVisible = serialization.IsPlayerVisible;
            ((IRestoreFromObjectSerialization<AttributeManagerSerialization>)Attributes).RestoreFrom(serialization.AttributeManager);

            foreach (var command in serialization.Commands)
            {
                var match = Array.Find(Commands, x => x.Help.Command.Equals(command.CommandName));
                ((IRestoreFromObjectSerialization<CustomCommandSerialization>)match)?.RestoreFrom(command);
            }
        }

        #endregion
    }
}