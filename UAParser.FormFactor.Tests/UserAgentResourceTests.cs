using NUnit.Framework;

namespace UAParser.FormFactor.Tests
{
	[TestFixture]
	[Parallelizable]
	public class UserAgentResourceTests : ResourceTests
	{
		[Test]
		public void CanRunUserAgentParserTests()
		{
			RunTests("UAParser.FormFactor.Tests.Resources.test_ua.yaml", UserAgentYamlTestCase.ReadFromMap);
		}
	}
}