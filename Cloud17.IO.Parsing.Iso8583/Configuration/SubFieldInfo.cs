using System.Text.Json.Serialization;

namespace Cloud17.IO.Parsing.Iso8583.Configuration
{
	/// <summary>
	///   Sub field info
	/// </summary>
	public class SubFieldInfo
	{
		#region Public properties

		/// <summary>
		///   Field data length
		/// </summary>
		[JsonPropertyName("len")]
		public int Length { get; set; }

		/// <summary>
		///   Field name
		/// </summary>
		[JsonPropertyName("name")]
		public string Name { get; set; }

		/// <summary>
		///   Field Justification (left/right)
		/// </summary>
		[JsonPropertyName("justify")]
		public string Justification { get; set; }

		/// <summary>
		///   Field Justification (left/right)
		/// </summary>
		[JsonPropertyName("code")]
		public string Code { get; set; }
		#endregion

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

	}
}