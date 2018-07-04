namespace UAParser.FormFactor.Models
{
	/// <summary>
	/// Represents the physical device the user agent is using
	/// </summary>
	public class Device
	{
		/// <summary>
		/// Constructs a Device instance
		/// </summary>
		public Device(string family, string brand, string model, DeviceFormFactor formFactor)
		{
			Family = family.Trim();

			if (brand != null)
			{
				Brand = brand.Trim();
			}

			if (model != null)
			{
				Model = model.Trim();
			}

			FormFactor = formFactor;
		}

		/// <summary>
		/// Returns true if the device is likely to be a spider or a bot device
		/// </summary>
		public bool IsSpider => FormFactor == DeviceFormFactor.Spider;

		/// <summary>
		///The brand of the device
		/// </summary>
		public string Brand { get; }

		/// <summary>
		/// The family of the device, if available
		/// </summary>
		public string Family { get; }

		/// <summary>
		/// The model of the device, if available
		/// </summary>
		public string Model { get; }

		/// <summary>
		/// A form factor of the device
		/// </summary>
		public DeviceFormFactor FormFactor { get; }

		/// <summary>
		/// A readable description of the device
		/// </summary>
		public override string ToString()
		{
			return Family;
		}
	}
}