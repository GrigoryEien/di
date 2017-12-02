using System.Drawing;

namespace TagsCloudVisualization
{
    public class DrawingConfig
    {
        public DrawingConfig(Font font, Brush brush, Size size)
        {
            Font = font;
            Brush = brush;
            Size = size;
        }

        public Font Font { get; set; }
        public Brush Brush { get; set; }
        public Size Size { get; set; }
    }
}