using System.Drawing;

namespace TagsCloudVisualization
{
	public class WordInRect
	{
		public string Word;
		public Rectangle Rect;
		public Font Font;

		public WordInRect(string word, Rectangle rect, Font font)
		{
			this.Word = word;
			this.Rect = rect;
			this.Font = font;
		}
	}
}