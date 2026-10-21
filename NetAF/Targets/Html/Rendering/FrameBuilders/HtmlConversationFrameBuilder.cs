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

        /// <summary>
        /// Get or set if the log is rendered.
        /// </summary>
        public bool RenderLog { get; set; }

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

        /// <inheritdoc/>
        public IFrame Build(string title, IConverser converser, CommandHelp[] contextualCommands, Size size)
        {
            builder.Clear();

            if (!string.IsNullOrEmpty(title))
                builder.H1(title);

            Participant? lastParticipant = null;

            if (RenderLog)
            {
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
            }
            else
            {
                builder.H3(converser.Identifier.Name);

                var line = converser?.Conversation?.CurrentParagraph?.Line ?? string.Empty;
                builder.P(line);
            }

            AppendCommands(builder, CommandTitle, contextualCommands);

            return new HtmlFrame(builder);
        }

        #endregion
    }
}
