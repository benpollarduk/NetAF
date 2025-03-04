namespace NetAF.Targets.Console.Rendering
{
    /// <summary>
    /// Represents a cell containing ANSI data.
    /// </summary>
    /// <param name="Character">The character contained in the cell.</param>
    /// <param name="Foreground">The foreground of the cell.</param>
    /// <param name="Background">The background of the cell.</param>
    public record AnsiCell(char Character, AnsiColor Foreground, AnsiColor Background);
}
