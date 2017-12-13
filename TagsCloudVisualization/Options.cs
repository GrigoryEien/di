using CommandLine;
using CommandLine.Text;

namespace TagsCloudVisualization
{
    class Options
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
        public string BrushColor { get; set; }

        [Option('f', "font", DefaultValue = "Arial", HelpText = "Font name")]
        public string FontName { get; set; }

        [Option("bw", DefaultValue = "function-words.txt", HelpText = "File with banned words")]
        public string BannedWords { get; set; }

        [Option("ext", DefaultValue = "png", HelpText = "Output file extension")]
        public string Extension { get; set; }

        [Option("width", DefaultValue = 1000, HelpText = "Output file width")]
        public int Width { get; set; }

        [Option("height", DefaultValue = 1000, HelpText = "Output file heigth")]
        public int Heigth { get; set; }
                
        [HelpOption]
        public string GetUsage() {
            return HelpText.AutoBuild(this,
                (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
        }

    }
}