using Cloud17.IO.Parsing.Configuration;
using Cloud17.IO.Parsing.Interfaces;
using Cloud17.IO.Parsing.Logging;
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
		#region Public Properties

		/// <summary>
		/// List of documents in file
		/// </summary>
		public IList<IReportDocument> Documents { get; }

		/// <summary>
		/// File parser configuration
		/// </summary>
		public TConfiguration ParserConfiguration { get; }

		/// <summary>
		/// Specified if file has been identified as known file
		/// </summary>
		public bool IsKnownFile { get; set; }

		#endregion Public Properties

		#region Protected Properties

		/// <summary>
		/// Byte array of file content
		/// </summary>
		protected byte[] FileContent { get; set; }

		/// <summary>
		/// Logger instance
		/// </summary>
		protected ILog Log { get; set; }

		#endregion Protected Properties

		#region Public Constructors

		/// <summary>
		/// Base constructor
		/// </summary>
		/// <param name="fileContent">Content of parsed file</param>
		/// <param name="configuration">Parser configuration</param>
		protected BaseSchemeParser(byte[] fileContent, TConfiguration configuration)
		{
			Log = LogProvider.GetLogger(GetType());
			Documents = new List<IReportDocument>();

			FileContent = fileContent;
			ParserConfiguration = configuration;
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