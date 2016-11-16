namespace UAParser.FormFactor
{
	public static class UAParserExtensions
	{
		public static DeviceFormFactor GetFormFactor(this ClientInfo info)
		{
			return FormFactorParser.GetDefault().GetFormFactor(info.String, info?.Device?.IsSpider == true);
		}
	}
}