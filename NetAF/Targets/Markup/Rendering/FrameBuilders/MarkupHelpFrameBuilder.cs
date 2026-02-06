using NetAF.Assets;
using NetAF.Commands;
using NetAF.Extensions;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;
using System.Text;

namespace NetAF.Targets.Markup.Rendering.FrameBuilders
{
    /// <summary>
    /// Provides a builder of help frames.
    /// </summary>
    /// <param name="builder">A builder to use for the text layout.</param>
    public sealed class MarkupHelpFrameBuilder(MarkupBuilder builder) : IHelpFrameBuilder
    {
        #region Implementation of IHelpFrameBuilder

        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="commandHelp">The command help.</param>
        /// <param name="prompts">The prompts to display for the command.</param>
        /// <param name="size">The size of the frame.</param>
        /// <returns>The frame.</returns>
        public IFrame Build(string title, CommandHelp commandHelp, Prompt[] prompts, Size size)
        {
            builder.Clear();

            builder.Heading(title, HeadingLevel.H1);
            builder.Newline();

            if (commandHelp != null)
            {
                builder.WriteLine($"Command: {commandHelp.Command}");

                if (!string.IsNullOrEmpty(commandHelp.Shortcut))
                    builder.WriteLine($"Shortcut: {commandHelp.Shortcut}");

                builder.WriteLine($"Description: {commandHelp.Description.EnsureFinishedSentence()}");

                if (!string.IsNullOrEmpty(commandHelp.Instructions))
                    builder.WriteLine($"Instructions: {commandHelp.Instructions.EnsureFinishedSentence()}");

                if (!string.IsNullOrEmpty(commandHelp.DisplayAs))
                    builder.WriteLine($"Example: {commandHelp.DisplayAs}");

                StringBuilder promptBuilder = new();

                foreach (var prompt in prompts ?? [])
                    promptBuilder.Append($"'{prompt.Entry}' ");

                var promptString = promptBuilder.ToString();

                if (!string.IsNullOrEmpty(promptString))
                    builder.WriteLine($"Prompts: {promptString}");
            }

            return new MarkupFrame(builder);
        }

        #endregion
    }
}
