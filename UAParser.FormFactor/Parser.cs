using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using UAParser.FormFactor.Models;
using UAParser.FormFactor.Utils;

namespace UAParser.FormFactor
{
	/// <summary>
	/// Represents a parser of a user agent string
	/// </summary>
	public class Parser : IUAParser
	{
		private static readonly Regex _mobileUserAgentCommon = CreateFormFactorRegex(@"(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|iris|kindle|lge |maemo|midp|mmp|mobile.+firefox|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows ce|xda|xiino");
		private static readonly Regex _mobileUserAgentExtra = CreateFormFactorRegex(@"1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-");
		private static readonly Regex _tabletUserAgent = CreateFormFactorRegex(@"android|ipad|playbook|silk|tablet");

		private readonly Func<string, OS> _osParser;
		private readonly Func<string, Device> _deviceParser;
		private readonly Func<string, UserAgent> _userAgentParser;

		private Parser(MinimalYamlParser yamlParser, ParserOptions options)
		{
			const string other = "Other";

			var config = new Config(options ?? new ParserOptions());

			_userAgentParser = CreateParser(Read(yamlParser.ReadMapping("user_agent_parsers"), config.UserAgentSelector), new UserAgent(other, null, null, null));
			_osParser = CreateParser(Read(yamlParser.ReadMapping("os_parsers"), config.OSSelector), new OS(other, null, null, null, null));
			_deviceParser = CreateParser(Read(yamlParser.ReadMapping("device_parsers"), config.DeviceSelector), new Device(other, string.Empty, string.Empty, DeviceFormFactor.Desktop));
		}

		private static Regex CreateFormFactorRegex(string pattern)
		{
			return new Regex(pattern, RegexOptions.Multiline | RegexOptions.IgnoreCase | RegexOptions.Compiled);
		}

		private static IEnumerable<T> Read<T>(IEnumerable<Dictionary<string, string>> entries, Func<Func<string, string>, T> selector)
		{
			return from cm in entries select selector(cm.Find);
		}

		/// <summary>
		/// Returns a <see cref="Parser"/> instance based on the regex definitions in a yaml string
		/// </summary>
		/// <param name="yaml">a string containing yaml definitions of reg-ex</param>
		/// <param name="parserOptions">specifies the options for the parser</param>
		/// <returns>A <see cref="Parser"/> instance parsing user agent strings based on the regexes defined in the yaml string</returns>
		public static IUAParser FromYaml(string yaml, ParserOptions parserOptions = null)
		{
			return new Parser(new MinimalYamlParser(yaml), parserOptions);
		}

		/// <summary>
		/// Returns a <see cref="Parser"/> instance based on the embedded regex definitions.
		/// <remarks>The embedded regex definitions may be outdated. Consider passing in external yaml definitions using <see cref="Parser.FromYaml"/></remarks>
		/// </summary>
		/// <param name="parserOptions">specifies the options for the parser</param>
		/// <returns></returns>
		public static IUAParser GetDefault(ParserOptions parserOptions = null)
		{
			using (var stream = typeof(Parser).GetTypeInfo().Assembly.GetManifestResourceStream("UAParser.FormFactor.regexes.yaml"))
			using (var reader = new StreamReader(stream))
			{
				return new Parser(new MinimalYamlParser(reader.ReadToEnd()), parserOptions);
			}
		}

		/// <summary>
		/// Parse a user agent string and obtain all client information
		/// </summary>
		public ClientInfo Parse(string uaString)
		{
			var os = ParseOS(uaString);
			var device = ParseDevice(uaString);
			var ua = ParseUserAgent(uaString);

			return new ClientInfo(uaString, os, device, ua);
		}

		/// <summary>
		/// Parse a user agent string and obtain the OS information
		/// </summary>
		public OS ParseOS(string uaString)
		{
			return _osParser(uaString);
		}

		/// <summary>
		/// Parse a user agent string and obtain the device information
		/// </summary>
		public Device ParseDevice(string uaString)
		{
			return _deviceParser(uaString);
		}

		/// <summary>
		/// Parse a user agent string and obtain the UserAgent information
		/// </summary>
		public UserAgent ParseUserAgent(string uaString)
		{
			return _userAgentParser(uaString);
		}

