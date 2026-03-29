namespace NetAF.Targets.Console.Rendering
{
    /// <summary>
    /// Represents any object that can vary a color.
    /// </summary>
    public interface IVariationGenerator
    {
        /// <summary>
        /// Vary a color.
        /// </summary>
        /// <param name="color">The color to vary.</param>
        /// <returns>The varied color</returns>
        AnsiColor Vary(AnsiColor color);
    }
}
