using System.Drawing;

namespace TagsCloudVisualization.CloudBuilding
{
    public interface ICloudSaver
    {
        void SaveCloud(Bitmap cloud, string filename, string extension);
    }
}