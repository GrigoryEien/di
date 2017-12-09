using System.Drawing;

namespace TagsCloudVisualization.Interfaces
{
    public interface IDrawingConfig
    {
        Size Size { get; }
        Font Font { get; }
        SolidBrush GenerateBrush(WordInRect word);
    }
}