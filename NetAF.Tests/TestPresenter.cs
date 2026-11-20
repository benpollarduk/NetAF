using NetAF.Assets;
using NetAF.Rendering;
using System.Collections.Generic;
using System.Text;

namespace NetAF.Tests
{
    internal class TestPresenter : IFramePresenter
    {
        public int RenderCount { get; private set; }
        public Queue<string> Renders { get; } = [];

        private readonly StringBuilder builder = new();

        public void Present(string frame)
        {
            builder.Append(frame);
            RenderCount++;
            Renders.Enqueue(frame);
        }

        public override string ToString()
        {
            return builder.ToString();
        }

        public Size GetPresentableSize()
        {
            return new Size(10, 10);
        }
    }
}
