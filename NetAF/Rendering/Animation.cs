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
    /// <param name="AnimationVisualFrame">The visual frame that creates the animation.</param>
    public record Animation(string Name, string Description, GridAnimatedVisualFrame AnimationVisualFrame)
    {
        #region Methods

        /// <summary>
        /// Resize this animation if needed.
        /// </summary>
        /// <param name="newSize">The new size.</param>
        /// <param name="resizeMode">The mode to use when resizing the animation.</param>
        /// <returns>The resized animation.</returns>
        public Animation ResizeIfNeeded(Size newSize, VisualResizeMode resizeMode)
        {
            // check if resize of the animation is needed
            if (AnimationVisualFrame.Frames.All(x => x.Width == newSize.Width && x.Height == newSize.Height))
                return this;

            // perform resize
            return resizeMode switch
            {
                VisualResizeMode.Crop => Crop(newSize),
                VisualResizeMode.Scale => Scale(newSize),
                VisualResizeMode.ScaleDown => AnimationVisualFrame.Frames.Any(x => newSize.Width < x.Width) || AnimationVisualFrame.Frames.Any(x => newSize.Height < x.Height) ? Scale(newSize) : this,
                _ => throw new NotImplementedException()
            };
        }

        /// <summary>
        /// Crop this animation to a new size.
        /// </summary>
        /// <param name="newSize">The new size.</param>
        /// <returns>The cropped animation.</returns>
        public Animation Crop(Size newSize) => new(Name, Description, Crop(AnimationVisualFrame, newSize));

        /// <summary>
        /// Scale this animation to a new size.
        /// </summary>
        /// <param name="newSize">The new size.</param>
        /// <returns>The scaled animation.</returns>
        public Animation Scale(Size newSize) => new(Name, Description, Scale(AnimationVisualFrame, newSize));

        #endregion

        #region StaticMethods

        /// <summary>
        /// Crop an animation visual frame to a specified size.
        /// </summary>
        /// <param name="frame">The animation visual frame.</param>
        /// <param name="renderSize">The render size.</param>
        /// <returns>The cropped animation visual frame.</returns>
        private static GridAnimatedVisualFrame Crop(GridAnimatedVisualFrame frame, Size renderSize)
        {
            // can't crop if the source is smaller than the render size
            if (frame.Frames.All(x => x.Width < renderSize.Width) && frame.Frames.All(x => x.Height < renderSize.Height))
                return frame;

            List<GridVisualFrame> croppedFrames = [];

            foreach (var f in frame.Frames)
            {
                var newWidth = Math.Min(renderSize.Width, f.Width);
                var newHeight = Math.Min(renderSize.Height, f.Height);
                var newSize = new Size(newWidth, newHeight);
                var newBuilder = new GridVisualBuilder(AnsiColor.Black, AnsiColor.White);
                newBuilder.Resize(newSize);

                for (var row = 0; row < newHeight; row++)
                {
                    for (var column = 0; column < newWidth; column++)
                    {
                        var cell = f.GetCell(column, row);
                        newBuilder.SetCell(column, row, cell.Character, cell.Foreground, cell.Background);
                    }
                }

                croppedFrames.Add(new GridVisualFrame(newBuilder, f.CursorLeft, f.CursorTop));
            }

            return new GridAnimatedVisualFrame([..croppedFrames], frame.Options);
        }

        /// <summary>
        /// Scale an animation visual frame to a specified size.
        /// </summary>
        /// <param name="frame">The animation visual frame.</param>
        /// <param name="renderSize">The render size.</param>
        /// <returns>The scaled animation visual frame.</returns>
        private static GridAnimatedVisualFrame Scale(GridAnimatedVisualFrame frame, Size renderSize)
        {
            List<GridVisualFrame> croppedFrames = [];

            foreach (var f in frame.Frames)
            {
                var ratio = Math.Max(f.Width / (double)renderSize.Width, f.Height / (double)renderSize.Height);
                var targetWidth = (int)Math.Max(1, Math.Floor(f.Width / ratio));
                var targetHeight = (int)Math.Max(1, Math.Floor(f.Height / ratio));

                var widthRatio = f.Width / (double)targetWidth;
                var heightRatio = f.Height / (double)targetHeight;

                var newBuilder = new GridVisualBuilder(AnsiColor.Black, AnsiColor.White);
                newBuilder.Resize(new Size(targetWidth, targetHeight));

                for (var row = 0; row < targetHeight; row++)
                {
                    var sourceRow = (int)Math.Floor(heightRatio * row);

                    for (var column = 0; column < targetWidth; column++)
                    {
                        var sourceColumn = (int)Math.Floor(widthRatio * column);
                        var cell = f.GetCell(sourceColumn, sourceRow);
                        newBuilder.SetCell(column, row, cell.Character, cell.Foreground, cell.Background);
                    }
                }

                croppedFrames.Add(new GridVisualFrame(newBuilder, f.CursorLeft, f.CursorTop));
            }

            return new GridAnimatedVisualFrame([.. croppedFrames], frame.Options);
        }

        #endregion
    }
}
