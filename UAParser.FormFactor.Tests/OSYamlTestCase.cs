using System.Collections.Generic;
using NUnit.Framework;
using UAParser.FormFactor.Models;

namespace UAParser.FormFactor.Tests
{
	public class OSYamlTestCase : YamlTestCase
	{
		public string Family { get; set; }

		public string Major { get; set; }

		public string Minor { get; set; }

		public string Patch { get; set; }

		public string PatchMinor { get; set; }

		public static OSYamlTestCase ReadFromMap(Dictionary<string, string> map)
		{
			var tc = new OSYamlTestCase()
			{
				UserAgent = map["user_agent_string"],
				Family = map["family"],
				Major = map["major"],
				Minor = map["minor"],
				Patch = map["patch"],
				PatchMinor = map["patch_minor"]
			};
			return tc;
		}

		public override void Verify(ClientInfo clientInfo)
		{
			Assert.NotNull(clientInfo);
			AssertMatch(Family, clientInfo.OS.Family, "Family");
			AssertMatch(Major, clientInfo.OS.Major, "Major");
			AssertMatch(Minor, clientInfo.OS.Minor, "Minor");
			AssertMatch(Patch, clientInfo.OS.Patch, "Patch");
			AssertMatch(PatchMinor, clientInfo.OS.PatchMinor, "PatchMinor");
		}
	}
}