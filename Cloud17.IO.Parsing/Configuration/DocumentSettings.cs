using System;
using System.Globalization;
using Cloud17.IO.Parsing.Interfaces;
using Newtonsoft.Json;

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
		[JsonProperty("documentCulture")]
		public string DocumentCultureName { get; set; } = "en-US";

		/// <summary>
		/// Document culture info. Default CultureInfo("en-US")
		/// </summary>
		public CultureInfo DocumentCulture => new CultureInfo(DocumentCultureName);

		/// <summary>
		/// Array of paterns for file content and type identification
		/// </summary>
		[JsonProperty("fileIdentificators")]
		public string[] FileIdentificators { get; set; }

		/// <summary>
		/// C# string expression to preproces file content. eg. new StringBuilder(\"{0}\").Replace(\"\\\\n\", \"\\\\r\\\\n\").ToString(). Must have string result value.
		/// </summary>
		[JsonProperty("contentPreprocess")]
		public string ContentPreprocess { get; set; }

		/// <summary>
		///   Regular expression patterns which identify document type
		/// </summary>
		[JsonProperty("documentTypePatterns")]
		public string[] DocumentTypePatterns { get; set; }

		/// <summary>
		/// Regular expression string which defines splitter for multiple documents in one file
		/// </summary>
		[JsonProperty("documentSplitter")]
		public string DocumentSplitter { get; set; }

		/// <summary>
		///   Type of document data processor. Must implements <see cref="IDataProcessor" />
		/// </summary>
		[JsonProperty("dataProcessor")]
		public Type DataProcessor
		{
			get { return _dataProcessor; }
			set
			{
				var type = value;

				if (!typeof (IDataProcessor).IsAssignableFrom(type))
				{
					throw new NotImplementedException($"Type {type} do not implements interface {typeof (IDataProcessor)}");
				}

				_dataProcessor = type;
			}
		}

		/// <summary>
		///   Document entity. Must implements <see cref="IReportDocument" />
		/// </summary>
		[JsonProperty("dataEntity")]
		public Type DataEntity
		{
			get { return _dataEntity; }
			set
			{
				var type = value;

				if (!typeof (IReportDocument).IsAssignableFrom(type))
				{
					throw new NotImplementedException($"Type {type} do not implements interface {typeof (IReportDocument)}");
				}

				_dataEntity = type;
			}
		}

		/// <summary>
		///   Entity properties mappings
		/// </summary>
		[JsonProperty("mappings")]
		public ItemMapping[] Mappings { get; set; }

		#endregion
	}
}