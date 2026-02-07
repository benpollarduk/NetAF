# Markup

## Introduction
Markup allows rich text frames to be described in a simple target agnostic language and then rendered in various formats.

## Process
NetAF provides the **MarkupBuilder** to simplify generating markup for frames and the **MarkupAdapter** for rendering them in a project. This allows custom implementations of **IFramePresenter** to be written to render to different targets.

When writing a custom implementation of **IFramePresenter** it is advisable to first use the **Tokenizer** to tokenize the markup and then the tokens can be passed to the **MarkupParser** to parse to generate a model. This model is much easier to work with and is essentially an abstract syntax tree. The root of the model is a **DocumentNode**. The model can be thought of as a simple DOM (Document Object Model).

*Markup -> Tokens -> Model -> Renderer*

The **ModelParser** allows markup to be parsed directly to a **DocumentNode**, handling all tokenizaton for you to simplify the process.

```csharp
ModelParser.TryParse(markup, out DocumentNode? doc);
```

## Syntax
The following syntax is supported.

```
# Example - heading 1.
## Example - heading 2.
### Example - heading 3.
#### Example - heading 4.

[bold]Example[/bold] - all text between the tags is bold.

[italic]Example[/italic] - all text between the tags is bold.

[stikethrough]Example[/stikethrough] - all text between the tags uses strikethrough.

[underline]Example[/underline] - all text between the tags uses underline.

[monospace]Example[/monospace] - all text between the tags should be monospace.

[foreground:#FFFFFF]Example[/foreground] - all text between the tags has a specified foreground color in hex, RRGGBB.

[background:#FFFFFF]Example[/background] - all text between the tags has a specified background color in hex, RRGGBB.

\n - newline.
```

# Example
```
# Markup Example

## This is a sub heading
This is a paragraph. So far the same as markdown.

[bold]Here is some text in bold![/bold] And some in [italic]italics[/italic]. And some [bold][italic]bold italics[/italic][/bold].

You can [underline]underline sections of text[/underline], and also [strikethrough]strike through sections[/strikethrough] if desired.

[foreground:#FF0000]This will be in red.[/foreground]

[background:#0000FF]And this will have a blue background.[/background]

Sometimes, particularly when rendering maps and other visuals monospace is needed to ensure the horizontal spacing looks correct.

[monospace]|---------|[/monospace]
```
