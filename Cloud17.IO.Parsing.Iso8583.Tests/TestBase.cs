
using Microsoft.Extensions.Configuration;

using Xunit;
using Xunit.Abstractions;

namespace Cloud17.IO.Parsing.Iso8583.Tests
{
	public class TestBase : IClassFixture<ConfigurationFixture>
	{
		private readonly ConfigurationFixture _configurationFixture;
		protected ITestOutputHelper Output { get; }
		protected IConfiguration Configuration => _configurationFixture.Configuration;

		protected TestBase(ConfigurationFixture configurationFixture, ITestOutputHelper output)
		{
			_configurationFixture = configurationFixture;
			Output = output;
		}

	}
}
