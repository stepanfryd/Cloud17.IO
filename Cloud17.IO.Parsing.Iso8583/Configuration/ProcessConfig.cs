using System.Text.Json.Serialization;

namespace Cloud17.IO.Parsing.Iso8583.Configuration
{
	/// <summary>
	/// Configuration specifies which data elements,
	/// message types and private data subelements are procesed from ISO8583 file
	/// </summary>
	public class ProcessConfig
	{
		#region Public properties

		/// <summary>
		/// Set of Data elements to process
		/// </summary>
		[JsonPropertyName("dataElements")]
		public int[] DataElements { get; set; }

		/// <summary>
		/// Set of message types to process
		/// </summary>
		[JsonPropertyName("messageTypes")]
		public int[] MessageTypes { get; set; }

		/// <summary>
		/// Set of Private Data Subelements to process
		/// </summary>
		[JsonPropertyName("pds")]
		public int[] Pds { get; set; }

		#endregion Public Fields
	}
}
