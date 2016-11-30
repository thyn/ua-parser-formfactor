namespace UAParser.FormFactor
{
	public class CientInfoWithFormFactor
	{
		private readonly ClientInfo _info;
		public string String => _info.String;
		public OS OS => _info.OS;
		private DeviceWithFormFactor Device { get; }
		public UserAgent UserAgent => _info.UserAgent;
		public UserAgent UA => _info.UA;
		public override string ToString()
		{
			return string.Format("{0} {1} {2}", (object) this.OS, (object) this.Device, (object) this.UA);
		}

		public CientInfoWithFormFactor(ClientInfo info, DeviceFormFactor formFactor)
		{
			_info = info;
			Device = new DeviceWithFormFactor(info.Device, formFactor);
		}
	}
}