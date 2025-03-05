using NetAF.Rendering;
using System.Text;

namespace NetAF.Tests
{
    internal class TestPresenter : IFramePresenter
    {
        private readonly StringBuilder builder = new();

        public void Present(string frame)
        {
            builder.Append(frame);
        }

        public override string ToString()
        {
            return builder.ToString();
        }
    }
}
