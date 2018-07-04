using UAParser.FormFactor.Utils;

namespace UAParser.FormFactor.Models
{
	/// <summary>
	/// Represents a user agent, commonly a browser
	/// </summary>
	public class UserAgent
	{
		/// <summary>
		/// Construct a UserAgent instance
		/// </summary>
		public UserAgent(string family, string major, string minor, string patch)
		{
			Family = family;
			Major = major;
			Minor = minor;
			Patch = patch;
		}

		/// <summary>
		/// The family of user agent
		/// </summary>
		public string Family { get; }

		/// <summary>
		/// Major version of the user agent, if available
		/// </summary>
		public string Major { get; }

		/// <summary>
		/// Minor version of the user agent, if available
		/// </summary>
		public string Minor { get; }

		/// <summary>
		/// Patch version of the user agent, if available
		/// </summary>
		public string Patch { get; }

		/// <summary>
		/// The user agent as a readbale string
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			var version = VersionString.Format(Major, Minor, Patch);
			return Family + (!string.IsNullOrEmpty(version) ? " " + version : null);
		}
	}
}