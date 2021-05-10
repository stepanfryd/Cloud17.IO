using Cloud17.IO.Parsing.Iso8583.Entities;

using Moq;

using System;

using Xunit;

namespace Cloud17.IO.Parsing.Iso8583.Tests.Entities
{
	public class DataElementTests
	{
		private MockRepository mockRepository;



		public DataElementTests()
		{
			this.mockRepository = new MockRepository(MockBehavior.Strict);


		}

		private DataElement CreateDataElement()
		{
			return new DataElement();
		}

		[Fact]
		public void ShouldSerializeSubElements_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			var dataElement = this.CreateDataElement();

			// Act
			var result = dataElement.ShouldSerializeSubElements();

			// Assert
			Assert.True(false);
			this.mockRepository.VerifyAll();
		}

		[Fact]
		public void ShouldSerializeSubFields_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			var dataElement = this.CreateDataElement();

			// Act
			var result = dataElement.ShouldSerializeSubFields();

			// Assert
			Assert.True(false);
			this.mockRepository.VerifyAll();
		}
	}
}
