using NUnit.Framework;

namespace UAParser.FormFactor.Tests
{
	[TestFixture]
	[Parallelizable]
	public class DeviceResourceTests : ResourceTests
	{
		[Test]
		public void CanRunDeviceTests()
		{
			RunTests("UAParser.FormFactor.Tests.Resources.test_device.yaml", DeviceYamlTestCase.ReadFromMap);
		}
	}
}