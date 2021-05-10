using Cloud17.IO.Parsing.Iso8583.Entities;

using Moq;

using System;

using Xunit;

namespace Cloud17.IO.Parsing.Iso8583.Tests.Entities
{
	public class SubFieldTests
	{
		private MockRepository mockRepository;



		public SubFieldTests()
		{
			this.mockRepository = new MockRepository(MockBehavior.Strict);


		}

		private SubField CreateSubField()
		{
			return new SubField();
		}

		[Fact]
		public void TestMethod1()
		{
			// Arrange
			var subField = this.CreateSubField();

			// Act


			// Assert
			Assert.True(false);
			this.mockRepository.VerifyAll();
		}
	}
}
