using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using Autofac;
using CommandLine.Text;
using TagsCloudVisualization.CloudBuilding;
using TagsCloudVisualization.Visualization;
using TagsCloudVisualization.WordsExtraction;
using Color = System.Windows.Media.Color;
using ColorConverter = System.Windows.Media.ColorConverter;

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
            IEnumerable<string> bannedWords = new List<string>();
            try
            {
                lines = File.ReadLines(options.Source);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
                return;
            }

            try
            {
                bannedWords = File.ReadLines(options.BannedWords);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
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

            var size = new Size(options.Width, options.Heigth);
            
            var drawingConfig = new DrawingConfig(options.FontName, options.BrushColor, size);

            var container = builder.Build();
            var cloudBilder = container.Resolve<ICloudBuilder>();


            var cloud = cloudBilder.BuildCloud(lines, options.Count, drawingConfig);
            new CloudSaver().SaveCloud(cloud, options.Destination, options.Extension);
        }

    }
}