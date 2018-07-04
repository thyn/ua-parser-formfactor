using NUnit.Framework;

namespace UAParser.FormFactor.Tests
{
	[TestFixture]
	[Parallelizable]
	public class UserAgentOsResourceTests : ResourceTests
	{
		[Test]
		public void CanRunUserAgentParserOsTests()
		{
			RunTests("UAParser.FormFactor.Tests.Resources.test_os.yaml", OSYamlTestCase.ReadFromMap);
		}
	}
}