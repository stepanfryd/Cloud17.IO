using System.Collections.Generic;
using System.Xml.Serialization;
using System.Diagnostics;

namespace Cloud17.IO.Parsing.Iso8583.Entities
{
	/// <summary>
	///   Message data element
	/// </summary>
	[XmlRoot("dataElement")]
	[DebuggerDisplay("[{Code}] {Value}")]
	public class DataElement
	{
		#region Public

		/// <summary>
		///   Data element code
		/// </summary>
		[XmlAttribute("code")]
		public int Code { get; set; }

		/// <summary>
		///   Data element raw value
		/// </summary>
		[XmlAttribute("value")]
		public string Value { get; set; }

		/// <summary>
		///   Data element data length
		/// </summary>
		[XmlAttribute("length")]
		public int Length { get; set; }

		/// <summary>
		///   List of sub elements
		/// </summary>
		[XmlElement(ElementName = "subElement", IsNullable = true)]
		public List<SubElement> SubElements { get; set; }

		/// <summary>
		///   List of Sub fields
		/// </summary>
		[XmlElement(ElementName = "subField", IsNullable = true)]
		public List<SubField> SubFields { get; set; }

		#endregion

		#region Constructors

		/// <summary>
		///   Creates instance of type
		/// </summary>
		public DataElement()
		{
			SubElements = new List<SubElement>();
			SubFields = new List<SubField>();
		}

		#endregion

		/// <summary>
		/// Sub element should be serialized if any
		/// </summary>
		/// <returns></returns>
		public bool ShouldSerializeSubElements()
		{
			return SubElements?.Count > 0;
		}

		/// <summary>
		/// Subfields should be serialized if any
		/// </summary>
		/// <returns></returns>
		public bool ShouldSerializeSubFields()
		{
			return SubFields?.Count > 0;
		}
	}
}