﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetAF.Assets;
using NetAF.Extensions;

namespace NetAF.Utilities
{
    /// <summary>
    /// Provides a helper class for string interpretation.
    /// </summary>
    public static class StringUtilities
    {
        #region Constants

        /// <summary>
        /// Get the new line string.
        /// </summary>
        public const char Newline = '\n';

        /// <summary>
        /// Get the character for line feed.
        /// </summary>
        public const char LF = (char)10;

        /// <summary>
        /// Get the character for carriage return.
        /// </summary>
        public const char CR = (char)13;

        #endregion
        
        #region StaticMethods

        /// <summary>
        /// Preen input to remove any ambiguity around special characters.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>The preened input.</returns>
        public static string PreenInput(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

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
        public static string ExtractNextWordFromString(ref string input)
        {
            const char space = (char)32;

            var specialCharacters = new[] { space, LF };
            StringBuilder word = new();

            // prepare the start of the string
            input = input.TrimStart(space);

            foreach (var t in input)
            {
                // check for non-special characters
                if (!specialCharacters.Contains(t))
                {
                    word.Append(t);
                }
                else
                {
                    // check for newlines - these need to be added to the word
                    if (t == LF)
                        word.Append(t);

                    break;
                }
            }

            // trim the word from the input
            input = input.Remove(0, word.Length);

            // trim spaces and carriage returns from the word
            return word.ToString().Trim(space, CR);
        }
        
        /// <summary>
        /// Cut a line from a paragraph.
        /// </summary>
        /// <param name="paragraph">The paragraph.</param>
        /// <param name="maxWidth">The max line length.</param>
        /// <returns>The line cut from the paragraph.</returns>
        public static string CutLineFromParagraph(ref string paragraph, int maxWidth)
        {
            StringBuilder chunk = new();
            var originalParagraph = paragraph;

            while (chunk.Length < maxWidth)
            {
                var word = ExtractNextWordFromString(ref paragraph);

                if (string.IsNullOrEmpty(word))
                    break;

                // maybe the input was one solid line and nothing could be cut out...
                if (chunk.Length == 0 && word == originalParagraph && originalParagraph.Length >= maxWidth)
                {
                    chunk.Append(originalParagraph.AsSpan(0, maxWidth));
                    paragraph = originalParagraph.Remove(0, maxWidth);
                    break;
                }

                if (chunk.Length + word.Length > maxWidth)
                {
                    paragraph = word + " " + paragraph;
                    break;
                }

                if (!string.IsNullOrEmpty(word) && !word.EndsWith(Newline))
                    word += " ";

                chunk.Append(word);

                if (word.EndsWith(Newline))
                    break;
            }

            return chunk.ToString().TrimEnd();
        }

        /// <summary>
        /// Construct a sentence describing a series of examinables.
        /// </summary>
        /// <param name="examinables">The examinables.</param>
        /// <returns>The sentence.</returns>
        public static string ConstructExaminablesAsSentence(IExaminable[] examinables)
        {
            if (!examinables.Any())
                return string.Empty;

            StringBuilder builder = new();
            var examinableNames = (from i in examinables where i.IsPlayerVisible select i.Identifier).Select(x => x.Name).ToList();

            if (examinableNames.Count == 1)
                return $"{examinableNames[0].GetObjectifier().ToSentenceCase()} {examinableNames[0]}.";

            for (var i = 0; i < examinableNames.Count; i++)
            {
                var examinable = examinableNames[i];

                if ((i == 0) && (examinableNames.Count > 2))
                    builder.Append($"{examinable.GetObjectifier().ToSentenceCase()} {examinable}, ");
                else if (i == 0)
                    builder.Append($"{examinable.GetObjectifier().ToSentenceCase()} {examinable} ");
                else if (i < examinableNames.Count - 2)
                    builder.Append($"{examinable.GetObjectifier()} {examinable}, ");
                else if (i < examinableNames.Count - 1)
                    builder.Append($"{examinable.GetObjectifier()} {examinable} ");
                else
                    builder.Append($"and {examinable.GetObjectifier()} {examinable}.");
            }

            return builder.ToString();
        }

        /// <summary>
        /// Construct a line describing a series of attributes.
        /// </summary>
        /// <param name="attributes">The attributes.</param>
        /// <returns>The sentence.</returns>
        public static string ConstructAttributesAsString(Dictionary<Assets.Attributes.Attribute, int> attributes)
        {
            if (attributes?.Any() != true)
                return string.Empty;

            StringBuilder builder = new();

            for (var i = 0; i < attributes.Count; i++)
                builder.Append($"{attributes.Keys.ElementAt(i).Name}: {attributes.Values.ElementAt(i)}{(i < attributes.Count - 1 ? "\t" : string.Empty)}");

            return builder.ToString();
        }

        /// <summary>
        /// Split text in to a verb and a noun.
        /// </summary>
        /// <param name="text">The text to split.</param>
        /// <param name="verb">The verb.</param>
        /// <param name="noun">The noun.</param>
        public static void SplitTextToVerbAndNoun(string text, out string verb, out string noun)
        {
            // if there is a space
            if (text.IndexOf(" ", StringComparison.Ordinal) > -1)
            {
                // verb all text up to space
                verb = text.Substring(0, text.IndexOf(" ", StringComparison.Ordinal)).Trim();

                // noun is all text after space
                noun = text.Substring(text.IndexOf(" ", StringComparison.Ordinal)).Trim();
            }
            else
            {
                verb = text;
                noun = string.Empty;
            }
        }

        /// <summary>
        /// Concatenate a collection of strings.
        /// </summary>
        /// <param name="values">The collection of strings.</param>
        /// <param name="delimiter">An optional delimiter to used between each string.</param>
        /// <returns>The concatenated string.</returns>
        public static string Concatenate(string[] values, string delimiter = "")
        {
            StringBuilder builder = new();

            for (var i = 0; i < values.Length; i++)
            {
                builder.Append(values[i]);

                if (i < values.Length - 1)
                    builder.Append(delimiter);
            }

            return builder.ToString();
        }

        #endregion
    }
}
