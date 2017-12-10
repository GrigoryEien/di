using System.Collections.Generic;

namespace TagsCloudVisualization.WordsExtraction
{
    public interface IFileReader
    {
        IEnumerable<string> ReadFile(string filename);
    }
}