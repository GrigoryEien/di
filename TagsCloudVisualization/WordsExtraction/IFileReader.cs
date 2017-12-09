using System.Collections.Generic;

namespace TagsCloudVisualization.Interfaces
{
    public interface IFileReader
    {
        IEnumerable<string> ReadFile(string filename);
    }
}