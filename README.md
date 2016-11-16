# UAParser.FormFactor

Small extension for [UAParser](https://www.nuget.org/packages/UAParser) to detect device's form factor by useragent. Based on http://detectmobilebrowsers.com.

# Installation

https://www.nuget.org/packages/UAParser.FormFactor/

# Supported form factors.

1. Mobile
2. Tablet
3. Desktop
4. Spider

# Usage

	var parser = Parser.GetDefault(); //using UAParser
	var result = parser.Parse(userAgent);
	Console.WriteLine(result.GetFormFactor());
	

