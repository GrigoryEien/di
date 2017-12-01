using System.Collections.Generic;

namespace TagsCloudVisualization.Interfaces
{
    public interface IDictionaryNormalizer
    {
        Dictionary<string, int> NormalizeDictionary(Dictionary<string, int> dict);
    }
}