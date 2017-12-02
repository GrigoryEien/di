using System.Collections.Generic;
using System.Linq;
using TagsCloudVisualization.Interfaces;

namespace TagsCloudVisualization
{
    public class WordsFilter : IWordsFilter
    {
        public IEnumerable<string> Filter(IEnumerable<string> words)
        {
            return words.SelectMany(x => x.Split(' ')).Select(x => x.ToUpper()).Where(x => x.Length >= 4);
        }
    }
}