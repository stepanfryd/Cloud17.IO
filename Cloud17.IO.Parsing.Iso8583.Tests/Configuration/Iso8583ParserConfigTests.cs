using Cloud17.IO.Parsing.Iso8583.Configuration;

using Moq;

using System;

using Xunit;

namespace Cloud17.IO.Parsing.Iso8583.Tests.Configuration
{
	public class Iso8583ParserConfigTests
	{
		private MockRepository mockRepository;



		public Iso8583ParserConfigTests()
		{
			this.mockRepository = new MockRepository(MockBehavior.Strict);


		}

		private Iso8583ParserConfig CreateIso8583ParserConfig()
		{
			return new Iso8583ParserConfig();
		}

		[Fact]
		public void TestMethod1()
		{
			// Arrange
			var iso8583ParserConfig = this.CreateIso8583ParserConfig();

			// Act


			// Assert
			Assert.True(false);
			this.mockRepository.VerifyAll();
		}
	}
}
