﻿using System.Linq;

namespace BP.AdventureFramework.Utilities
{
    /// <summary>
    /// Provides a helper class for string interpretation.
    /// </summary>
    internal static class StringUtilities
    {
        /// <summary>
        /// Get the new line string.
        /// </summary>
        internal const string Newline = "\n";

        /// <summary>
        /// Get the character for line feed.
        /// </summary>
        internal const char LF = (char)10;

        /// <summary>
        /// Get the character for carriage return.
        /// </summary>
        internal const char CR = (char)13;

        /// <summary>
        /// Preen input to remove any ambiguity around special characters.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>The preened input.</returns>
        internal static string PreenInput(string input)
        {
            var output = input.Replace($"{CR}{LF}", $"{LF}");
            output = output.Replace($"{LF}{CR}", $"{LF}");
            output = output.Replace($"{CR}", $"{LF}");
            return output;
        }

        /// <summary>
        /// Extract the next word from a string. This will remove the word from the input string.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns>The extracted work.</returns>
        internal static string ExtractNextWordFromString(ref string input)
        {
            const char space = (char)32;

            var specialCharacters = new[] { space, LF };
            var word = string.Empty;

            // prepare the start of the string
            input = input.TrimStart(space);

            foreach (var t in input)
            {
                // check for non-special characters
                if (!specialCharacters.Contains(t))
                {
                    word += t;
                }
                else
                {
                    // check for newlines - these need to be added to the word
                    if (t == LF)
                        word += t;

                    break;
                }
            }

            // trim the word from the input
            input = input.Remove(0, word.Length);

            // trim spaces and carriage returns from the word
            return word.Trim(space, CR);
        }
        
        /// <summary>
        /// Cut a line from a paragraph.
        /// </summary>
        /// <param name="paragraph">The paragraph.</param>
        /// <param name="maxWidth">The max line length.</param>
        /// <returns>The line cut from the paragraph.</returns>
        internal static string CutLineFromParagraph(ref string paragraph, int maxWidth)
        {
            var chunk = string.Empty;

            while (chunk.Length < maxWidth)
            {
                var word = ExtractNextWordFromString(ref paragraph);

                if (string.IsNullOrEmpty(word))
                    break;

                if (chunk.Length + word.Length > maxWidth)
                {
                    paragraph = word + " " + paragraph;
                    break;
                }

                if (!string.IsNullOrEmpty(word) && !word.EndsWith(Newline))
                    word += " ";

                chunk += word;

                if (chunk.EndsWith(Newline))
                    break;
            }

            return chunk.TrimEnd();
        }
    }
}
