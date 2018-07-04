using System;
using System.Collections.Generic;

namespace UAParser.FormFactor.Utils
{
	/// <summary>
	/// Just enough string parsing to recognize the regexes.yaml file format. Introduced to remove
	/// dependency on large Yaml parsing lib. Note that a unittest ensures compatibility
	/// by ensuring regexes and properties are read similar to using the full yaml lib
	/// </summary>
	internal class MinimalYamlParser
	{
		private readonly Dictionary<string, Mapping> _mappings = new Dictionary<string, Mapping>();

		public MinimalYamlParser(string yamlString)
		{
			ReadIntoMappingModel(yamlString);
		}

		internal IDictionary<string, Mapping> Mappings => _mappings;

		public IEnumerable<Dictionary<string, string>> ReadMapping(string mappingName)
		{
			if (_mappings.TryGetValue(mappingName, out var mapping))
			{
				foreach (var s in mapping.Sequences)
				{
					var temp = s;
					yield return temp;
				}
			}
		}

		private static string ReadQuotedValue(string value)
		{
			if (value.StartsWith("'") && value.EndsWith("'"))
			{
				return value.Substring(1, value.Length - 2);
			}

			if (value.StartsWith("\"") && value.EndsWith("\""))
			{
				return value.Substring(1, value.Length - 2);
			}

			return value;
		}

		private void ReadIntoMappingModel(string yamlInputString)
		{
			// line splitting using various splitting characters
			var lines = yamlInputString.Split(new[] { Environment.NewLine, "\r", "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
			var lineCount = 0;
			Mapping activeMapping = null;

			foreach (var line in lines)
			{
				lineCount++;
				if (line.Trim().StartsWith("#")) //skipping comments
				{
					continue;
				}

				if (line.Trim().Length == 0)
				{
					continue;
				}

				//is this a new mapping entity
				if (line[0] != ' ')
				{
					var indexOfMappingColon = line.IndexOf(':');
					if (indexOfMappingColon == -1)
					{
						throw new ArgumentException("YamlParsing: Expecting mapping entry to contain a ':', at line " + lineCount);
					}

					var name = line.Substring(0, indexOfMappingColon).Trim();
					activeMapping = new Mapping();
					_mappings.Add(name, activeMapping);
					continue;
				}

				//reading scalar entries into the active mapping
				if (activeMapping == null)
				{
					throw new ArgumentException("YamlParsing: Expecting mapping entry to contain a ':', at line " + lineCount);
				}

				var seqLine = line.Trim();
				if (seqLine[0] == '-')
				{
					activeMapping.BeginSequence();
					seqLine = seqLine.Substring(1);
				}

				var indexOfColon = seqLine.IndexOf(':');
				if (indexOfColon == -1)
				{
					throw new ArgumentException("YamlParsing: Expecting scalar mapping entry to contain a ':', at line " + lineCount);
				}

				var key = seqLine.Substring(0, indexOfColon).Trim();
				var value = ReadQuotedValue(seqLine.Substring(indexOfColon + 1).Trim());
				activeMapping.AddToSequence(key, value);
			}
		}

		#region CLASSES

		internal class Mapping
		{
			private Dictionary<string, string> _lastEntry;

			public Mapping()
			{
				Sequences = new List<Dictionary<string, string>>();
			}

			public List<Dictionary<string, string>> Sequences { get; }

			public void BeginSequence()
			{
				_lastEntry = new Dictionary<string, string>();
				Sequences.Add(_lastEntry);
			}

			public void AddToSequence(string key, string value)
			{
				_lastEntry[key] = value;
			}
		}

		#endregion CLASSES
	}
}