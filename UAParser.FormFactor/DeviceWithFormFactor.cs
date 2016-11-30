namespace UAParser.FormFactor
{
	public class DeviceWithFormFactor
	{
		private readonly Device _device;
		private readonly DeviceFormFactor _formFactor;

		public Device OriginalDevice => _device;

		public DeviceWithFormFactor(Device device, DeviceFormFactor formFactor)
		{
			_device = device;
			_formFactor = formFactor;
		}

		/// <summary>
		/// Device Form Factor
		/// </summary>
		public DeviceFormFactor FormFactor => _formFactor;

		public bool IsSpider => _device.IsSpider == true;

		/// <summary>The brand of the device</summary>
		public string Brand => _device.Brand;

		/// <summary>The family of the device, if available</summary>
		public string Family => _device.Family;

		/// <summary>The model of the device, if available</summary>
		public string Model => _device.Model;

		/// <summary>A readable description of the device</summary>
		public override string ToString()
		{
			return this.Family;
		}
	}
}