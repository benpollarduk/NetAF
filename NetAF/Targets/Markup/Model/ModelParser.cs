using NetAF.Targets.Markup.Model.Nodes;
using NetAF.Targets.Markup.Model.Tokens;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Linq;

namespace NetAF.Targets.Markup.Model
{
    /// <summary>
    /// Provides parsing functionality between various formats and the abstract syntax tree.
    /// </summary>
    public static class ModelParser
    {
        #region StaticMethods

        /// <summary>
        /// Try and parse a string to a DocumentNode.
        /// </summary>
        /// <param name="input">The input to try and parse.</param>
        /// <param name="documentNode">The parsed document node.</param>
        /// <returns>True of the parse was successful, else false.</returns>
        public static bool TryParse(string input, out DocumentNode documentNode)
        {
            var tokens = Tokenizer.Tokenize(input);
            return TryParse(tokens, out documentNode);
        }

        /// <summary>
        /// Try and parse a collection of tokens to a DocumentNode.
        /// </summary>
        /// <param name="tokens">The tokens to try and parse.</param>
        /// <param name="documentNode">The parsed document node.</param>
        /// <returns>True of the parse was successful, else false.</returns>
        internal static bool TryParse(IEnumerable<Token> tokens, out DocumentNode documentNode)
        {
            documentNode = new DocumentNode();

            try
            {
                var inlineStack = new Stack<List<IInlineNode>>();
                var styleStack = new Stack<TextStyle>();
                var currentParagraph = new ParagraphNode();
                documentNode.Blocks.Add(currentParagraph);
                inlineStack.Push(currentParagraph.Inlines);

                using var e = tokens.GetEnumerator();

                while (e.MoveNext())
                {
                    var token = e.Current;

                    switch (token.Type)
                    {
                        case TokenType.Text:

                            // add text to the current deepest nested style span or paragraph
                            inlineStack.Peek().Add(new TextNode(token.Tag));

                            break;

                        case TokenType.OpenTag:

                            var style = GetStyleFromToken(token.Tag);
                            styleStack.Push(style);
                            var span = new StyleSpanNode(style);
                            inlineStack.Peek().Add(span);

                            // push the span's inlines so subsequent text goes inside it
                            inlineStack.Push(span.Inlines);

                            break;

                        case TokenType.CloseTag:

                            // pop the stack to return to the parent style or the base paragraph
                            if (inlineStack.Count > 1)
                            {
                                inlineStack.Pop();
                                styleStack.Pop();
                            }

                            break;

                        case TokenType.NewLine:

                            // a new line terminates the current paragraph and starts a fresh one
                            currentParagraph = new ParagraphNode();
                            documentNode.Blocks.Add(currentParagraph);
                            inlineStack.Clear();
                            inlineStack.Push(currentParagraph.Inlines);

                            // if there are active styles, re-open them in the new paragraph
                            // iterate bottom-up to maintain the correct nesting order
                            foreach (var activeStyle in styleStack.Reverse())
                            {
                                var carryOverSpan = new StyleSpanNode(activeStyle);
                                inlineStack.Peek().Add(carryOverSpan);
                                inlineStack.Push(carryOverSpan.Inlines);
                            }

                            break;

                        case TokenType.Heading:

                            var text = new StringBuilder();

                            // consume tokens until the end of the heading line
                            while (e.MoveNext() && e.Current.Type != TokenType.NewLine)
                            {
                                if (e.Current.Type == TokenType.Text)
                                    text.Append(e.Current.Tag);
                            }

                            documentNode.Blocks.Add(
                                new HeadingNode(text.ToString(), ParseHeadingLevel(token.Tag))
                            );

                            // after a heading, prepare a new paragraph for subsequent content
                            currentParagraph = new ParagraphNode();
                            documentNode.Blocks.Add(currentParagraph);
                            inlineStack.Clear();
                            styleStack.Clear(); // headings usually break the style flow
                            inlineStack.Push(currentParagraph.Inlines);

                            break;
                    }
                }

                TidyDocumentNode(documentNode);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception caught parsing markup: {ex.Message}");
                return false;
            }

            return true;
        }

        private static void TidyDocumentNode(DocumentNode documentNode)
        {
            // remove the first paragraph if it ended up being empty
            if (documentNode.Blocks.Count != 0 &&
                documentNode.Blocks[0] is ParagraphNode first &&
                first.Inlines.Count == 0)
            {
                documentNode.Blocks.Remove(first);
            }

            // remove the last paragraph if it ended up being empty
            if (documentNode.Blocks.Count != 0 &&
                documentNode.Blocks[^1] is ParagraphNode last &&
                last.Inlines.Count == 0)
            {
                documentNode.Blocks.Remove(last);
            }
        }

        private static Color GetColorFromHex(string hex)
        {
            if (string.IsNullOrWhiteSpace(hex)) return null;

            if (!hex.StartsWith('#'))
                hex = $"#{hex}";

            return Color.FromHtml(hex);
        }

        private static TextStyle GetStyleFromToken(string tag)
        {
            if (string.IsNullOrEmpty(tag)) return new TextStyle();

            if (tag.Equals(MarkupSyntax.Bold, StringComparison.InvariantCultureIgnoreCase))
                return new TextStyle(Bold: true);

            if (tag.Equals(MarkupSyntax.Italic, StringComparison.InvariantCultureIgnoreCase))
                return new TextStyle(Italic: true);

            if (tag.Equals(MarkupSyntax.Strikethrough, StringComparison.InvariantCultureIgnoreCase))
                return new TextStyle(Strikethrough: true);

            if (tag.Equals(MarkupSyntax.Underline, StringComparison.InvariantCultureIgnoreCase))
                return new TextStyle(Underline: true);

            if (tag.Equals(MarkupSyntax.Monospace, StringComparison.InvariantCultureIgnoreCase))
                return new TextStyle(Monospace: true);

            var foregoundWithDelimiter = MarkupSyntax.Foregound + MarkupSyntax.Delimiter;
            var bakgroundWithDelimiter = MarkupSyntax.Background + MarkupSyntax.Delimiter;

            if (tag.StartsWith(foregoundWithDelimiter, StringComparison.InvariantCultureIgnoreCase))
            {
                var hex = tag[foregoundWithDelimiter.Length..].Trim();
                var color = GetColorFromHex(hex);
                return new TextStyle(Foreground: color);
            }

            if (tag.StartsWith(bakgroundWithDelimiter, StringComparison.InvariantCultureIgnoreCase))
            {
                var hex = tag[bakgroundWithDelimiter.Length..].Trim();
                var color = GetColorFromHex(hex);
                return new TextStyle(Background: color);
            }

            return new TextStyle();
        }

        private static HeadingLevel ParseHeadingLevel(string heading)
        {
            if (string.IsNullOrEmpty(heading)) return HeadingLevel.H1;

            int level = 0;
            int i = 0;

            while (i < heading.Length && heading[i] == MarkupSyntax.Heading)
            {
                level++;
                i++;
            }

            return level switch
            {
                1 => HeadingLevel.H1,
                2 => HeadingLevel.H2,
                3 => HeadingLevel.H3,
                _ => HeadingLevel.H4
            };
        }

        #endregion
    }
}