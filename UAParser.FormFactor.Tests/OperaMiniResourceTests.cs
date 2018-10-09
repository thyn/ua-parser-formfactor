using NUnit.Framework;

namespace UAParser.FormFactor.Tests
{
	[TestFixture]
	[Parallelizable]
	public class OperaMiniResourceTests : ResourceTests
	{
		[Test]
		public void CanRunOperaMiniTests()
		{
			RunTests("UAParser.FormFactor.Tests.Resources.opera_mini_user_agent_strings.yaml", UserAgentYamlTestCase.ReadFromMap);
		}
	}
}