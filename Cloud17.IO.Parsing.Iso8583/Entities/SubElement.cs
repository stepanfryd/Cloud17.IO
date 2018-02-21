using System.Collections.Generic;

namespace Cloud17.IO.Parsing.Iso8583.Entities
{
	/// <summary>
	///   Message Private Data Subelement sub element PDS
	/// </summary>
	public class SubElement
	{
		#region Constructors

		/// <summary>
		/// Default constructor
		/// </summary>
		public SubElement() : this(0, 0, null, new List<SubField>())
		{
		}

		/// <summary>
		/// Base constructor
		/// </summary>
		/// <param name="code">Sub element code</param>
		/// <param name="length">Sub element bit lenght</param>
		/// <param name="value">Sub element value</param>
		/// <param name="subFields">List of sub element subfields</param>
		public SubElement(int code, int length, string value, List<SubField> subFields = null)
		{
			if (subFields == null) subFields = new List<SubField>();

			Code = code;
			Length = length;
			Value = value;
			SubFields = subFields;
		}

		#endregion Constructors

		#region Public

		/// <summary>
		///   Sub element code
		/// </summary>
		public int Code { get; set; }

		/// <summary>
		///   Sub element value
		/// </summary>
		public string Value { get; set; }

		/// <summary>
		///   Data length
		/// </summary>
		public int Length { get; set; }

		/// <summary>
		///   Sub element sub fields
		/// </summary>
		public List<SubField> SubFields { get; set; }

		#endregion Public
	}
}