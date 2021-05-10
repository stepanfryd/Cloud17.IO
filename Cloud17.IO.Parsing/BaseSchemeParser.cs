using Cloud17.IO.Parsing.Configuration;
using Cloud17.IO.Parsing.Interfaces;

using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;

namespace Cloud17.IO.Parsing
{
	/// <summary>
	/// Base abastract class for file parser
	/// </summary>
	/// <typeparam name="TConfiguration"></typeparam>
	public abstract class BaseSchemeParser<TConfiguration> :
		IDisposable,
		ISchemeParser<TConfiguration> where TConfiguration : IParserConfiguration
	{
		#region Public and Protected Properties

		/// <summary>
		/// List of documents in file
		/// </summary>
		public IList<IReportDocument> Documents { get; } = new List<IReportDocument>();

		/// <summary>
		/// File parser configuration
		/// </summary>
		public TConfiguration ParserConfiguration { get; }

		/// <summary>
		/// Specified if file has been identified as known file
		/// </summary>
		public bool IsKnownFile { get; set; }

		/// <summary>
		/// Byte array of file content
		/// </summary>
		protected byte[] FileContent { get; set; }

		/// <summary>
		/// Logger instance
		/// </summary>
		protected ILogger Log { get; set; }

		#endregion

		#region Public Constructors

		/// <summary>
		/// Base constructor
		/// </summary>
		/// <param name="fileContent">Content of parsed file</param>
		/// <param name="configuration">Parser configuration</param>
		protected BaseSchemeParser(ILogger logger, byte[] fileContent, TConfiguration configuration)
		{
			Log = logger ?? throw new ArgumentNullException(nameof(logger));
			FileContent = fileContent ?? throw new ArgumentNullException(nameof(fileContent));
			ParserConfiguration = configuration ?? throw new ArgumentNullException(nameof(configuration));
		}

		#endregion Public Constructors

		#region Public Methods

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public abstract void Dispose();

		/// <summary>
		///		Main parser method responsible for file reading/parsing
		/// </summary>
		public abstract void Parse();

		#endregion Public Methods
	}
}