using NetAF.Assets;
using NetAF.Assets.Characters;
using NetAF.Commands;
using NetAF.Conversations;
using NetAF.Extensions;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;
using System;
using System.Linq;

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

            if (converser?.Conversation?.Log != null)
            {
                foreach (var log in converser.Conversation.Log)
                {
                    switch (log.Participant)
                    {
                        case Participant.Player:
                            builder.P("You: " + log.Line);
                            break;
                        case Participant.Other:
                            builder.P($"{converser.Identifier.Name}: " + log.Line);
                            break;
                        default:
                            throw new NotImplementedException();
                    }
                }
            }

            if (contextualCommands?.Any() ?? false)
            {
                builder.H4(CommandTitle);

                foreach (var contextualCommand in contextualCommands)
                    builder.P($"{contextualCommand.DisplayCommand} - {contextualCommand.Description.EnsureFinishedSentence()}");
            }

            return new HtmlFrame(builder);
        }

        #endregion
    }
}
