


# UAParser.FormFactor

Small extension for [UAParser](https://www.nuget.org/packages/UAParser) to detect device's form factor by useragent. Based on http://detectmobilebrowsers.com.

# Installation

2017.09.22 new release to support version 3.0.0 of UAParser , built only for 4.5 framework.

https://www.nuget.org/packages/UAParser.FormFactor/

# Supported form factors.

1. Mobile
2. Tablet
3. Desktop
4. Spider

# Usage

	var parser = Parser.GetDefault(); //using UAParser
	var result = parser.ParseDeviceWithFormFactor(userAgent); // or ParseWithFormFactor
	Console.WriteLine(result.FormFactor);
	

