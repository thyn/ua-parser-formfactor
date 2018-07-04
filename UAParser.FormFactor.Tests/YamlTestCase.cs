using NUnit.Framework;
using UAParser.FormFactor.Models;

namespace UAParser.FormFactor.Tests
{
	public abstract class YamlTestCase
	{
		public string UserAgent { get; set; }

		public abstract void Verify(ClientInfo clientInfo);

		protected void AssertMatch<T>(T expected, T actual, string type)
		{
			if (typeof(T) == typeof(string))
			{
				var exp = expected as string;
				var act = actual as string;

				if (string.IsNullOrEmpty(exp) && string.IsNullOrEmpty(act))
				{
					return;
				}
			}

			Assert.True(expected.Equals(actual), type + " did not match. (expected:" + expected + " actual:" + actual + ")  in " + UserAgent);
		}
	}
}