		private static Func<string, T> CreateParser<T>(IEnumerable<Func<string, T>> parsers, T defaultValue)
			where T : class
		{
			return CreateParser(parsers, defaultValue, t => t);
		}

		private static Func<string, TResult> CreateParser<T, TResult>(IEnumerable<Func<string, T>> parsers, T defaultValue, Func<T, TResult> selector)
			where T : class
		{
			parsers = parsers?.ToArray() ?? Enumerable.Empty<Func<string, T>>();
			return ua => selector(parsers.Select(p => p(ua)).FirstOrDefault(m => m != null) ?? defaultValue);
		}

		#region CLASSES

		private class Config
		{
			private readonly ParserOptions _options;

			internal Config(ParserOptions options)
			{
				_options = options;
			}

			public Func<string, OS> OSSelector(Func<string, string> indexer)
			{
				var regex = Regex(indexer, "OS");
				var os = indexer("os_replacement");
				var v1 = indexer("os_v1_replacement");
				var v2 = indexer("os_v2_replacement");
				var v3 = indexer("os_v3_replacement");
				var v4 = indexer("os_v4_replacement");

				return Parsers.OS(regex, os, v1, v2, v3, v4);
			}

			public Func<string, UserAgent> UserAgentSelector(Func<string, string> indexer)
			{
				var regex = Regex(indexer, "User agent");
				var family = indexer("family_replacement");
				var v1 = indexer("v1_replacement");
				var v2 = indexer("v2_replacement");
				var v3 = indexer("v3_replacement");

				return Parsers.UserAgent(regex, family, v1, v2, v3);
			}

			public Func<string, Device> DeviceSelector(Func<string, string> indexer)
			{
				var regex = Regex(indexer, "Device", indexer("regex_flag"));
				var device = indexer("device_replacement");
				var brand = indexer("brand_replacement");
				var model = indexer("model_replacement");

				return Parsers.Device(regex, device, brand, model);
			}

			private Regex Regex(Func<string, string> indexer, string key, string regexFlag = null)
			{
				var pattern = indexer("regex");
				if (pattern == null)
				{
					throw new Exception($"{key} is missing regular expression specification.");
				}

				// Some expressions in the regex.yaml file causes parsing errors
				// in .NET such as the \_ token so need to alter them before
				// proceeding.

				if (pattern.IndexOf(@"\_", StringComparison.Ordinal) >= 0)
				{
					pattern = pattern.Replace(@"\_", "_");
				}

				// TODO: potentially allow parser to specify e.g. to use
				// compiled regular expressions which are faster but increase
				// startup time
				var options = RegexOptions.None;
				if ("i".Equals(regexFlag))
				{
					options |= RegexOptions.IgnoreCase;
				}

#if REGEX_COMPILATION
                if (_options.UseCompiledRegex)
                {
                    options |= RegexOptions.Compiled;
                }
#endif

				return new Regex(pattern, options);
			}
		}

		private static class Parsers
		{
			private static bool IsMobile(string ua)
			{
				var userAgent = ua.ToLower();
				return _mobileUserAgentCommon.IsMatch(userAgent) ||
					   (userAgent.Length >= 4 && _mobileUserAgentExtra.IsMatch(userAgent.Substring(0, 4)));
			}

			private static bool IsTablet(string ua)
			{
				var userAgent = ua.ToLower();
				return _tabletUserAgent.IsMatch(userAgent);
			}

			private static DeviceFormFactor GetFormFactor(string userAgent, bool isSpider)
			{
				if (isSpider)
				{
					return DeviceFormFactor.Spider;
				}

				if (IsMobile(userAgent))
				{
					return DeviceFormFactor.Mobile;
				}

				if (IsTablet(userAgent))
				{
					return DeviceFormFactor.Tablet;
				}

				return DeviceFormFactor.Desktop;
			}

			private static bool IsSpider(string family)
			{
				return "Spider".Equals(family, StringComparison.OrdinalIgnoreCase);
			}

			public static Func<string, OS> OS(Regex regex, string osReplacement, string v1Replacement, string v2Replacement, string v3Replacement, string v4Replacement)
			{
				return Create(regex, from family in Replace(osReplacement, "$1")
									 from v1 in Replace(v1Replacement, "$2")
									 from v2 in Replace(v2Replacement, "$3")
									 from v3 in Replace(v3Replacement, "$4")
									 from v4 in Replace(v4Replacement, "$5")
									 select new OS(family, v1, v2, v3, v4));
			}

