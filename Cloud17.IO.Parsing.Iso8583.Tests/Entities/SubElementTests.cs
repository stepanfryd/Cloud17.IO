using Cloud17.IO.Parsing.Iso8583.Entities;

using Moq;

using System;

using Xunit;

namespace Cloud17.IO.Parsing.Iso8583.Tests.Entities
{
	public class SubElementTests
	{
		private MockRepository mockRepository;



		public SubElementTests()
		{
			this.mockRepository = new MockRepository(MockBehavior.Strict);


		}

		private SubElement CreateSubElement()
		{
			return new SubElement();
		}

		[Fact]
		public void TestMethod1()
		{
			// Arrange
			var subElement = this.CreateSubElement();

			// Act


			// Assert
			Assert.True(false);
			this.mockRepository.VerifyAll();
		}
	}
}
