
# UAParser.FormFactor

[![Build status](https://img.shields.io/appveyor/ci/thyn/ua-parser-formfactor.svg)](https://ci.appveyor.com/project/thyn/ua-parser-formfactor) [![AppVeyor tests](https://img.shields.io/appveyor/tests/thyn/ua-parser-formfactor.svg)](https://ci.appveyor.com/project/thyn/ua-parser-formfactor/build/tests) [![NuGet](https://img.shields.io/nuget/vpre/UAParser.FormFactor.svg)](https://www.nuget.org/packages/UAParser.FormFactor/)

A .net wrapper for the ua-parser library with form factors (Mobile,Table,Desktop,Spider). No hidden references to System.Net.Http. Based on http://detectmobilebrowsers.com and https://github.com/ua-parser/uap-csharp

# Changes


2018.07.04 Fork UAParser to support .net standart 2.0. Update yaml files. Fix forked tests to support new yaml test files. Remove hell with System.Net.http. Remove custom wrappers for old UAParser library. Supports .Net 4.6.1+
2017.09.22 new release to support version 3.0.0 of UAParser , built only for 4.5 framework.

# Installation

https://www.nuget.org/packages/UAParser.FormFactor/

# Supported form factors.

1. Mobile
2. Tablet
3. Desktop
4. Spider

# Usage

	  using UAParser;

...

  string uaString = "Mozilla/5.0 (iPhone; CPU iPhone OS 5_1_1 like Mac OS X) AppleWebKit/534.46 (KHTML, like Gecko) Version/5.1 Mobile/9B206 Safari/7534.48.3";

  // get a parser with the embedded regex patterns
  var uaParser = Parser.GetDefault();
  
  // get a parser using externally supplied yaml definitions
  // var uaParser = Parser.FromYamlFile(pathToYamlFile);
  // var uaParser = Parser.FromYaml(yamlString);

  ClientInfo c = uaParser.Parse(uaString);

  Console.WriteLine(c.UserAgent.Family); // => "Mobile Safari"
  Console.WriteLine(c.UserAgent.Major);  // => "5"
  Console.WriteLine(c.UserAgent.Minor);  // => "1"
  Console.WriteLine(c.OS.Family);        // => "iOS"
  Console.WriteLine(c.OS.Major);         // => "5"
  Console.WriteLine(c.OS.Minor);         // => "1"
  Console.WriteLine(c.Device.Family);    // => "iPhone"
  Console.WriteLine(result.Device.FormFactor);  // => Mobile
	

