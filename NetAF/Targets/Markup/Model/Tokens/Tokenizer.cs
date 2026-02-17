using System.Collections.Generic;
using System.Text;

namespace NetAF.Targets.Markup.Model.Tokens
{
    /// <summary>
    /// Provides functionality to convert a string into a collection of tokens.
    /// </summary>
    internal static class Tokenizer
    {
        #region StaticMethods

        /// <summary>
        /// Tokenize a string.
        /// </summary>
        /// <param name="input">The string to tokenize.</param>
        /// <returns>A collection of tokens.</returns>
        internal static IEnumerable<Token> Tokenize(string input)
        {
            var i = 0;

            // iterate through entire input
            while (IsNotEnd(input, i))
            {
                // look for headings (must be at start of line)
                if (IsHeadingStart(input, i))
                {
                    StringBuilder builder = new();

                    // could be multiple #'s
                    while (IsNotEnd(input, i) && IsHeading(input, i))
                    {
                        builder.Append(MarkupSyntax.Heading);
                        i++;
                    }

                    // skip space
                    if (IsNotEnd(input, i) && IsSpace(input, i))
                        i++;

                    yield return new Token(TokenType.Heading, builder.ToString());
                    continue;
                }

                // tags
                if (IsOpenTag(input, i))
                {
                    // find the next close tag
                    int nextOpen = input.IndexOf(MarkupSyntax.OpenTag, i + 1);
                    int end = input.IndexOf(MarkupSyntax.CloseTag, i);

                    // if there's another open before the next close, the current open must be text instead of an open
                    if (end > i && (nextOpen == -1 || nextOpen > end))
                    {
                        var tag = input.Substring(i + 1, end - i - 1);

                        if (tag.StartsWith(MarkupSyntax.EndTag))
                            yield return new Token(TokenType.CloseTag, tag[1..]);
                        else
                            yield return new Token(TokenType.OpenTag, tag);

                        i = end + 1;
                        continue;
                    }
                    else
                    {
                        // no valid close found for this close so it must be text
                        yield return new Token(TokenType.Text, input[i].ToString());
                        i++;
                        continue;
                    }
                }

                // new line
                if (IsNewline(input, i))
                {
                    yield return new Token(TokenType.NewLine, MarkupSyntax.NewLine.ToString());
                    i++;
                    continue;
                }

                // text
                int start = i;

                while (IsNotEnd(input, i) && !IsOpenTag(input, i) && !IsNewline(input, i))
                    i++;

                yield return new Token(TokenType.Text, input[start..i]);
            }
        }

        private static bool IsHeadingStart(string input, int characterIndex)
        {
            return (characterIndex == 0 || input[characterIndex - 1] == MarkupSyntax.NewLine) && input[characterIndex] == MarkupSyntax.Heading;
        }

        private static bool IsOpenTag(string input, int characterIndex)
        {
            return input[characterIndex] == MarkupSyntax.OpenTag;
        }

        private static bool IsNewline(string input, int characterIndex)
        {
            return input[characterIndex] == MarkupSyntax.NewLine;
        }

        private static bool IsSpace(string input, int characterIndex)
        {
            return input[characterIndex] == MarkupSyntax.Space;
        }

        private static bool IsHeading(string input, int characterIndex)
        {
            return input[characterIndex] == MarkupSyntax.Heading;
        }

        private static bool IsNotEnd(string input, int characterIndex)
        {
            return characterIndex < input.Length;
        }

        #endregion
    }
}
