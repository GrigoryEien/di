using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Autofac;
using TagsCloudVisualization.CloudBuilding;
using TagsCloudVisualization.Visualization;
using TagsCloudVisualization.WordsExtraction;

namespace TagsCloudVisualization
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new Options();
            CommandLine.Parser.Default.ParseArguments(args, options);
            if (options.Source is null || options.Destination is null)
            {
                Console.WriteLine("Destination and source are required. Use -h or --help for help");
                return;
            }


            IEnumerable<string> lines;
            var resultOfReadLines = Result.Of(() => File.ReadLines(options.Source));
            if (resultOfReadLines.IsSuccess)
                lines = resultOfReadLines.Value;
            else
            {
                Console.WriteLine(resultOfReadLines.Error);
                return;
            }

            IEnumerable<string> bannedWords = new List<string>();
            var resultOfReadBannedWords = Result.Of(() => File.ReadLines(options.BannedWords));

            if (resultOfReadBannedWords.IsSuccess)
                bannedWords = resultOfReadBannedWords.Value;
            else
            {
                Console.WriteLine(resultOfReadBannedWords.Error);
                Console.WriteLine("No words will be filtered");
            }

            var builder = new ContainerBuilder();
            builder.RegisterInstance(new FrequencyAnalyzer()).As<IFrequencyAnalyzer>();
            builder.RegisterInstance(new DictionaryNormalizer()).As<IDictionaryNormalizer>();
            builder.RegisterInstance(new CloudLayouter(new Point(0, 0), options.HorizontalExtensionCoefficient))
                .As<ICloudLayouter>();
            builder.RegisterInstance(new WordsFilter(bannedWords)).As<IWordsFilter>();
            builder.RegisterInstance(new LayoutNormalizer()).As<ILayoutNormalizer>();
            builder.RegisterType<CloudBuilder>().As<ICloudBuilder>();
            builder.RegisterType<CloudDrawer>().As<ICloudDrawer>();
            builder.RegisterInstance(new FileReader()).As<IFileReader>();
            builder.RegisterInstance(new CloudSaver()).As<ICloudSaver>();

            if (options.Width <= 0)
            {
                Console.WriteLine("Width should be positive");
                return;
            }
            
            if (options.Heigth <= 0)
            {
                Console.WriteLine("Height should be positive");
                return;
            }
            var size = new Size(options.Width, options.Heigth);

            var drawingConfig = new DrawingConfig(options.FontName, options.BrushColor, size);

            var container = builder.Build();
            var cloudBilder = container.Resolve<ICloudBuilder>();


            var cloud = cloudBilder.BuildCloud(lines, options.Count, drawingConfig);
            new CloudSaver().SaveCloud(cloud, options.Destination, options.Extension);
        }
    }
}