using System.Drawing;
using System.Windows.Media;
using Brush = System.Drawing.Brush;

namespace TagsCloudVisualization
{
    public class DrawingConfig
    {
        public DrawingConfig(Font font, SolidBrush brush, Size size)
        {
            Font = font;
            Brush = brush;
            Size = size;
        }

        public Font Font { get; set; }
        public SolidBrush Brush { get; set; }
        public Size Size { get; set; }
    }
}