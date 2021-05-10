using Cloud17.IO.Parsing.Interfaces;

using System;
using System.Globalization;
using System.Text.Json.Serialization;


namespace Cloud17.IO.Parsing.Configuration
{
	/// <summary>
	///   Single document settings
	/// </summary>
	public class DocumentSettings
	{
		#region Fields
		private Type _dataEntity;
		private Type _dataProcessor;
		#endregion

		#region Public

		/// <summary>
		/// Culture Info Name. Default en-US
		/// </summary>
		[JsonPropertyName("documentCulture")]
		public string DocumentCultureName { get; set; } = "en-US";

		/// <summary>
		/// Document culture info. Default CultureInfo("en-US")
		/// </summary>
		public CultureInfo DocumentCulture => new CultureInfo(DocumentCultureName);

		/// <summary>
		/// Array of paterns for file content and type identification
		/// </summary>
		[JsonPropertyName("fileIdentificators")]
		public string[] FileIdentificators { get; set; }

		/// <summary>
		/// C# string expression to preproces file content. eg. new StringBuilder(\"{0}\").Replace(\"\\\\n\", \"\\\\r\\\\n\").ToString(). Must have string result value.
		/// </summary>
		[JsonPropertyName("contentPreprocess")]
		public string ContentPreprocess { get; set; }

		/// <summary>
		///   Regular expression patterns which identify document type
		/// </summary>
		[JsonPropertyName("documentTypePatterns")]
		public string[] DocumentTypePatterns { get; set; }

		/// <summary>
		/// Regular expression string which defines splitter for multiple documents in one file
		/// </summary>
		[JsonPropertyName("documentSplitter")]
		public string DocumentSplitter { get; set; }

		/// <summary>
		///   Type of document data processor. Must implements <see cref="IDataProcessor" />
		/// </summary>
		[JsonPropertyName("dataProcessor")]
		public Type DataProcessor
		{
			get { return _dataProcessor; }
			set
			{
				var type = value;

				if (!typeof(IDataProcessor).IsAssignableFrom(type))
				{
					throw new NotImplementedException($"Type {type} do not implements interface {typeof(IDataProcessor)}");
				}

				_dataProcessor = type;
			}
		}

		/// <summary>
		///   Document entity. Must implements <see cref="IReportDocument" />
		/// </summary>
		[JsonPropertyName("dataEntity")]
		public Type DataEntity
		{
			get { return _dataEntity; }
			set
			{
				var type = value;

				if (!typeof(IReportDocument).IsAssignableFrom(type))
				{
					throw new NotImplementedException($"Type {type} do not implements interface {typeof(IReportDocument)}");
				}

				_dataEntity = type;
			}
		}

		/// <summary>
		///   Entity properties mappings
		/// </summary>
		[JsonPropertyName("mappings")]
		public ItemMapping[] Mappings { get; set; }

		#endregion
	}
}