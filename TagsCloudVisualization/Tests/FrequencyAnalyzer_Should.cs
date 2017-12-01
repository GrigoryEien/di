//using System.Collections.Generic;
//using FluentAssertions;
//using NUnit.Framework;
//
//namespace TagsCloudVisualization
//{
//	public class FrequencyAnalyzer_Should
//	{
//		[TestFixture]
//		public class FrequencyAnalyzer_should
//		{
//			
//			[Test]
//			public void ShouldCountOnlyWordsWithLengthGreaterOrEquallTo()
//			{
//				var lines = new List<string>(){"Some words should be skipped"};
//				var actulaDict = FrequencyAnalyzer.GetFrequencyDict(lines, 4);
//				var expectedDict = new Dictionary<string,int>()
//				{
//					{"SOME",1},
//					{"WORDS",1 },
//					{"SHOULD",1 },
//					{"SKIPPED",1}
//				};
//				expectedDict.ShouldBeEquivalentTo(actulaDict);
//			}
//
//			[Test]
//			public void ShouldIgnoreCases()
//			{
//				var lines = new List<string>() { "test TEST Test" };
//				var actulaDict = FrequencyAnalyzer.GetFrequencyDict(lines, 4);
//				var expectedDict = new Dictionary<string, int>()
//				{
//					{ "TEST",3},
//				};
//				expectedDict.ShouldBeEquivalentTo(actulaDict);
//			}
//		}
//	}
//}