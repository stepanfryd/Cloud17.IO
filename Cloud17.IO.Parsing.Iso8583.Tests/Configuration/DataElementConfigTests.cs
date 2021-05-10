using Cloud17.IO.Parsing.Iso8583.Configuration;

using Moq;

using System;
using System.Collections.Generic;

using Xunit;

namespace Cloud17.IO.Parsing.Iso8583.Tests.Configuration
{
	public class DataElementConfigTests
	{
		private MockRepository mockRepository;

		private Mock<IDictionary<int, DataElementInfo>> mockDictionary;

		public DataElementConfigTests()
		{
			this.mockRepository = new MockRepository(MockBehavior.Strict);

			this.mockDictionary = this.mockRepository.Create<IDictionary<int, DataElementInfo>>();
		}

		private DataElementConfig CreateDataElementConfig()
		{
			return new DataElementConfig(
					this.mockDictionary.Object);
		}

		[Fact]
		public void LoadPrimaryBitmap_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			var dataElementConfig = this.CreateDataElementConfig();
			bool[] primaryBitmap = null;

			// Act
			dataElementConfig.LoadPrimaryBitmap(
				primaryBitmap);

			// Assert
			Assert.True(false);
			this.mockRepository.VerifyAll();
		}
	}
}
