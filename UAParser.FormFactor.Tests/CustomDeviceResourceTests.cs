using NUnit.Framework;

namespace UAParser.FormFactor.Tests
{
	[TestFixture]
	[Parallelizable]
	public class CustomDeviceResourceTests : ResourceTests
	{
		[Test]
		public void CanRunCustomDeviceTests()
		{
			RunTests("UAParser.FormFactor.Tests.Resources.custom_test_device.yaml", OSYamlTestCase.ReadFromMap);
		}
	}
}