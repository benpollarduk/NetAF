using NetAF.Assets;
using NetAF.Commands;
using NetAF.Extensions;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;
using System.Text;

namespace NetAF.Targets.Text.Rendering.FrameBuilders
{
    /// <summary>
    /// Provides a builder of help frames.
    /// </summary>
    /// <param name="builder">A builder to use for the text layout.</param>
    public sealed class TextHelpFrameBuilder(StringBuilder builder) : IHelpFrameBuilder
    {
        #region Implementation of IHelpFrameBuilder

        /// <inheritdoc/>
        public IFrame Build(string title, CommandHelp commandHelp, Prompt[] prompts, Size size)
        {
            builder.Clear();

            builder.AppendLine(title);
            builder.AppendLine();

            if (commandHelp != null)
            {
                builder.AppendLine($"Command: {commandHelp.Command}");

                if (!string.IsNullOrEmpty(commandHelp.Shortcut))
                    builder.AppendLine($"Shortcut: {commandHelp.Shortcut}");

                builder.AppendLine($"Description: {commandHelp.Description.EnsureFinishedSentence()}");

                if (!string.IsNullOrEmpty(commandHelp.Instructions))
                    builder.AppendLine($"Instructions: {commandHelp.Instructions.EnsureFinishedSentence()}");

                if (!string.IsNullOrEmpty(commandHelp.DisplayAs))
                    builder.AppendLine($"Example: {commandHelp.DisplayAs}");

                StringBuilder synonymBuilder = new();

                foreach (var synonym in commandHelp.Synonyms ?? [])
                    synonymBuilder.Append($"'{synonym}' ");

                var synonymString = synonymBuilder.ToString();

                if (!string.IsNullOrEmpty(synonymString))
                    builder.AppendLine($"Synonyms: {synonymString}");

                StringBuilder promptBuilder = new();

                foreach (var prompt in prompts ?? [])
                    promptBuilder.Append($"'{prompt.Entry}' ");

                var promptString = promptBuilder.ToString();

                if (!string.IsNullOrEmpty(promptString))
                    builder.AppendLine($"Prompts: {promptString}");
            }

            return new TextFrame(builder);
        }

        #endregion
    }
}
