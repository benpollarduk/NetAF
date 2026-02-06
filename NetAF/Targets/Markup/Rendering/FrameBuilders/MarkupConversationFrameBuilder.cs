using NetAF.Assets;
using NetAF.Assets.Characters;
using NetAF.Commands;
using NetAF.Conversations;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;
using System;

namespace NetAF.Targets.Markup.Rendering.FrameBuilders
{
    /// <summary>
    /// Provides a builder of conversation frames.
    /// </summary>
    /// <param name="builder">A builder to use for the text layout.</param>
    public sealed class MarkupConversationFrameBuilder(MarkupBuilder builder) : IConversationFrameBuilder
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
        /// <param name="builder">The HTML builder.</param>
        /// <param name="commandTitle">The title for the command section.</param>
        /// <param name="commands">The commands.</param>
        private static void AppendCommands(MarkupBuilder builder, string commandTitle, CommandHelp[] commands)
        {
            if ((commands?.Length ?? 0) == 0)
                return;

            builder.Heading(commandTitle, HeadingLevel.H2);

            foreach (var contextualCommand in commands)
            {
                builder.Write(contextualCommand.DisplayCommand, new TextStyle(Bold: true));
                builder.WriteLine($" - {contextualCommand.Description}");
            }
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
                builder.Heading(title, HeadingLevel.H1);

            Participant? lastParticipant = null;

            if (converser?.Conversation?.Log != null)
            {
                foreach (var log in converser.Conversation.Log)
                {
                    if (lastParticipant != log.Participant)
                    {
                        if (lastParticipant != null)
                            builder.Newline();

                        switch (log.Participant)
                        {
                            case Participant.Player:
                                builder.Heading("You", HeadingLevel.H2);
                                break;
                            case Participant.Other:
                                builder.Heading(converser.Identifier.Name, HeadingLevel.H2);
                                break;
                            default:
                                throw new NotImplementedException();
                        }

                        lastParticipant = log.Participant;
                    }

                    builder.WriteLine(log.Line);
                }
            }

            AppendCommands(builder, CommandTitle, contextualCommands);

            return new MarkupFrame(builder);
        }

        #endregion
    }
}
