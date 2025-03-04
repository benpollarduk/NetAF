using NetAF.Rendering;

namespace NetAF.Targets.Console.Rendering
{
    /// <summary>
    /// Represents any object that is an ANSI based grid frame.
    /// </summary>
    public interface IAnsiGridFrame : IFrame
    {
        /// <summary>
        /// Get a cell from the grid.
        /// </summary>
        /// <param name="x">The x position of the cell.</param>
        /// <param name="y">The y position of the cell.</param>
        /// <returns>The ANSI cell.</returns>
        AnsiCell GetCell(int x, int y);
    }
}
