using NetAF.Assets;
using NetAF.Assets.Characters;
using NetAF.Commands;
using NetAF.Conversations;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;
using System;

namespace NetAF.Targets.Html.Rendering.FrameBuilders
{
    /// <summary>
    /// Provides a builder of conversation frames.
    /// </summary>
    /// <param name="builder">A builder to use for the text layout.</param>
    public sealed class HtmlConversationFrameBuilder(HtmlBuilder builder) : IConversationFrameBuilder
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
        private static void AppendCommands(HtmlBuilder builder, string commandTitle, CommandHelp[] commands)
        {
            if ((commands?.Length ?? 0) == 0)
                return;

            builder.H4(commandTitle);

            foreach (var contextualCommand in commands)
                builder.P($"{contextualCommand.DisplayCommand} - {contextualCommand.Description}");
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
                builder.H1(title);

            Participant? lastParticipant = null;

            if (converser?.Conversation?.Log != null)
            {
                foreach (var log in converser.Conversation.Log)
                {
                    if (lastParticipant != log.Participant)
                    {
                        if (lastParticipant != null)
                            builder.Br();

                        switch (log.Participant)
                        {
                            case Participant.Player:
                                builder.H3("You");
                                break;
                            case Participant.Other:
                                builder.H3(converser.Identifier.Name);
                                break;
                            default:
                                throw new NotImplementedException();
                        }

                        lastParticipant = log.Participant;
                    }

                    builder.P(log.Line);
                }
            }

            AppendCommands(builder, CommandTitle, contextualCommands);

            return new HtmlFrame(builder);
        }

        #endregion
    }
}
