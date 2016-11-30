using System;

namespace UAParser.FormFactor
{
	public static class UAParserExtensions
	{
		public static DeviceWithFormFactor ParseDeviceWithFormFactor(this Parser parser, string uaString)
		{
			var device = parser.ParseDevice(uaString);
			return new DeviceWithFormFactor(device,
				FormFactorParser.GetDefault().GetFormFactor(uaString, device?.IsSpider == true));
		}

		public static CientInfoWithFormFactor ParseWithFormFactor(this Parser parser, string uaString)
		{
			var client = parser.Parse(uaString);
			return new CientInfoWithFormFactor(client,
				FormFactorParser.GetDefault().GetFormFactor(uaString, client?.Device.IsSpider == true));
		}
	}
}