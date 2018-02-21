using Newtonsoft.Json;

namespace Cloud17.IO.Parsing.Iso8583.Configuration
{
	/// <summary>
	///   Sub element info
	/// </summary>
	public class SubElementInfo
	{
		#region Constructors

		/// <summary>
		///   Default constructor
		/// </summary>
		public SubElementInfo()
		{
		}

		/// <summary>
		///   Create instance of sub element
		/// </summary>
		/// <param name="tagLength">Specified data length for tag ID</param>
		/// <param name="dataLength">Specify data length for sublement data</param>
		public SubElementInfo(int tagLength, int dataLength) : this()
		{
			TagLength = tagLength;
			DataLength = dataLength;
		}

		#endregion

		#region Public

		/// <summary>
		///   Tag ID data length
		/// </summary>
		[JsonProperty("tagLength", Required = Required.Always)]
		public int TagLength { get; set; }

		/// <summary>
		///   Data data length
		/// </summary>
		[JsonProperty("dataLength", Required = Required.Always)]
		public int DataLength { get; set; }

		#endregion
	}
}