using Cloud17.IO.Parsing.Iso8583.Configuration;
using Cloud17.IO.Parsing.Iso8583.Parsing;
using Cloud17.IO.Parsing.Iso8583.Tests.TestComponents;

using Moq;

using System.Linq;

using Xunit;
using Xunit.Abstractions;

namespace Cloud17.IO.Parsing.Iso8583.Tests.Parsing
{
	public class Iso8583ParserTests : TestBase
	{

		private MockRepository _mockRepository;

		public Iso8583ParserTests(ConfigurationFixture configurationFixture, ITestOutputHelper output) : base(configurationFixture, output)
		{
			_mockRepository = new MockRepository(MockBehavior.Strict);
		}

		[Fact]
		public void Dispose_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			var iso8583Parser = new Iso8583Parser(
				new TestLogger<Iso8583Parser>(Output),
				new byte[] { },
				new Iso8583ParserConfig
				{
					DataElementConfigPath = Files.ISO8583_PARSER_CONFIG
				});

			// Act
			iso8583Parser.Dispose();

			// Assert
			_mockRepository.VerifyAll();
		}

		[Theory]
		[InlineData(Files.T112_TEST_FILE_01, "UTF-8")]
		[InlineData(Files.T112_TEST_FILE_02, "IBM037")]
		[InlineData(Files.T112_TEST_FILE_03, "IBM037")]
		[InlineData(Files.T112_TEST_FILE_04, "IBM037")]
		[InlineData(Files.T112_TEST_FILE_05, "IBM037")]
		[InlineData(Files.T112_TEST_FILE_06, "IBM037")]
		[InlineData(Files.T112_TEST_FILE_07, "IBM037")]
		public void Parse_StateUnderTest_ExpectedBehavior(string inputFile, string encoding)
		{
			// Arrange
			var iso8583Parser = new Iso8583Parser(
				new TestLogger<Iso8583Parser>(Output),
				System.IO.File.ReadAllBytes(inputFile),
				new Iso8583ParserConfig
				{
					DataElementConfigPath = Files.ISO8583_PARSER_CONFIG,
					SourceEncodingName = encoding
				});

			// Act
			iso8583Parser.Parse();

			// Assert
			Assert.NotNull(iso8583Parser);
			Assert.True(iso8583Parser.Documents.Count>0);
			_mockRepository.VerifyAll();
		}
	}
}
