using Cloud17.IO.Parsing.Iso8583.Configuration;
using Cloud17.IO.Parsing.Iso8583.Serialization;

using Moq;

using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

using Xunit;
using Xunit.Abstractions;

namespace Cloud17.IO.Parsing.Iso8583.Tests.Configuration
{
	public class ParserConfigurationTests : TestBase
	{
		private MockRepository _mockRepository;

		public ParserConfigurationTests(ConfigurationFixture configurationFixture, ITestOutputHelper output) : base(configurationFixture, output)
		{
			_mockRepository = new MockRepository(MockBehavior.Strict);

		}

		[Fact]
		public void ConfigurationSerializationTest_ExpectedBehavior()
		{
			var cont = System.IO.File.ReadAllText(Files.ISO8583_PARSER_CONFIG);

			var config = JsonSerializer.Deserialize<ParserConfiguration>(cont, new JsonSerializerOptions
			{
				Converters = {
							new DataElementInfoSerializer()
						}
			});

			Assert.NotNull(config);
			Assert.True(config.DataElements.Any());
			Assert.True(config.DataElements.ContainsKey(3));
			Assert.True(config.DataElements[3].Fields.Count == 3);
			Assert.Null(config.DataElements[3].Fields[2].Code);
			Assert.Null(config.DataElements[3].Fields[2].Justification);
			Assert.True(config.DataElements[3].Fields[2].Length == 2);
			Assert.True(config.DataElements[3].Fields[2].Name == "Cardholder “To” Account Type Code");

			Assert.True(config.SubDataElementFields.Any());
		}
	}
}
