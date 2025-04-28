using NetAF.Assets;
using NetAF.Extensions;
using NetAF.Logic;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;
using NetAF.Utilities;
using System.Text;

namespace NetAF.Targets.Text.Rendering.FrameBuilders
{
    /// <summary>
    /// Provides a builder of about frames.
    /// </summary>
    /// <param name="builder">A builder to use for the text layout.</param>
    public sealed class TextAboutFrameBuilder(StringBuilder builder) : IAboutFrameBuilder
    {
        #region Implementation of IAboutFrameBuilder

        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="game">The game.</param>
        /// <param name="size">The size of the frame.</param>
        /// <returns>The frame.</returns>
        public IFrame Build(string title, Game game, Size size)
        {
            builder.Clear();

            builder.AppendLine(title);
            builder.AppendLine();
            builder.AppendLine(game.Info.Name);
            builder.AppendLine(game.Info.Description.EnsureFinishedSentence());

            if (!string.IsNullOrEmpty(game.Info.Author))
                builder.AppendLine($"Created by: {game.Info.Author}".EnsureFinishedSentence());
            else
                builder.AppendLine(Info.AboutNetAF);

            return new TextFrame(builder);
        }

        #endregion
    }
}