			public static Func<string, Device> Device(Regex regex, string familyReplacement, string brandReplacement, string modelReplacement)
			{
				return Create(regex, (input, match, nums) =>
				{
					var binder =
						from family in ReplaceAll(familyReplacement)
						from brand in ReplaceAll(brandReplacement)
						from model in ReplaceAll(modelReplacement)
						select new
						{
							Family = family,
							Brand = brand,
							Model = model
						};

					var parsedDevice = binder(match, nums);

					return new Device(parsedDevice.Family, parsedDevice.Brand, parsedDevice.Model, GetFormFactor(input, IsSpider(parsedDevice.Family)));
				});
			}

			public static Func<string, UserAgent> UserAgent(Regex regex, string familyReplacement, string majorReplacement, string minorReplacement, string patchReplacement)
			{
				return Create(regex, from family in Replace(familyReplacement, "$1")
									 from v1 in Replace(majorReplacement, "$2")
									 from v2 in Replace(minorReplacement, "$3")
									 from v3 in Replace(patchReplacement, "$4")
									 select new UserAgent(family, v1, v2, v3));
			}

			private static Func<Match, IEnumerator<int>, string> Replace(string replacement)
			{
				if (replacement != null)
				{
					return Select(_ => replacement);
				}

				return Select();
			}

			private static Func<Match, IEnumerator<int>, string> Replace(string replacement, string token)
			{
				if (replacement != null)
				{
					if (replacement.Contains(token))
					{
						return Select(s => s != null ? replacement.ReplaceFirstOccurence(token, s) : replacement);
					}
					else if (replacement.Contains("$"))
					{
						return ReplaceAll(replacement);
					}
				}

				return Replace(replacement);
			}

			private static readonly string[] _allReplacementTokens = new string[]
			{
				"$1","$2","$3","$4","$5","$6","$7","$8","$9"
			};

			private static Func<Match, IEnumerator<int>, string> ReplaceAll(string replacement)
			{
				if (replacement == null)
				{
					return Select();
				}

				string ReplaceFunction(string replacementString, string matchedGroup, string token)
				{
					return matchedGroup != null
						? replacementString.ReplaceFirstOccurence(token, matchedGroup)
						: replacementString;
				}

				return (m, num) =>
				{
					var finalString = replacement;
					if (finalString.Contains("$"))
					{
						var groups = m.Groups;
						for (var i = 0; i < _allReplacementTokens.Length; i++)
						{
							var tokenNumber = i + 1;
							var token = _allReplacementTokens[i];
							if (finalString.Contains(token))
							{
								var replacementText = string.Empty;
								Group group;
								if (tokenNumber <= groups.Count && (group = groups[tokenNumber]).Success)
								{
									replacementText = group.Value;
								}

								finalString = ReplaceFunction(finalString, replacementText, token);
							}

							if (!finalString.Contains("$"))
							{
								break;
							}
						}
					}

					return finalString;
				};
			}

			private static Func<Match, IEnumerator<int>, string> Select()
			{
				return Select(v => v);
			}

			private static Func<Match, IEnumerator<int>, T> Select<T>(Func<string, T> selector)
			{
				return (m, num) =>
				{
					if (!num.MoveNext())
					{
						throw new InvalidOperationException();
					}

					var groups = m.Groups;
					Group group;
					return selector(num.Current <= groups.Count && (group = groups[num.Current]).Success
									? group.Value : null);
				};
			}

			private static Func<string, T> Create<T>(Regex regex, Func<string, Match, IEnumerator<int>, T> binder)
			{
				return input =>
				{
					var m = regex.Match(input);
					var num = Generate(1, n => n + 1);
					return m.Success ? binder(input, m, num) : default;
				};
			}

			private static Func<string, T> Create<T>(Regex regex, Func<Match, IEnumerator<int>, T> binder)
			{
				return Create(regex, (input, match, nums) => binder(match, nums));
			}

			private static IEnumerator<T> Generate<T>(T initial, Func<T, T> next)
			{
				for (var state = initial; ; state = next(state))
				{
					yield return state;
				}
			}
		}

		#endregion CLASSES
	}
}