using System.Drawing;

namespace TagsCloudVisualization.Interfaces
{
    public interface ICloudLayouter
    {
        Rectangle PutNextRectangle(Size size);
    }
}