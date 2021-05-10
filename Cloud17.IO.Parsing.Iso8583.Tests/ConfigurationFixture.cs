using Microsoft.Extensions.Configuration;

using System;

namespace Cloud17.IO.Parsing.Iso8583.Tests
{
	public class ConfigurationFixture : IDisposable
	{
		public IConfiguration Configuration { get; set; }

		public ConfigurationFixture()
		{
			Configuration = new ConfigurationBuilder()
				.AddEnvironmentVariables()
				.SetBasePath(AppContext.BaseDirectory)
				.AddJsonFile("appsettings.json", false, true)
				.Build();
		}

		public void Dispose()
		{

		}
	}
}
