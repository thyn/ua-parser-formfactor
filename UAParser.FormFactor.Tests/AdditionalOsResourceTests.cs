using NUnit.Framework;

namespace UAParser.FormFactor.Tests
{
	[TestFixture]
	[Parallelizable]
	public class AdditionalOsResourceTests : ResourceTests
	{
		[Test]
		public void CanRunAdditionalOsTests()
		{
			RunTests("UAParser.FormFactor.Tests.Resources.additional_os_tests.yaml", OSYamlTestCase.ReadFromMap);
		}
	}
}