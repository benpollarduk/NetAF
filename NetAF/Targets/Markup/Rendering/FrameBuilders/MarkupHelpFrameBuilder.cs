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

        /// <inheritdoc/>
        public IFrame Build(string title, CommandHelp commandHelp, Prompt[] prompts, Size size)
        {
            builder.Clear();

            builder.Heading(title, HeadingLevel.H1);
            builder.Newline();

            if (commandHelp != null)
            {
                var bold = new TextStyle(Bold: true);
                builder.Write("Command: ", bold);
                builder.WriteLine(commandHelp.Command);

                if (!string.IsNullOrEmpty(commandHelp.Shortcut))
                {
                    builder.Write("Shortcut: ", bold);
                    builder.WriteLine(commandHelp.Shortcut);
                }

                builder.Write("Description: ", bold);
                builder.WriteLine(commandHelp.Description.EnsureFinishedSentence());

                if (!string.IsNullOrEmpty(commandHelp.Instructions))
                {
                    builder.Write("Instructions: ", bold);
                    builder.WriteLine(commandHelp.Instructions.EnsureFinishedSentence());
                }

                if (!string.IsNullOrEmpty(commandHelp.DisplayAs))
                {
                    builder.Write("Example: ", bold);
                    builder.WriteLine(commandHelp.DisplayAs);
                }

                StringBuilder synonymBuilder = new();

                foreach (var synonym in commandHelp.Synonyms ?? [])
                    synonymBuilder.Append($"'{synonym}' ");

                var synonymString = synonymBuilder.ToString();

                if (!string.IsNullOrEmpty(synonymString))
                {
                    builder.Write("Synonyms: ", bold);
                    builder.WriteLine(synonymString);
                }

                StringBuilder promptBuilder = new();

                foreach (var prompt in prompts ?? [])
                    promptBuilder.Append($"'{prompt.Entry}' ");

                var promptString = promptBuilder.ToString();

                if (!string.IsNullOrEmpty(promptString))
                {
                    builder.Write("Prompts: ", bold);
                    builder.WriteLine(promptString);
                }
            }

            return new MarkupFrame(builder);
        }

        #endregion
    }
}
