# HAProxy Api Client

Simple C# library to control HAProxy over TCP port.

# Installation

https://www.nuget.org/packages/UAParser.FormFactor/

# Overview

Small extension for [UAParser](https://www.nuget.org/packages/UAParser) to detect device's form factor by useragent. Based on http://detectmobilebrowsers.com.

# Supported form factors.

1. Mobile
2. Tablet
3. Desktop
4. Spider

# Usage

	  var parser = Parser.GetDefault(); //using UAParser
		var result = parser.Parse(userAgent);
    Console.WriteLine(result.GetFormFactor());
	

