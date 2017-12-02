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

        public Bitmap BuildCloud(IEnumerable<string> lines, int count, DrawingConfig drawingConfig)
        {
            var mostFrequenWords = filter.Filter(lines);
            var frequentWords = frequencyAnalyzer.GetFrequencyDict(mostFrequenWords).Take(count);
            var mostFrequentWords = frequentWords
                .OrderByDescending(x => x.Value)
                .Take(count)
                .ToDictionary(x => x.Key, x => x.Value);
            mostFrequentWords = dictionaryNormalizer.NormalizeDictionary(mostFrequentWords);
            var rects = CalculateRectsForWords(mostFrequentWords, new Point(0, 0), drawingConfig.Font);
            return cloudDrawer.DrawMap(rects,drawingConfig);
        }

        private WordInRect[] CalculateRectsForWords(Dictionary<string, int> words, Point center, Font font)
        {
            var graphics = Graphics.FromImage(new Bitmap(1, 1));

            return words.Select(x =>
            {
                font = new Font(font.FontFamily,x.Value);
                var size = graphics.MeasureString(x.Key, font);
                var rect = circularCloudLayouter.PutNextRectangle(size.ToSize());
                return new WordInRect(x.Key, rect, font);
            }).ToArray();
        }
    }
}