using System.Collections.Generic;
using NUnit.Framework;
using UAParser.FormFactor.Models;

namespace UAParser.FormFactor.Tests
{
	public class UserAgentYamlTestCase : YamlTestCase
	{
		public string Family { get; set; }

		public string Major { get; set; }

		public string Minor { get; set; }

		public string Patch { get; set; }

		public static UserAgentYamlTestCase ReadFromMap(Dictionary<string, string> map)
		{
			var tc = new UserAgentYamlTestCase()
			{
				UserAgent = map["user_agent_string"],
				Family = map["family"],
				Major = map["major"],
				Minor = map["minor"],
				Patch = map["patch"],
			};
			return tc;
		}

		public override void Verify(ClientInfo clientInfo)
		{
			Assert.NotNull(clientInfo);
			AssertMatch(Family, clientInfo.UA.Family, "Family");
			AssertMatch(Major, clientInfo.UA.Major, "Major");
			AssertMatch(Minor, clientInfo.UA.Minor, "Minor");
			AssertMatch(Patch, clientInfo.UA.Patch, "Patch");
		}
	}
}