using Cloud17.IO.Parsing.Configuration;

using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Cloud17.IO.Parsing
{
	/// <summary>
	///   Parser file reader. Provides parsing functionality form configuration
	/// </summary>
	public class TextSchemeFileReader : BaseSchemeParser<ParserConfiguration>
	{
		#region Fields and constants

		private readonly string _textContent;
		private List<DocumentSettings> _documentSettings;

		#endregion Fields and constants

		#region Constructors

		/// <summary>
		///   Create instance of file parser
		/// </summary>
		/// <param name="fileContent">
		///   Full path to file hase to be parsed
		/// </param>
		/// <param name="parserConfiguration">
		///   Instance of parser configuration
		/// </param>
		/// <exception cref="NullReferenceException">
		/// </exception>
		/// <exception cref="FileNotFoundException">
		/// </exception>
		public TextSchemeFileReader(ILogger<TextSchemeFileReader> logger, byte[] fileContent, ParserConfiguration parserConfiguration)
			: base(logger, fileContent, parserConfiguration)
		{
			if (fileContent == null || fileContent.Length == 0)
				throw new NullReferenceException("File doesn't containt any data.");
			if (parserConfiguration == null) throw new NullReferenceException("No parser configuration specified.");

			_textContent = Encoding.Default.GetString(FileContent);

			IsKnownFile = DocumentSettings.Count > 0;
		}

		#endregion Constructors

		/// <summary>
		///   Allows an object to try to free resources and perform other cleanup operations before it
		///   is reclaimed by garbage collection.
		/// </summary>
		~TextSchemeFileReader()
		{
			Dispose(false);
		}

		/// <summary>
		///   Method loads (JSON) configuration from specified file and creates instance for
		///   configuration object.
		/// </summary>
		/// <param name="configurationPath">
		///   Path to configuration file
		/// </param>
		/// <returns>
		/// </returns>
		/// <exception cref="NullReferenceException">
		/// </exception>
		/// <exception cref="FileNotFoundException">
		/// </exception>
		public static ParserConfiguration LoadConfiguration(string configurationPath)
		{
			if (string.IsNullOrEmpty(configurationPath))
			{
				throw new ArgumentNullException(nameof(configurationPath), "Parser configuration file is not specified.");
			}

			if (!File.Exists(configurationPath))
			{
				throw new FileNotFoundException($"Parser configuration file {configurationPath} doesn't exist.",
					configurationPath);
			}

			return JsonSerializer.Deserialize<ParserConfiguration>(File.ReadAllText(configurationPath));
		}

		public static ParserConfiguration ParseConfiguration(string configuration)
		{
			return JsonSerializer.Deserialize<ParserConfiguration>(configuration);
		}

		public override void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		///   Main class method. Process
		/// </summary>
		public override void Parse()
		{
			if (DocumentSettings.Count == 0) return;

			var documents = GetBaseDocuments(DocumentSettings);
			if (documents == null) return;

			try
			{
				foreach (var docBlock in documents)
				{
					if (!string.IsNullOrEmpty(docBlock))
					{
						ProcessDocument(docBlock, DocumentSettings);
					}
				}
			}
			catch (Exception e)
			{
				Log.LogError(e, "Document parsing error");
				throw;
			}
		}

		private List<DocumentSettings> DocumentSettings
		{
			get
			{
				if (_documentSettings == null)
				{
					_documentSettings = ParserConfiguration.DocumentsSettings
						.Where(d => d.FileIdentificators.All(i => Regex.IsMatch(_textContent, i, RegexOptions.Multiline)))
						 .ToList();
				}

				return _documentSettings;
			}
		}

		/// <summary>
		///   Performs application-defined tasks associated with freeing, releasing, or resetting
		///   unmanaged resources.
		/// </summary>
		protected virtual void Dispose(bool disposing) { }

		private string[] GetBaseDocuments(IEnumerable<DocumentSettings> settings)
		{
			foreach (var set in settings)
			{
				if (string.IsNullOrEmpty(set.DocumentSplitter))
				{
					return new[] { _textContent };
				}

				return Regex.Split(_textContent, set.DocumentSplitter, RegexOptions.Multiline);
			}
			return null;
		}

		private void ProcessDocument(string docBlock, IEnumerable<DocumentSettings> settings)
		{
			try
			{
				foreach (var set in settings)
				{
					if (set.DocumentTypePatterns.All(c => Regex.Match(docBlock, c).Success))
					{
						var document = ReportDocumentParser.Parse(docBlock, set);
						if (document != null)
						{
							Documents.Add(document);
						}
					}
				}
			}
			catch (Exception e)
			{
				Log.LogError(e, "Document processing error");
				throw;
			}
		}
	}
}