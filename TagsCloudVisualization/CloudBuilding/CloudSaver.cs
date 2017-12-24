using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection;

namespace TagsCloudVisualization.CloudBuilding
{
    public class CloudSaver : ICloudSaver
    {
        public Result<None> SaveCloud(Bitmap cloud, string filename, string extension)
        {
            var format = GetImageFormat(extension);
            if (format == null)
                return Result.Fail<None>(
                    $"Unsupported extension: '{extension}'. Try another one.");
            var fullname = filename + "." + extension;
            return Result.OfAction(() => cloud.Save(fullname, format));
        }

        private static ImageFormat GetImageFormat(string extension)
        {
            ImageFormat result = null;
            var prop = typeof(ImageFormat)
                .GetProperties()
                .FirstOrDefault(p => p.Name.Equals(extension, StringComparison.InvariantCultureIgnoreCase));
            if (prop != null)
            {
                result = prop.GetValue(prop) as ImageFormat;
            }
            return result;
        }
    }
}