
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Cloud17.IO.Parsing.Iso8583.Configuration
{
	/// <summary>
	///   Data element info
	/// </summary>
	public class DataElementInfo
	{

		#region Public properties

		/// <summary>
		///   Sub element info
		/// </summary>
		[JsonPropertyName("elements")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public SubElementInfo Elements { get; set; }

		/// <summary>
		///   Array of sub field info
		/// </summary>
		[JsonPropertyName("fields")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public List<SubFieldInfo> Fields { get; set; } = new List<SubFieldInfo>();

		/*
		/// <summary>
		///   Fix length data value
		/// </summary>
		[JsonPropertyName("fix")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public int? FixLen
		{
			get
			{
				if (!IsVariable) return Length; else return null;
			}
			set
			{
				if (value != null)
				{
					Length = value.Value;
				}
			}
		}

		/// <summary>
		///   Variable length data value
		/// </summary>
		[JsonPropertyName("var")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public int? VarLen
		{
			get
			{
				if (IsVariable) return Length; else return null;
			}
			set
			{
				if (value != null)
				{
					Length = value.Value;
					IsVariable = true;
				}
			}
		}
		*/

		public DataLength DataLength { get; set; }


		/// <summary>
		///   Data element is in the message
		/// </summary>
		[JsonIgnore]
		public bool Exists { get; set; }

		#endregion

		#region Constructors

		/// <summary>
		///   Creates instance of object
		/// </summary>
		public DataElementInfo()
		{
		}

		/// <summary>
		///   Create instance of object
		/// </summary>
		/// <param name="length">Data length</param>
		/// <param name="isVariable">Is variable data length</param>
		public DataElementInfo(int length, bool isVariable)
			: this(length, isVariable, null, null)
		{
		}

		/// <summary>
		///   Create instance of object
		/// </summary>
		/// <param name="length">Data length</param>
		/// <param name="isVariable">Is variable data length</param>
		/// <param name="subElement">Sub elements info</param>
		public DataElementInfo(int length, bool isVariable, SubElementInfo subElement)
			: this(length, isVariable, subElement, null)
		{
		}

		/// <summary>
		///   Create instance of object
		/// </summary>
		/// <param name="length">Data length</param>
		/// <param name="isVariable">Is variable data length</param>
		/// <param name="subfields">Sub fields infos</param>
		public DataElementInfo(int length, bool isVariable, IEnumerable<SubFieldInfo> subfields) : this(length, isVariable, null, subfields)
		{
		}

		/// <summary>
		///   Create instance of object
		/// </summary>
		/// <param name="length">Data length</param>
		/// <param name="isVariable">Is variable data length</param>
		/// <param name="subElement">Sub elements info</param>
		/// <param name="subfields">Sub fields infos</param>
		public DataElementInfo(int length, bool isVariable, SubElementInfo subElement, IEnumerable<SubFieldInfo> subfields)
		{
			DataLength = new DataLength
			{
				Length = length,
				IsVariable = isVariable
			};
			Elements = subElement;
			Fields = new List<SubFieldInfo>(subfields);
		}

		#endregion
	}
}