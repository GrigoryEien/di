using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Design;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Fclp;
using Autofac;
using CommandLine;
using CommandLine.Text;
using TagsCloudVisualization.Interfaces;
using Color = System.Windows.Media.Color;

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
                Console.WriteLine("Destination and source are required");
                return;
            }


            IEnumerable<string> lines;
            try
            {
                lines = File.ReadLines(options.Source);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
                return;
            }
           
            var builder = new ContainerBuilder();
            builder.RegisterInstance(new FrequencyAnalyzer()).As<IFrequencyAnalyzer>();
            builder.RegisterInstance(new DictionaryNormalizer()).As<IDictionaryNormalizer>();
            builder.RegisterInstance(new CloudLayouter(new Point(0, 0), options.HorizontalExtensionCoefficient))
                .As<ICloudLayouter>();
            builder.RegisterInstance(new WordsFilter(options.BannedWords)).As<IWordsFilter>();
            builder.RegisterInstance(new LayoutNormalizer()).As<ILayoutNormalizer>();
            builder.RegisterType<CloudBuilder>().As<ICloudBuilder>();
            builder.RegisterType<CloudDrawer>().As<ICloudDrawer>();
            builder.RegisterInstance(new FileReader()).As<IFileReader>();
            builder.RegisterInstance(new CloudSaver()).As<ICloudSaver>();

            var color = GetColorByName(options.BrushColor);
            var size = new Size(options.Width, options.Heigth);
            var font = new Font(options.FontName, 10);
            var brushColor = new SolidBrush(color);
            var drawingConfig = new DrawingConfig(font,brushColor,size);

            var container = builder.Build();
            var cloudBilder = container.Resolve<ICloudBuilder>();


            var cloud = cloudBilder.BuildCloud(lines, options.Count, drawingConfig);
            new CloudSaver().SaveCloud(cloud, options.Destination, options.Extension);
        }

        private static System.Drawing.Color GetColorByName(string name)
        {
            var brush = (SolidColorBrush) new BrushConverter().ConvertFromString(name);
            var solidBrushColor = brush.Color;
            return System.Drawing.Color.FromArgb(solidBrushColor.A, solidBrushColor.R, solidBrushColor.G,
                solidBrushColor.B);
        }
    }

    internal class Options
    {
        [Option('c', "count", DefaultValue = 100, HelpText = "How many words should be in cloud")]
        public int Count { get; set; }

        [Option('s', "source", Required = true, HelpText = "Textfile")]
        public string Source { get; set; }

        [Option('d', "dest", Required = true, HelpText = "Output file (without extension")]
        public string Destination { get; set; }

        [Option("hec", DefaultValue = 1, HelpText = "Horizontal extension coefficient")]
        public int HorizontalExtensionCoefficient { get; set; }

        [Option("clr", DefaultValue = "Magenta", HelpText = "Color")]
        public string BrushColor { get; set; };

        [Option('f', "font", DefaultValue = "Arial", HelpText = "Font name")]
        public string FontName { get; set; }

        [Option("bw", DefaultValue = "function-words.txt", HelpText = "File with banned words")]
        public string BannedWords { get; set; }
        
        [Option("ext",DefaultValue = "png", HelpText = "Output file extension")]
        public string Extension { get; set; }

        [Option("width", DefaultValue = 1000, HelpText = "Output file width")]
        public int Width { get; set; }
        
        [Option("heigth", DefaultValue = 1000, HelpText = "Output file heigth")]
        public int Heigth { get; set; }
    }
}