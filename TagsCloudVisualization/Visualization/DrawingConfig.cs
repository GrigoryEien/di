using System.Drawing;
using ColorConverter = System.Windows.Media.ColorConverter;

namespace TagsCloudVisualization.Visualization
{
    public class DrawingConfig : IDrawingConfig
    {
        public Font Font { get; set; }
        public Size Size { get; set; }
        private SolidBrush Brush;

        public DrawingConfig(string fontName, string brushColor, Size size)
        {
            var color = DrawingConfig.GetColorByName(brushColor);
            
            Font = new Font(fontName, 10);
            Brush = new SolidBrush(color);
            Size = size;
        }

        public SolidBrush GenerateBrush(WordInRect wordInRect)
        {
            return Brush;
        }


        public static Color GetColorByName(string name)
        {
            var color = (System.Windows.Media.Color) ColorConverter.ConvertFromString(name);
            return Color.FromArgb(color.A, color.R, color.G,
                color.B);
        }
    }
}