using System.IO;

namespace NetAF.Rendering.Presenter
{
    /// <summary>
    /// Represents a presenter for TextWriter.
    /// </summary>
    public sealed class TextWriterPresenter : IFramePresenter
    {
        #region Fields

        private readonly TextWriter writer;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the TextWriterPresenter class.
        /// </summary>
        /// <param name="writer">The writer.</param>
        public TextWriterPresenter(TextWriter writer)
        {
            this.writer = writer;
        }

        #endregion

        #region Overrides of Object

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return writer.ToString();
        }

        #endregion

        #region Implementation of IFramePresenter

        /// <summary>
        /// Write a character.
        /// </summary>
        /// <param name="value">The character to write.</param>
        public void Write(char value)
        {
            writer.Write(value);
        }

        /// <summary>
        /// Write a string.
        /// </summary>
        /// <param name="value">The string to write.</param>
        public void Write(string value)
        {
            writer.Write(value);
        }

        #endregion
    }
}
