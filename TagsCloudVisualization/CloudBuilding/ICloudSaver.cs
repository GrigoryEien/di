using System.Drawing;

namespace TagsCloudVisualization.Interfaces
{
    public interface ICloudSaver
    {
        void SaveCloud(Bitmap cloud, string filename, string extension);
    }
}