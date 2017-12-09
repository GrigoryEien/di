using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TagsCloudVisualization.Interfaces;

namespace TagsCloudVisualization
{
    public class WordsFilter : IWordsFilter
    {
        private static readonly char[] BannedChars = new char[]
        {
            '.',
            ',',
            '!',
            '?',
            '\'',
            ':',
            '\"'
        };

        private static string[] bannedWords;

        public WordsFilter(string pathToTxtWithBannedWords)
        {
            bannedWords = File.ReadLines(pathToTxtWithBannedWords).SelectMany(x => x.Split(' '))
                .Select(x => x.ToUpper()).ToArray();
        }

        public IEnumerable<string> Filter(IEnumerable<string> words)
        {
            return words.SelectMany(x => x.Split(' ')).Select(x => x.Trim(BannedChars).ToUpper())
                .Where(x => !bannedWords.Contains(x));
        }
    }
}