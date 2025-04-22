using NetAF.Assets;
using NetAF.Assets.Characters;
using NetAF.Commands;
using NetAF.Conversations;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;
using System;
using System.Text;

namespace NetAF.Targets.Text.Rendering.FrameBuilders
{
    /// <summary>
    /// Provides a builder of conversation frames.
    /// </summary>
    /// <param name="builder">A builder to use for the text layout.</param>
    public sealed class TextConversationFrameBuilder(StringBuilder builder) : IConversationFrameBuilder
    {
        #region Properties

        /// <summary>
        /// Get or set the command title.
        /// </summary>
        public string CommandTitle { get; set; } = "You can:";

        #endregion

        #region StaticMethods

        /// <summary>
        /// Append commands.
        /// </summary>
        /// <param name="builder">The string builder.</param>
        /// <param name="commandTitle">The title for the command section.</param>
        /// <param name="commands">The commands.</param>
        private static void AppendCommands(StringBuilder builder, string commandTitle, CommandHelp[] commands)
        {
            if ((commands?.Length ?? 0) == 0)
                return;

            builder.AppendLine(commandTitle);

            foreach (var contextualCommand in commands)
                builder.AppendLine($"{contextualCommand.DisplayCommand} - {contextualCommand.Description}");
        }

        #endregion

        #region Implementation of IConversationFrameBuilder

        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="title">The title to display to the user.</param>
        /// <param name="converser">The converser.</param>
        /// <param name="contextualCommands">The contextual commands to display.</param>
        /// <param name="size">The size of the frame.</param>
        /// <returns>The frame.</returns>
        public IFrame Build(string title, IConverser converser, CommandHelp[] contextualCommands, Size size)
        {
            builder.Clear();

            if (!string.IsNullOrEmpty(title))
                builder.AppendLine(title);

            Participant? lastParticipant = null;

            if (converser?.Conversation?.Log != null)
            {
                foreach (var log in converser.Conversation.Log)
                {
                    if (lastParticipant != log.Participant)
                    {
                        if (lastParticipant != null)
                            builder.AppendLine();

                        switch (log.Participant)
                        {
                            case Participant.Player:
                                builder.AppendLine("You");
                                break;
                            case Participant.Other:
                                builder.AppendLine(converser.Identifier.Name);
                                break;
                            default:
                                throw new NotImplementedException();
                        }

                        lastParticipant = log.Participant;
                    }

                    builder.AppendLine(log.Line);
                }
            }

            AppendCommands(builder, CommandTitle, contextualCommands);

            return new TextFrame(builder);
        }

        #endregion
    }
}
