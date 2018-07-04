using UAParser.FormFactor.Models;

namespace UAParser.FormFactor
{
	public interface IUAParser
	{
		/// <summary>
		/// Parse a user agent string and obtain all client information
		/// </summary>
		ClientInfo Parse(string uaString);

		/// <summary>
		/// Parse a user agent string and obtain the OS information
		/// </summary>
		OS ParseOS(string uaString);

		/// <summary>
		/// Parse a user agent string and obtain the device information
		/// </summary>
		Device ParseDevice(string uaString);

		/// <summary>
		/// Parse a user agent string and obtain the UserAgent information
		/// </summary>
		UserAgent ParseUserAgent(string uaString);
	}
}