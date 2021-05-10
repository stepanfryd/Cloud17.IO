using Microsoft.Extensions.Logging;

using System;
using Xunit.Abstractions;

namespace Cloud17.IO.Parsing.Iso8583.Tests.TestComponents
{
	public class TestLogger<T> : ILogger<T>
	{
		private readonly ITestOutputHelper _output;

		public TestLogger(ITestOutputHelper output)
		{
			_output = output;
		}
		public IDisposable BeginScope<TState>(TState state)
		{
			throw new NotImplementedException();
		}

		public bool IsEnabled(LogLevel logLevel)
		{
			return true;
		}

		public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
		{
			_output.WriteLine($"=============== {logLevel} ${eventId} ===============");
			_output.WriteLine(state.ToString());
			if(exception!=null)
			{
				_output.WriteLine(exception.Message);
				_output.WriteLine(exception.StackTrace);
			}
			_output.WriteLine($"===============");
		}
	}
}
