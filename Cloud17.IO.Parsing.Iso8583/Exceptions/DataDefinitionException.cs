using System;
using System.Runtime.Serialization;

namespace Cloud17.IO.Parsing.Iso8583.Exceptions
{
	public class DataDefinitionException : ApplicationException
	{
		public DataDefinitionException()
		{
		}

		public DataDefinitionException(string message) : base(message)
		{
		}

		public DataDefinitionException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected DataDefinitionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
