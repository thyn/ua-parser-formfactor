using NUnit.Framework;

namespace UAParser.FormFactor.Tests
{
	[TestFixture]
	[Parallelizable]
	public class FirefoxUserAgentStringResourceTests : ResourceTests
	{
		[Test]
		public void CanRunFirefoxUserAgentStringTests()
		{
			RunTests("UAParser.FormFactor.Tests.Resources.firefox_user_agent_strings.yaml", UserAgentYamlTestCase.ReadFromMap);
		}
	}
}