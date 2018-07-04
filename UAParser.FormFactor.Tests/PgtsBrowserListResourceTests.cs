using NUnit.Framework;

namespace UAParser.FormFactor.Tests
{
	[TestFixture]
	[Parallelizable]
	public class PgtsBrowserListResourceTests : ResourceTests
	{
		[Test]
		public void CanRunPgtsBrowserListTests()
		{
			RunTests("UAParser.FormFactor.Tests.Resources.pgts_browser_list.yaml", UserAgentYamlTestCase.ReadFromMap);
		}
	}
}