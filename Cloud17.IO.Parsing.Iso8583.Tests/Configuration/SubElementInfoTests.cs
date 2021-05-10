using Cloud17.IO.Parsing.Iso8583.Configuration;

using Moq;

using System;

using Xunit;

namespace Cloud17.IO.Parsing.Iso8583.Tests.Configuration
{
	public class SubElementInfoTests
	{
		private MockRepository mockRepository;



		public SubElementInfoTests()
		{
			this.mockRepository = new MockRepository(MockBehavior.Strict);


		}

		private SubElementInfo CreateSubElementInfo()
		{
			return new SubElementInfo();
		}

		[Fact]
		public void TestMethod1()
		{
			// Arrange
			var subElementInfo = this.CreateSubElementInfo();

			// Act


			// Assert
			Assert.True(false);
			this.mockRepository.VerifyAll();
		}
	}
}
