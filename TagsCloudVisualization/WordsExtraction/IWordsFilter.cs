using System.Collections.Generic;

namespace TagsCloudVisualization.Interfaces
{
    public interface IWordsFilter
    {
        IEnumerable<string> Filter(IEnumerable<string> words);
    }
}