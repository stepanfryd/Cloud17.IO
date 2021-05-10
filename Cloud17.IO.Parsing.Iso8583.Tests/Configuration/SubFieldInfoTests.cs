using Cloud17.IO.Parsing.Iso8583.Configuration;

using Moq;

using System;

using Xunit;

namespace Cloud17.IO.Parsing.Iso8583.Tests.Configuration
{
	public class SubFieldInfoTests
	{
		private const string _fieldName = "FIELD_NAME";
		private const int _fieldLength = 10;

		private MockRepository mockRepository;



		public SubFieldInfoTests()
		{
			this.mockRepository = new MockRepository(MockBehavior.Strict);


		}

		private SubFieldInfo CreateSubFieldInfo()
		{
			return new SubFieldInfo(_fieldLength, _fieldName);
		}

		[Fact]
		public void TestMethod1()
		{
			// Arrange
			var subFieldInfo = this.CreateSubFieldInfo();

			// Act


			// Assert
			Assert.True(false);
			this.mockRepository.VerifyAll();
		}
	}
}
