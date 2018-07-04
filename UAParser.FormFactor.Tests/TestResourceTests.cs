using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using YamlDotNet.RepresentationModel;

namespace UAParser.FormFactor.Tests
{
	[TestFixture]
	[Parallelizable]
	public class TestResourceTests
	{
		[Test]
		public void CanRunDeviceTests()
		{
			RunTests("UAParser.FormFactor.Tests.Resources.test_device.yaml", DeviceYamlTestCase.ReadFromMap);
		}

		[Test]
		public void CanRunAdditionalOsTests()
		{
			RunTests("UAParser.FormFactor.Tests.Resources.additional_os_tests.yaml", OSYamlTestCase.ReadFromMap);
		}

		[Test]
		public void CanRunFirefoxUserAgentStringTests()
		{
			RunTests("UAParser.FormFactor.Tests.Resources.firefox_user_agent_strings.yaml", UserAgentYamlTestCase.ReadFromMap);
		}

		[Test]
		public void CanRunPgtsBrowserListTests()
		{
			RunTests("UAParser.FormFactor.Tests.Resources.pgts_browser_list.yaml", UserAgentYamlTestCase.ReadFromMap);
		}

		[Test]
		public void CanRunUserAgentParserTests()
		{
			RunTests("UAParser.FormFactor.Tests.Resources.test_ua.yaml", UserAgentYamlTestCase.ReadFromMap);
		}

		[Test]
		public void CanRunUserAgentParserOsTests()
		{
			RunTests("UAParser.FormFactor.Tests.Resources.test_os.yaml", OSYamlTestCase.ReadFromMap);
		}

		public void RunTests<TTestCase>(string resourceName, Func<Dictionary<string, string>, TTestCase> testCaseFunction) where TTestCase : YamlTestCase
		{
			var testCases = GetTestCases(resourceName, "test_cases", testCaseFunction);

			RunTestCases(testCases);
		}

		private static void RunTestCases<TTestCase>(List<TTestCase> testCases)
			where TTestCase : YamlTestCase
		{
			var parser = Parser.GetDefault();
			Assert.AreNotEqual(0, testCases.Count);

			var sb = new StringBuilder();
			for (var i = 0; i < testCases.Count; i++)
			{
				var tc = testCases[i];
				if (tc == null)
				{
					continue;
				}

				var clientInfo = parser.Parse(tc.UserAgent);
				try
				{
					tc.Verify(clientInfo);
				}
				catch (AssertionException ex)
				{
					sb.AppendLine("testcase " + (i + 1) + ": " + ex.Message);
				}
			}
			Assert.True(0 == sb.Length, "Failed tests: " + Environment.NewLine + sb);
		}

		public List<TTestCase> GetTestCases<TTestCase>(string resourceName, string yamlNodeName, Func<Dictionary<string, string>, TTestCase> testCaseFunction)
		{
			var yamlContent = this.GetTestResources(resourceName);
			var yaml = new YamlStream();
			yaml.Load(new StringReader(yamlContent));

			//reading overall configurations
			var regexConfigNode = (YamlMappingNode) yaml.Documents[0].RootNode;
			var regexConfig = new Dictionary<string, YamlNode>();
			foreach (var entry in regexConfigNode.Children)
			{
				regexConfig.Add(((YamlScalarNode) entry.Key).Value, entry.Value);
			}

			var testCasesNode = (YamlSequenceNode) regexConfig[yamlNodeName];
			var testCases = testCasesNode.ConvertToDictionaryList()
			  .Select(configMap =>
			  {
				  if (!configMap.ContainsKey("js_ua")) //we deliberatly skip tests with js-user agents
				  {
					  return testCaseFunction(configMap);
				  }

				  return default(TTestCase);
			  })
			  .ToList();
			return testCases;
		}
	}
}