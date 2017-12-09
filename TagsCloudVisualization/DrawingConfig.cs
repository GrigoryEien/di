using System.Drawing;
using System.Windows.Media;
using TagsCloudVisualization.Interfaces;
using Brush = System.Drawing.Brush;

namespace TagsCloudVisualization
{
    public class DrawingConfig : IDrawingConfig
    {
        public DrawingConfig(Font font, SolidBrush brush, Size size)
        {
            Font = font;
            Brush = brush;
            Size = size;
        }

        public SolidBrush GenerateBrush(WordInRect wordInRect)
        {
            return Brush;
        }

        public Font Font { get; set; }
        public Size Size { get; set; }
        private SolidBrush Brush;
        
    }
}