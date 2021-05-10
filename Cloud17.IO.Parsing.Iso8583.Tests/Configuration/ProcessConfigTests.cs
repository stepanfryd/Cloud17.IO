using Cloud17.IO.Parsing.Iso8583.Configuration;

using Moq;

using System;

using Xunit;

namespace Cloud17.IO.Parsing.Iso8583.Tests.Configuration
{
	public class ProcessConfigTests
	{
		private MockRepository mockRepository;



		public ProcessConfigTests()
		{
			this.mockRepository = new MockRepository(MockBehavior.Strict);


		}

		private ProcessConfig CreateProcessConfig()
		{
			return new ProcessConfig();
		}

		[Fact]
		public void TestMethod1()
		{
			// Arrange
			var processConfig = this.CreateProcessConfig();

			// Act


			// Assert
			Assert.True(false);
			this.mockRepository.VerifyAll();
		}
	}
}
