using System.Collections.Generic;
using System.Text;

namespace NetAF.Targets.Markup.Ast.Tokens
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
            while (i < input.Length)
            {
                // look for headings (must be at start of line)
                if ((i == 0 || input[i - 1] == MarkupSyntax.NewLine) && input[i] == MarkupSyntax.Heading)
                {
                    StringBuilder builder = new();

                    // could be multiple #'s
                    while (i < input.Length && input[i] == MarkupSyntax.Heading)
                    {
                        builder.Append(MarkupSyntax.Heading);
                        i++;
                    }

                    // skip space
                    if (i < input.Length && input[i] == MarkupSyntax.Space)
                        i++;

                    yield return new Token(TokenType.Heading, builder.ToString());
                    continue;
                }

                // tags
                if (input[i] == MarkupSyntax.OpenTag)
                {
                    int end = input.IndexOf(MarkupSyntax.CloseTag, i);

                    if (end > i)
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
                        // no close
                        yield return new Token(TokenType.Text, input[i].ToString());
                        i++;
                        continue;
                    }
                }

                // new line
                if (input[i] == MarkupSyntax.NewLine)
                {
                    yield return new Token(TokenType.NewLine, MarkupSyntax.NewLine.ToString());
                    i++;
                    continue;
                }

                // text
                int start = i;

                while (i < input.Length && input[i] != MarkupSyntax.OpenTag && input[i] != MarkupSyntax.NewLine)
                    i++;

                yield return new Token(TokenType.Text, input[start..i]);
            }
        }

        #endregion
    }
}
