using System.Collections.Generic;

namespace TagsCloudVisualization.Interfaces
{
    public interface IFrequencyAnalyzer
    {
        Dictionary<string, int> GetFrequencyDict(IEnumerable<string> lines);
    }
}