using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetAF.Assets;
using NetAF.Assets.Characters;
using NetAF.Assets.Locations;
using NetAF.Extensions;
using NetAF.Logic;

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
        /// Preen output to remove any ambiguity around special characters. Special characters will be substituted to prevent rendering issues.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns>The input with all instances of tab replaced with spaces.</returns>
        public static string PreenOutput(string input)
        {
            var dictionary = new Dictionary<char, string>
            {
                { (char)0,  string.Empty }, // null
                { (char)7,  string.Empty }, // bell
                { (char)8,  string.Empty }, // backspace
                { (char)9,  "    " },       // horizontal tab
                { (char)11, "    " },       // vertical tab
            };

            foreach (var pair in dictionary)
                input = input.Replace(pair.Key.ToString(), pair.Value);

            return input;
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
            input = input[word.Length..];

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
                    paragraph = originalParagraph[maxWidth..];
                    break;
                }

                if (chunk.Length + word.Length > maxWidth)
                {
                    paragraph = word + " " + paragraph;
                    break;
                }

                if (!string.IsNullOrEmpty(word) && !word.EndsWith(Newline))
                    word = $"{word} ";

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
                builder.Append($"{attributes.Keys.ElementAt(i).Name}: {attributes.Values.ElementAt(i)}{(i < attributes.Count - 1 ? ", " : string.Empty)}");

            return builder.ToString();
        }

        /// <summary>
        /// Split text input into a command and argument.
        /// </summary>
        /// <param name="input">The input to split.</param>
        /// <param name="command">The command.</param>
        /// <param name="argument">The argument.</param>
        public static void SplitInputToCommandAndArgument(string input, out string command, out string argument)
        {
            // if there is a space
            if (input.IndexOf(' ') > -1)
            {
                // command is all text up to space
                command = input[..input.IndexOf(' ')].Trim();

                // argument is all text after space
                argument = input[input.IndexOf(' ')..].Trim();
            }
            else
            {
                command = input;
                argument = string.Empty;
            }
        }

        /// <summary>
        /// Split text input into a command and arguments.
        /// </summary>
        /// <param name="input">The input to split.</param>
        /// <param name="command">The command.</param>
        /// <param name="arguments">The arguments.</param>
        public static void SplitInputToCommandAndArguments(string input, out string command, out string[] arguments)
        {
            // split to words
            var words = input.Split(' ');

            if (words.Length == 0)
            {
                command = string.Empty;
                arguments = [];
            }
            else if (words.Length == 1)
            {
                command = words[0];
                arguments = [];
            }
            else
            {
                command = words[0];
                arguments = [.. words.Skip(1)];
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

        /// <summary>
        /// Find all keywords for a scene.
        /// </summary>
        /// <param name="scene">The examination scene.</param>
        /// <param name="viewPoint">The viewpoint.</param>
        /// <returns>An array of keywords.</returns>
        public static string[] FindKeywords(ExaminationScene scene = null, ViewPoint viewPoint = null)
        {
            string[] viewPointKeywords = viewPoint != null ? [.. viewPoint.Rooms.Where(x => x != null).Select(x => x.Identifier.Name).Concat(viewPoint.Directions.Select(x => x.ToString()))] : [];
            var examinableKeywords = scene != null ? Game.GetAllPlayerVisibleExaminables(scene.Examiner as PlayableCharacter, scene.Room).Select(x => x.Identifier.Name) : [];
            return [.. viewPointKeywords.Concat(examinableKeywords).Distinct()];
        }

        /// <summary>
        /// Locate all instances of a set of keywords in a string and return their positions as an array of StringMarkers.
        /// </summary>
        /// <param name="value">The string to search within.</param>
        /// <param name="keyWords">The keywords to locate.</param>
        /// <param name="caseSensitive">Whether the search should be case-sensitive.</param>
        /// <returns>An array of StringMarkers indicating the positions of the keywords.</returns>
        public static StringMarker[] LocateStringMarkers(string value, string[] keyWords, bool caseSensitive = false)
        {
            List<StringMarker> markers = new();

            var comparison = caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;

            foreach (var word in keyWords)
            {
                if (string.IsNullOrEmpty(word))
                    continue;

                var index = value.IndexOf(word, comparison);

                while (index != -1)
                {
                    if (IsWholeWordMatch(value, index, word.Length))
                        markers.Add(new StringMarker(word, index));

                    index = value.IndexOf(word, index + word.Length, comparison);
                }
            }

            return [.. markers];
        }

        /// <summary>
        /// Determine if a match at a given position within a string represents a whole word, as opposed to a substring of a larger word.
        /// </summary>
        /// <param name="value">The string being searched.</param>
        /// <param name="index">The start index of the match.</param>
        /// <param name="length">The length of the match.</param>
        /// <returns>True if the match is bordered by non-word characters (or the start/end of the string), otherwise false.</returns>
        private static bool IsWholeWordMatch(string value, int index, int length)
        {
            if (index > 0 && IsWordCharacter(value[index - 1]))
                return false;

            var endIndex = index + length;

            if (endIndex < value.Length && IsWordCharacter(value[endIndex]))
                return false;

            return true;
        }

        /// <summary>
        /// Determine if a character is considered part of a word.
        /// </summary>
        /// <param name="character">The character to check.</param>
        /// <returns>True if the character is a letter or digit, otherwise false.</returns>
        private static bool IsWordCharacter(char character)
        {
            return char.IsLetterOrDigit(character);
        }

        /// <summary>
        /// Determine which characters within a string should be highlighted based on a set of markers.
        /// </summary>
        /// <param name="value">The string being evaluated.</param>
        /// <param name="markers">The markers indicating which parts of the string to highlight.</param>
        /// <param name="caseSensitive">Whether the search should be case-sensitive.</param>
        /// <returns>An array of booleans, one per character in the string, indicating whether that character should be highlighted.</returns>
        public static bool[] GetHighlightMask(string value, StringMarker[] markers, bool caseSensitive = false)
        {
            var highlights = new bool[value?.Length ?? 0];

            if (string.IsNullOrEmpty(value) || markers == null || markers.Length == 0)
                return highlights;

            var comparison = caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;

            foreach (var marker in markers)
            {
                if (string.IsNullOrEmpty(marker.Value))
                    continue;

                var index = value.IndexOf(marker.Value, comparison);

                while (index != -1)
                {
                    if (IsWholeWordMatch(value, index, marker.Length))
                    {
                        for (var i = index; i < index + marker.Length && i < highlights.Length; i++)
                            highlights[i] = true;
                    }

                    index = value.IndexOf(marker.Value, index + marker.Length, comparison);
                }
            }

            return highlights;
        }

        #endregion
    }
}
