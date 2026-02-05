# Abstract Syntax Tree

## Introduction
The Abstract Syntax Tree complex frames to be described in a markup language and then rendered in various rich formats.

## Conversion Process
Markup -> Tokens -> AST

Frames are described as markup.
The markup is tokenized.
The tokens are parsed to AST.

## Markup
```
#, ##, ###, #### - headings 1 - 4.
[bold][/bold] - all text between the tags is bold.
[italic][/italic] - all text between the tags is bold.
[foreground:FFFFFF][foreground:FFFFFF] - all text between the tags has a specified foreground color in hex, RRGGBB.
[background:FFFFFF][background:FFFFFF] - all text between the tags has a specified background color in hex, RRGGBB.
```