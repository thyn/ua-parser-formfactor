namespace UAParser.FormFactor.Models
{
	/// <summary>
	/// Represents the user agent client information resulting from parsing
	/// a user agent string
	/// </summary>
	public class ClientInfo : IUAParserOutput
	{
		/// <summary>
		/// Constructs an instance of the ClientInfo with results of the user agent string parsing
		/// </summary>
		public ClientInfo(string inputString, OS os, Device device, UserAgent userAgent)
		{
			String = inputString;
			OS = os;
			Device = device;
			UA = userAgent;
		}

		/// <summary>
		/// The user agent string, the input for the UAParser
		/// </summary>
		public string String { get; }

		/// <summary>
		/// The OS parsed from the user agent string
		/// </summary>
		public OS OS { get; }

		/// <summary>
		/// The Device parsed from the user agent string
		/// </summary>
		public Device Device { get; }

		/// <summary>
		/// The User Agent parsed from the user agent string
		/// </summary>
		public UserAgent UserAgent => UA;

		/// <summary>
		/// The User Agent parsed from the user agent string
		/// </summary>
		public UserAgent UA { get; }

		/// <summary>
		/// A readable description of the user agent client information
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return $"{OS} {Device} {UA}";
		}
	}
}