using System;

namespace NetAF.Assets
{
    /// <summary>
    /// Represents a three-dimensional point.
    /// </summary>
    /// <param name="x">The x position.</param>
    /// <param name="y">The y position.</param>
    /// <param name="z">The z position.</param>
    public readonly struct Point3D(int x, int y, int z) : IEquatable<Point3D>
    {
        #region Properties

        /// <summary>
        /// Get the X position.
        /// </summary>
        public int X { get; } = x;

        /// <summary>
        /// Get the Y position.
        /// </summary>
        public int Y { get; } = y;

        /// <summary>
        /// Get the Z position.
        /// </summary>
        public int Z { get; } = z;

        #endregion

        #region Implementation of IEquatable<Point3D>

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.</returns>
        public bool Equals(Point3D other)
        {
            return X == other.X && Y == other.Y && Z == other.Z;
        }

        #endregion
    }
}
