using System;
using NetAF.Assets.Locations;

namespace NetAF.Extensions
{
    /// <summary>
    /// Provides extension versions for Directions.
    /// </summary>
    public static class DirectionExtensions
    {
        /// <summary>
        /// Get an inverse direction.
        /// </summary>
        /// <param name="value">The direction.</param>
        /// <returns>The inverse direction.</returns>
        public static Direction Inverse(this Direction value)
        {
            return value switch
            {
                Direction.North => Direction.South,
                Direction.East => Direction.West,
                Direction.South => Direction.North,
                Direction.West => Direction.East,
                Direction.Up => Direction.Down,
                Direction.Down => Direction.Up,
                _ => throw new ArgumentOutOfRangeException(nameof(value), value, null),
            };
        }
    }
}
