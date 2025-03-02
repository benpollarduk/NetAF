﻿using System.IO;
using NetAF.Logic;
using NetAF.Rendering;

namespace NetAF.Tests
{
    /// <summary>
    /// Provides a console adapter for tests.
    /// </summary>
    internal class TestConsoleAdapter : IIOAdapter
    {
        #region Properties

        /// <summary>
        /// Get the input stream.
        /// </summary>
        public TextReader In
        {
            get
            {
                var memoryStream = new MemoryStream();
                return new StreamReader(memoryStream);
            }
        }

        /// <summary>
        /// Get the output stream.
        /// </summary>
        public TextWriter Out
        {
            get
            {
                var memoryStream = new MemoryStream();
                return new StreamWriter(memoryStream);
            }
        }

        /// <summary>
        /// Get the error output stream.
        /// </summary>
        public TextWriter Error
        {
            get
            {
                var memoryStream = new MemoryStream();
                return new StreamWriter(memoryStream);
            }
        }

        #endregion

        #region Implementation of IIOAdapter

        /// <summary>
        /// Setup for a game.
        /// </summary>
        /// <param name="game">The game to set up for.</param>
        public void Setup(Game game)
        {
        }

        /// <summary>
        /// Render a frame.
        /// </summary>
        /// <param name="frame">The frame to render.</param>
        public void RenderFrame(IFrame frame)
        {
            Out?.WriteLine(frame?.ToString());
        }

        /// <summary>
        /// Wait for acknowledgment.
        /// </summary>
        /// <returns>True if the acknowledgment was received correctly, else false.</returns>
        public bool WaitForAcknowledge()
        {
            return true;
        }

        /// <summary>
        /// Wait for input.
        /// </summary>
        /// <returns>The input.</returns>
        public string WaitForInput()
        {
            return In?.ReadLine();
        }

        #endregion
    }
}
