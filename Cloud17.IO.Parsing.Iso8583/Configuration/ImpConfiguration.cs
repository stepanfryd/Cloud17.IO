using Newtonsoft.Json;
using System.Collections.Generic;

namespace Cloud17.IO.Parsing.Iso8583.Configuration
{
	/// <summary>
	/// Iso8583 elements and fields parser configuration
	/// </summary>
	public class ImpConfiguration
	{
		#region Public Classes

		/// <summary>
		/// Configuration specifies which data elements,
		/// message types and private data subelements are procesed from ISO8583 file
		/// </summary>
		public class ProcessConfig
		{
			#region Public Fields

			/// <summary>
			/// Set of Data elements to process
			/// </summary>
			[JsonProperty("dataElements")]
			public int[] DataElements;

			/// <summary>
			/// Set of message types to process
			/// </summary>
			[JsonProperty("messageTypes")]
			public int[] MessageTypes;

			/// <summary>
			/// Set of Private Data Subelements to process
			/// </summary>
			[JsonProperty("pds")]
			public int[] Pds;

			#endregion Public Fields
		}

		#endregion Public Classes

		#region Public Properties

		/// <summary>
		/// Configuration section for data elements
		/// </summary>
		[JsonProperty("dataElements")]
		public DataElementConfig DataElements { get; set; }

		/// <summary>
		/// Configuration of data which are processed from file
		/// </summary>
		[JsonProperty("process")]
		public ProcessConfig Process { get; set; }

		/// <summary>
		/// Configuration section for private data subelement (PDS) fields
		/// </summary>
		[JsonProperty("subDataElementFields")]
		public Dictionary<string, SubFieldInfo[]> SubDataElementFields { get; set; }

		#endregion Public Properties

		#region Public Constructors

		/// <summary>
		/// Configuration base constructor
		/// </summary>
		/// <param name="dataElementConfig">Dictionary of data elements configurations</param>
		/// <param name="subDataElementFieldsConfig">Dictionary of subfields configurations</param>
		public ImpConfiguration(Dictionary<int, DataElementInfo> dataElementConfig = null,
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