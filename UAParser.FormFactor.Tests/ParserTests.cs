using NUnit.Framework;

namespace UAParser.FormFactor.Tests
{
	[TestFixture]
	[Parallelizable]
	public class ParserTests
	{
		[Test]
		public void CanGetDefaultParser()
		{
			var parser = Parser.GetDefault();
			Assert.NotNull(parser);
		}

		[Test]
		public void CanGetParserFromInput()
		{
			var yamlContent = this.GetTestResources("UAParser.FormFactor.Tests.Resources.regexes.yaml");
			var parser = Parser.FromYaml(yamlContent);
			Assert.NotNull(parser);
		}
	}
}