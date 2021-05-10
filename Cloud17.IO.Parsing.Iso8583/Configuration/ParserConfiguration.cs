using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Cloud17.IO.Parsing.Iso8583.Configuration
{
	/// <summary>
	/// Iso8583 elements and fields parser configuration
	/// </summary>
	public class ParserConfiguration
	{
		#region Public Properties

		/// <summary>
		/// Configuration section for data elements
		/// </summary>
		[JsonPropertyName("dataElements")]
		public DataElementConfig DataElements { get; set; } = new DataElementConfig();

		/// <summary>
		/// Configuration of data which are processed from file
		/// </summary>
		[JsonPropertyName("process")]
		public ProcessConfig Process { get; set; }

		/// <summary>
		/// Configuration section for private data subelement (PDS) fields
		/// </summary>
		[JsonPropertyName("subDataElementFields")]
		public Dictionary<string, SubFieldInfo[]> SubDataElementFields { get; set; }

		#endregion Public Properties

		#region Public Constructors

		public ParserConfiguration() { }

		/// <summary>
		/// Configuration base constructor
		/// </summary>
		/// <param name="dataElementConfig">Dictionary of data elements configurations</param>
		/// <param name="subDataElementFieldsConfig">Dictionary of subfields configurations</param>
		public ParserConfiguration(Dictionary<int, DataElementInfo> dataElementConfig = null,
									Dictionary<string, SubFieldInfo[]> subDataElementFieldsConfig = null)
		{
			if (dataElementConfig != null)
			{
				DataElements = new DataElementConfig(dataElementConfig);
			}

			if (subDataElementFieldsConfig != null)
			{
				SubDataElementFields = subDataElementFieldsConfig;
			}
		}

		#endregion Public Constructors
	}
}