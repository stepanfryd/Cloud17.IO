namespace Cloud17.IO.Parsing.Iso8583.Entities
{
	/// <summary>
	///   Data element sub field
	/// </summary>
	public class SubField
	{
		#region Public

		/// <summary>
		///   Sub field name
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		///   Sub field value
		/// </summary>
		public string Value { get; set; }

		/// <summary>
		///   Sub field data length
		/// </summary>
		public int Length { get; set; }

		/// <summary>
		/// Subfield Code
		/// </summary>
		public string Code { get; set; }

		#endregion
	}
}