using Cloud17.IO.Parsing.Iso8583.Entities;

using Moq;

using System;

using Xunit;

namespace Cloud17.IO.Parsing.Iso8583.Tests.Entities
{
	public class MessageTests
	{
		private MockRepository mockRepository;



		public MessageTests()
		{
			this.mockRepository = new MockRepository(MockBehavior.Strict);


		}

		private Message CreateMessage()
		{
			return new Message();
		}

		[Fact]
		public void IsValid_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			var message = this.CreateMessage();

			// Act
			var result = message.IsValid();

			// Assert
			Assert.True(false);
			this.mockRepository.VerifyAll();
		}

		[Fact]
		public void ShouldSerializeDataElements_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			var message = this.CreateMessage();

			// Act
			var result = message.ShouldSerializeDataElements();

			// Assert
			Assert.True(false);
			this.mockRepository.VerifyAll();
		}
	}
}
