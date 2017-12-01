using System.Collections.Generic;
using System.Drawing;
using TagsCloudVisualization.Interfaces;

namespace TagsCloudVisualization
{
	public class CloudDrawer : ICloudDrawer
	{
		private ILayoutNormalizer layoutNormalizer;

		public CloudDrawer(ILayoutNormalizer layoutNormalizer)
		{
			this.layoutNormalizer = layoutNormalizer;
		}
		
		public Bitmap DrawMap(IEnumerable<WordInRect> words) {
			var mainRect = layoutNormalizer.GetMainRect(words);
			var normalizedWords = layoutNormalizer.ShiftLayout(words, mainRect);
			var bitmap = new Bitmap(mainRect.Width, mainRect.Height);
			var graphics = Graphics.FromImage(bitmap);

			foreach (var word in normalizedWords) {
				graphics.DrawString(word.Word, word.Font, Brushes.Magenta, word.Rect, StringFormat.GenericTypographic);
			}
			return bitmap;
		}
	}
}