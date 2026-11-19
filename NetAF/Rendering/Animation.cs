using NetAF.Assets;
using NetAF.Targets.Console.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NetAF.Rendering
{
    /// <summary>
    /// Represents an animation.
    /// </summary>
    /// <param name="Name">The name of the animation.</param>
    /// <param name="Description">A description of the animation.</param>
    /// <param name="VisualBuilders">The visual builders that create the animation.</param>
    /// <param name="Options">The animation options.</param>
    public record Animation(string Name, string Description, GridVisualBuilder[] VisualBuilders, AnimationOptions Options)
    {
        #region Properties

        /// <summary>
        /// Get the width.
        /// </summary>
        public int Width => VisualBuilders.Max(x => x.DisplaySize.Width);

        /// <summary>
        /// Get the height.
        /// </summary>
        public int Height => VisualBuilders.Max(x => x.DisplaySize.Height);

        #endregion

        #region Methods

        /// <summary>
        /// Resize this animation if needed.
        /// </summary>
        /// <param name="newSize">The new size.</param>
        /// <param name="resizeMode">The mode to use when resizing the visual.</param>
        /// <returns>The resized animation.</returns>
        public Animation ResizeIfNeeded(Size newSize, VisualResizeMode resizeMode)
        {
            // check if resize of the animation is needed
            if (VisualBuilders.All(x => x.DisplaySize.Width == newSize.Width && x.DisplaySize.Height == newSize.Height))
                return this;

            // perform resize
            return resizeMode switch
            {
                VisualResizeMode.Crop => Crop(newSize),
                VisualResizeMode.Scale => Scale(newSize),
                VisualResizeMode.ScaleDown => VisualBuilders.Any(x => newSize.Width < x.DisplaySize.Width) || VisualBuilders.Any(x => newSize.Height < x.DisplaySize.Height) ? Scale(newSize) : this,
                _ => throw new NotImplementedException()
            };
        }

        /// <summary>
        /// Crop this animation to a new size.
        /// </summary>
        /// <param name="newSize">The new size.</param>
        /// <returns>The cropped animation.</returns>
        public Animation Crop(Size newSize) => new(Name, Description, Crop(VisualBuilders, newSize), Options);

        /// <summary>
        /// Scale this animation to a new size.
        /// </summary>
        /// <param name="newSize">The new size.</param>
        /// <returns>The scaled animation.</returns>
        public Animation Scale(Size newSize) => new(Name, Description, Scale(VisualBuilders, newSize), Options);

        #endregion

        #region StaticMethods

        /// <summary>
        /// Crop grid visual builders to a specified size.
        /// </summary>
        /// <param name="visualBuilders">The visual builders.</param>
        /// <param name="renderSize">The render size.</param>
        /// <returns>The cropped visual builders.</returns>
        internal static GridVisualBuilder[] Crop(GridVisualBuilder[] visualBuilders, Size renderSize)
        {
            List<GridVisualBuilder> cropped = [];

            foreach (var visualBuilder in visualBuilders)
                cropped.Add(Visual.Crop(visualBuilder, renderSize));
            
            return [..cropped];
        }

        /// <summary>
        /// Scale grid visual builders to a specified size.
        /// </summary>
        /// <param name="visualBuilders">The visual builders.</param>
        /// <param name="renderSize">The render size.</param>
        /// <returns>The scaled visual builders.</returns>
        internal static GridVisualBuilder[] Scale(GridVisualBuilder[] visualBuilders, Size renderSize)
        {
            List<GridVisualBuilder> scalled = [];

            foreach (var visualBuilder in visualBuilders)
                scalled.Add(Visual.Scale(visualBuilder, renderSize));

            return [.. scalled];
        }

        #endregion
    }
}
