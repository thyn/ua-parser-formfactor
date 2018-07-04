using System.Collections.Generic;
using NUnit.Framework;
using UAParser.FormFactor.Models;

namespace UAParser.FormFactor.Tests
{
	public class DeviceYamlTestCase : YamlTestCase
	{
		public string Family { get; set; }

		public static DeviceYamlTestCase ReadFromMap(Dictionary<string, string> map)
		{
			var tc = new DeviceYamlTestCase()
			{
				UserAgent = map["user_agent_string"],
				Family = map["family"],
			};
			return tc;
		}

		public override void Verify(ClientInfo clientInfo)
		{
			Assert.NotNull(clientInfo);
			AssertMatch(Family, clientInfo.Device.Family, "Family");
		}
	}
}