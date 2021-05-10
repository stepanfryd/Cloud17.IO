using Cloud17.IO.Parsing.Iso8583.Configuration;
using Cloud17.IO.Parsing.Iso8583.Exceptions;

using Moq;

using System;

using Xunit;

namespace Cloud17.IO.Parsing.Iso8583.Tests.Configuration
{
	public class DataElementInfoTests
	{
		public DataElementInfoTests()
		{

		}

		[Theory]
		[InlineData(10, true, null, null)]
		[InlineData(20, false, null, null)]
		public void IsValid_StateUnderTest_ExpectedBehavior(int length, bool isVariable, SubElementInfo[] subElement, SubFieldInfo[] subfields)
		{
			DataElementInfo elm;
			/*

			if (fixLen != null && varLen != null && subElement == null && subfields == null)
			{
				Assert.Throws<DataDefinitionException>(() =>
				{
					new DataElementInfo(fixLen, varLen);
				});
			} else if (fixLen != null && varLen == null && subElement == null && subfields == null)
			{
				elm = new DataElementInfo(fixLen, varLen);
				Assert.True(elm.IsValid);
				Assert.Equal(10, elm.FixLen);
				Assert.Null(elm.VarLen);
				Assert.NotNull(elm.Elements);
				Assert.NotNull(elm.Fields);

			}
			else if (fixLen == null && varLen != null && subElement == null && subfields == null)
			{
				elm = new DataElementInfo(fixLen, varLen);
				Assert.True(elm.IsValid);
				Assert.Equal(10, elm.VarLen);
				Assert.Null(elm.FixLen);
				Assert.NotNull(elm.Elements);
				Assert.NotNull(elm.Fields);
			}
			*/

		}
	}
}
