using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using UAParser.FormFactor.Utils;
using YamlDotNet.RepresentationModel;

namespace UAParser.FormFactor.Tests
{
	[TestFixture]
	[Parallelizable]
	public class YamlParsingTests
	{
		[Test]
		public void CanParseSameRegexesUsingMinimalYamlParser()
		{
			//read in the yaml file in fully functional yaml parser
			var yamlContent = this.GetTestResources("UAParser.FormFactor.Tests.Resources.regexes.yaml");
			Assert.NotNull(yamlContent);
			Assert.AreNotEqual("", yamlContent);

			var yaml = new YamlStream();
			yaml.Load(new StringReader(yamlContent));

			// read into the minimal parser
			var minimal = new MinimalYamlParser(yamlContent);

			var entries =
				from doc in yaml.Documents
				select doc.RootNode as YamlMappingNode into rn
				where rn != null
				from e in rn.Children
				select new
				{
					Key = e.Key as YamlScalarNode,
					Value = e.Value as YamlSequenceNode
				} into e
				where e.Key != null && e.Value != null
				select e;

			var config = entries.ToDictionary(e => e.Key.Value,
											  e => e.Value,
											  StringComparer.OrdinalIgnoreCase);

			foreach (var kvPair in config)
			{
				var configNode = kvPair.Value;
				var valueDic = from node in configNode ?? Enumerable.Empty<YamlNode>()
							   select node as YamlMappingNode
					into node
							   where node != null
							   select node.Children
								   .Where(e => e.Key is YamlScalarNode && e.Value is YamlScalarNode)
								   .GroupBy(e => e.Key.ToString(), e => e.Value.ToString(), StringComparer.OrdinalIgnoreCase)
								   .ToDictionary(e => e.Key, e => e.Last(), StringComparer.OrdinalIgnoreCase)
					into cm
							   select cm;

				var name = kvPair.Key;
				var minimalLookupList = minimal.ReadMapping(name).ToList();
				var yamlDictionaryList = valueDic.ToList();

				Assert.AreEqual(yamlDictionaryList.Count, minimalLookupList.Count);
				for (var i = 0; i < yamlDictionaryList.Count; i++)
				{
					var yamlDic = yamlDictionaryList[i];
					var minimalLookup = minimalLookupList[i];

					foreach (var seqKVPair in yamlDic)
					{
						Assert.True(minimalLookup.ContainsKey(seqKVPair.Key), seqKVPair.Key);
						var lookupResult = minimalLookup[seqKVPair.Key];
						Assert.AreEqual(seqKVPair.Value, lookupResult);
					}
				}
			}
		}
	}
}