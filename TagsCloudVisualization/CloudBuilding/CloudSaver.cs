using System;
using System.Drawing;

namespace TagsCloudVisualization.CloudBuilding
{
    public class CloudSaver : ICloudSaver
    {
        public Result<None> SaveCloud(Bitmap cloud, string filename, string extension)
        {
            if (extension != "png" && extension != "bmp")
                return Result.Fail<None>(
                    $"Unsupported extension: '{extension}'. Extension should be either png or bmp");
            var fullname = filename + "." + extension;
            cloud.Save(fullname);
            Console.WriteLine($"Saved to {fullname}");
            return Result.Ok();
        }
    }
}