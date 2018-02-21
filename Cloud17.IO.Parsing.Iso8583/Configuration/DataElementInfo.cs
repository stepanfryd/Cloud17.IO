using System;
using Newtonsoft.Json;

namespace Cloud17.IO.Parsing.Iso8583.Configuration
{
	/// <summary>
	///   Data element info
	/// </summary>
	public class DataElementInfo
	{
		#region Fields

		/// <summary>
		///   Sub element info
		/// </summary>
		[JsonProperty("elements", NullValueHandling = NullValueHandling.Ignore)] public SubElementInfo Elements;

		/// <summary>
		///   Array of sub field info
		/// </summary>
		[JsonProperty("fields", NullValueHandling = NullValueHandling.Ignore)] public SubFieldInfo[] Fields;

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
		/// <param name="fixLen">Fixed data length</param>
		/// <param name="varLen">Variable data length</param>
		public DataElementInfo(int? fixLen, int? varLen)
			: this(fixLen, varLen, null, null)
		{
		}

		/// <summary>
		///   Create instance of object
		/// </summary>
		/// <param name="fixLen">Fixed data length</param>
		/// <param name="varLen">Variable data length</param>
		/// <param name="subElement">Sub element info</param>
		public DataElementInfo(int? fixLen, int? varLen, SubElementInfo subElement)
			: this(fixLen, varLen, subElement, null)
		{
		}

		/// <summary>
		///   Create instance of object
		/// </summary>
		/// <param name="fixLen">Fixed data length</param>
		/// <param name="varLen">Variable data length</param>
		/// <param name="subfields">Sub fields infos</param>
		public DataElementInfo(int? fixLen, int? varLen, SubFieldInfo[] subfields) : this(fixLen, varLen, null, subfields)
		{
		}

		/// <summary>
		///   Create instance of object
		/// </summary>
		/// <param name="fixLen"></param>
		/// <param name="varLen"></param>
		/// <param name="subElement"></param>
		/// <param name="subfields"></param>
		public DataElementInfo(int? fixLen, int? varLen, SubElementInfo subElement, SubFieldInfo[] subfields)
		{
			if (fixLen == null && varLen == null) throw new ApplicationException($"Pamether fixLen or varLen must be set.");
			if (fixLen != null && varLen != null)
				throw new ApplicationException($"Both parameters (fixLen and varLen) cannot be set. Only one of them can be set");

			FixLen = fixLen;
			VarLen = varLen;
			Elements = subElement;
			Fields = subfields;
		}

		#endregion

		#region Public

		/// <summary>
		///   Fix length data value
		/// </summary>
		[JsonProperty("fix", NullValueHandling = NullValueHandling.Ignore)]
		public int? FixLen { get; set; }

		/// <summary>
		///   Variable length data value
		/// </summary>
		[JsonProperty("var", NullValueHandling = NullValueHandling.Ignore)]
		public int? VarLen { get; set; }

		/// <summary>
		///   Data element is in the message
		/// </summary>
		[JsonIgnore]
		public bool Exists { get; set; }

		#endregion

		/// <summary>
		///   Data element info is valid
		/// </summary>
		/// <returns></returns>
		public bool IsValid()
		{
			return !(FixLen == null && VarLen == null) && !(FixLen != null && VarLen != null);
		}
	}
}