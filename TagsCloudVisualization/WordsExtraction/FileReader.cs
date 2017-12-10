using System;
using System.Collections.Generic;
using System.IO;

namespace TagsCloudVisualization.WordsExtraction
{
    public class FileReader : IFileReader
    {
        public IEnumerable<string> ReadFile(string filename)
        {
            if (filename.EndsWith(".txt"))
                return File.ReadLines(filename);
            
            throw new FormatException(
                $"Format of {filename} is unsupported. Supported formats are: txt, doc, docx");
        }
    }
}