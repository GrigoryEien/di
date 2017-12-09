using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Media;
using Autofac;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using TagsCloudVisualization.Interfaces;
using Color = System.Windows.Media.Color;
using ColorConverter = System.Windows.Media.ColorConverter;

namespace TagsCloudVisualization.Tests
{
    public class GeneralTests
    {
        [TestFixture]
        public class GeneralTests_Should
        {
            [Test]
            public void GenerateBitmapOfPassedSize()
            {
                var builder = new ContainerBuilder();
                builder.RegisterInstance(new FrequencyAnalyzer()).As<IFrequencyAnalyzer>();
                builder.RegisterInstance(new DictionaryNormalizer()).As<IDictionaryNormalizer>();
                builder.RegisterInstance(new CloudLayouter(new Point(0, 0), 1))
                    .As<ICloudLayouter>();
                
                var wordsFilterMock = new Mock<IWordsFilter>();
                wordsFilterMock.Setup(d => d.Filter(It.IsAny<IEnumerable<string>>()))
                    .Returns<IEnumerable<string>>(words => words.Where(word => word.Length > 2));
                
                builder.RegisterInstance(wordsFilterMock.Object).As<IWordsFilter>();
                builder.RegisterInstance(new LayoutNormalizer()).As<ILayoutNormalizer>();
                builder.RegisterType<CloudBuilder>().As<ICloudBuilder>();
                builder.RegisterType<CloudDrawer>().As<ICloudDrawer>();
                builder.RegisterInstance(new FileReader()).As<IFileReader>();
                builder.RegisterInstance(new CloudSaver()).As<ICloudSaver>();

                var color = System.Drawing.Color.Aqua;
                var size = new Size(500, 700);
                var font = new Font("Arial", 10);
                var brushColor = new SolidBrush(color);
                var drawingConfig = new DrawingConfig(font, brushColor, size);
                var container = builder.Build();
                var cloudBilder = container.Resolve<ICloudBuilder>();


                var cloud = cloudBilder.BuildCloud(GenerateRandomStrings(100), 100, drawingConfig);

                cloud.Width.Should().Be(500);
                cloud.Height.Should().Be(700);
            }

            private static System.Drawing.Color GetColorByName(string name)
            {
                var color = (Color) ColorConverter.ConvertFromString(name);
                return System.Drawing.Color.FromArgb(color.A, color.R, color.G,
                    color.B);
            }

            private static string[] GenerateRandomStrings(int count)
            {
                var strings = new string[count];
                for (var i = 0; i < count; i++)
                {
                    var path = Path.GetRandomFileName();
                    path = path.Replace(".", "");
                    strings[i] = path;
                }
                return strings;
            }
        }
    }
}