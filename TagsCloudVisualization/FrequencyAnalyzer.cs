using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using TagsCloudVisualization.Interfaces;

namespace TagsCloudVisualization
{
	public class FrequencyAnalyzer : IFrequencyAnalyzer
	{
		public Dictionary<string, int> GetFrequencyDict(IEnumerable<string> lines)
		{
			return
				lines.GroupBy(x => x)
					.ToDictionary(k => k.Key, v => v.Count());
		}
	}
}