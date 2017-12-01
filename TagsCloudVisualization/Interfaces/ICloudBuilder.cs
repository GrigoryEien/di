using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization.Interfaces
{
    public interface ICloudBuilder
    {
         Bitmap BuildCloud(IEnumerable<string> lines, int count);
    }
}