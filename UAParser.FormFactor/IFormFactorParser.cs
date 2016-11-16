namespace UAParser.FormFactor
{
	public interface IFormFactorParser
	{
		DeviceFormFactor GetFormFactor(string userAgent,bool isSpider=false);
	}
}