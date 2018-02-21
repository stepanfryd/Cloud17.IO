using Newtonsoft.Json;

namespace Cloud17.IO.Parsing.Iso8583.Configuration
{
	/// <summary>
	///   Sub field info
	/// </summary>
	public class SubFieldInfo
	{
		#region Constructors

		/// <summary>
		///   Create instance of subfield info
		/// </summary>
		/// <param name="length">Field data length</param>
		/// <param name="name">Field name</param>
		public SubFieldInfo(int length, string name)
		{
			Length = length;
			Name = name;
		}

		#endregion

		#region Public

		/// <summary>
		///   Field data length
		/// </summary>
		[JsonProperty("len", Required = Required.Always)]
		public int Length { get; set; }

		/// <summary>
		///   Field name
		/// </summary>
		[JsonProperty("name", Required = Required.Always)]
		public string Name { get; set; }

		/// <summary>
		///   Field Justification (left/right)
		/// </summary>
		[JsonProperty("justifi")]
		public string Justification { get; set; }

		/// <summary>
		///   Field Justification (left/right)
		/// </summary>
		[JsonProperty("code")]
		public string Code { get; set; }
		#endregion
	}
}