using NUnit.Framework;

namespace UAParser.FormFactor.Tests
{
	[TestFixture]
	[Parallelizable]
	public class PodcastingResourceTests : ResourceTests
	{
		[Test]
		public void CanRunPodcastingTests()
		{
			RunTests("UAParser.FormFactor.Tests.Resources.podcasting_user_agent_strings.yaml", UserAgentYamlTestCase.ReadFromMap);
		}
	}
}