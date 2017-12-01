using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TagsCloudVisualization.Interfaces;

namespace TagsCloudVisualization
{
    public class CloudBilder : ICloudBuilder

    {
        private IWordsFilter filter;
        private IFrequencyAnalyzer frequencyAnalyzer;
        private ICloudDrawer cloudDrawer;
        private IDictionaryNormalizer dictionaryNormalizer;
        private ICircularCloudLayouter circularCloudLayouter;

        public CloudBilder(IDictionaryNormalizer dictionaryNormalizer, IWordsFilter filter,
            IFrequencyAnalyzer frequencyAnalyzer, ICloudDrawer cloudDrawer, ICircularCloudLayouter circularCloudLayouter)
        {
            this.dictionaryNormalizer = dictionaryNormalizer;
            this.filter = filter;
            this.frequencyAnalyzer = frequencyAnalyzer;
            this.cloudDrawer = cloudDrawer;
            this.circularCloudLayouter = circularCloudLayouter;
        }

        public Bitmap BuildCloud(IEnumerable<string> lines, int count)
        {
            var mostFrequenWords = filter.Filter(lines);
            var frequentWords = frequencyAnalyzer.GetFrequencyDict(mostFrequenWords).Take(count);
            var mostFrequentWords = frequentWords
                .OrderByDescending(x => x.Value)
                .Take(count)
                .ToDictionary(x => x.Key, x => x.Value);
            mostFrequentWords = dictionaryNormalizer.NormalizeDictionary(mostFrequentWords);
            var rects = CalculateRectsForWords(mostFrequentWords, new Point(0, 0));
            return cloudDrawer.DrawMap(rects);
        }

        private WordInRect[] CalculateRectsForWords(Dictionary<string, int> words, Point center)
        {
            var graphics = Graphics.FromImage(new Bitmap(1, 1));

            return words.Select(x =>
            {
                var font = new Font(FontFamily.GenericSansSerif, x.Value, FontStyle.Regular, GraphicsUnit.Pixel);
                var size = graphics.MeasureString(x.Key, font);
                var rect = circularCloudLayouter.PutNextRectangle(size.ToSize());
                return new WordInRect(x.Key, rect, font);
            }).ToArray();
        }
    }
}