using System;
using System.Drawing;

namespace TagsCloudVisualization.CloudBuilding
{
    public class CloudSaver : ICloudSaver
    {
        public void SaveCloud(Bitmap cloud, string filename, string extension)
        {
            if (extension == "png" || extension == "bmp")
            {
                var fullname = filename + "." + extension;
                cloud.Save(fullname);
                Console.WriteLine($"Saved to {fullname}");
                return;
            }
            throw new FormatException($"Unsupported extension: {extension}");
        }
    }
}