using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace TagsCloudVisualization
{
	public class WordsFilter_Should
	{
		[TestFixture]
		public class WordsFilter_should
		{
			private WordsFilter wordsFilter;
			[SetUp]
			public void SetUp()
			{
				wordsFilter = new WordsFilter("function-words.txt");
			}

			
			[Test]
			public void ShouldCountOnlyWordsWithLengthGreaterOrEquallTo()
			{
				var lines = new List<string>(){"Some words should be skipped"};
				var actualWords = wordsFilter.Filter(lines).ToArray();
				var expectedWords = new string[]
				{
					"WORDS",
					"SKIPPED",
				};
				expectedWords.ShouldBeEquivalentTo(actualWords);
			}

			[Test]
			public void ShouldIgnoreCases()
			{
				var lines = new List<string>() { "test TEST Test" };
				var actualWords = wordsFilter.Filter(lines).ToArray();
				var expectedWords = new string[]
				{
					"TEST",
					"TEST",
					"TEST",
				};
				expectedWords.ShouldBeEquivalentTo(actualWords);
			}
		}
	}
}